using System.Text;

namespace CSharpRefactoring
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
        public async Task<string> CancelationDelivery(bool logging, 
                                                string ignoreList = null, 
                                                HeaderEvent rabbitEvent = null,
                                                IfaceInterfaceManager faceInterfaceManager)
        {
            StringBuilder processingLog = new StringBuilder();
            AddProcessLog(processingLog, "CancelationDelivery started!");
            //1. I think that DateTime.UtcNow() should be applied here,because it stores a date (as fas as I understand the task) for lalter calculations.
            //2.we use "_" mostly for private fields in classes. I don`t know if this is the case. If startTime is in the same class, so this.startTime should be applied;
            DateTime startTime = DateTime.UtcNow();
            //Or: this._startTine = DateTime.UtcNow();
            //3.If possible, can we receive the InterfaceManager from outside, because there are too many dependencies: IfaceManager, db...
            List<IfaceInterface> interfaces = faceInterfaceManager?.LoadIfaceInterfacesByType(this._db.IfaceDatabase(), DbConstants.IFACE_TYPE_API_ID);
            IgnoreCheckInterfaces(interfaces, ignoreList);

            foreach (IfaceInterface iface in interfaces)
            {
                int totalProcessed = 0;
                int totalErrors = 0;

                if (iface.IfaceInterfaceOptions.OTARemoteSystem == null)
                {
                    AddProcessLog(processingLog, string.Format("Invalid/Missing  options for {0}", iface.Name));
                    continue;
                }

                string domain = iface.IfaceInterfaceOptions.OTARemoteSystem.RemoteUrl;

                var lstData = faceInterfaceManager.LoadObjectsByCompany(this._db.IfaceDatabase(), iface.CompanyId);

                foreach (var obj in lstData)
                {
                    try
                    {
                        if (obj.objMap == null)
                        {
                            continue;
                        }
                        string dbCode = obj.dbCode;
                        //5. this._db.GetLocaDB(dbCod) code reusable.
                        //6. db is comming from where? if it`s class field, let`s place this. as a pointer
                        var localDb = this._db.GetLocaDB(dbCode);
                        var spRemoteId = obj.objMap.RemoteId;
                        Guid spId = obj.Id;

                        if (rabbitEvent != null && rabbitEvent.DatabaseCode != dbCode)
                        {
                            continue; //rabbit message: skip, not my db
                        }
                        //7. Manager instance should also come from outside. New is glue :)
                        //8 if there is no rabbitEvent, it will pass null to the method
                        List<ExpCancelationDelivery> cancelInfos = Manager.Instance.GetCencelationInformation(localDb, spId, rabbitEvent?.parentId);

                        if (cancelInfos == null || cancelInfos.Count == 0)
                        {
                            continue;
                        }
                            
                        //9. This try-catch block has nothing to catch.
                        totalProcessed++;
                        List<ExpCancelationDelivery> details = new List<ExpCancelationDelivery>();
                               //start processing       
                            //11.Filter the if/else if with LINQ, better naming in foreach
                            foreach (var cancelInfo in cancelInfos
                                        .Where(c=> c.CurrentStatus == TransactionExternalStatus.ReadyToProcessCancel
                                        || c.CurrentStatus == TransactionExternalStatus.Ok).toList())
                            {
                                details.Add(cancelInfo);
                            }
                            if (details.Count == 0)
                            {
                                continue;
                            } 


                            foreach (var detail in details)
                            {
                                try
                                {
                                    HttpClient httpClient = new HttpClient();
                                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                    httpClient.BaseAddress = new Uri(domain);
                                    string requestUrl = string.Format("{0}companies/{1}/bookings/{2}/", domain, spRemoteId, detail.ExternalId);
                                    HttpResponseMessage response = await httpClient.DeleteAsync(requestUrl);
                                    string responseJson = string.Empty;

                                    bool success = false;
                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        responseJson = response.Content.ReadAsStringAsync().Result;
                                        BookAnswer cancelAnswer = JsonConvert.DeserializeObject<BookAnswer>(responseJson, new JsonSerializerSettings()
                                        {
                                            NullValueHandling = NullValueHandling.Ignore
                                        });

                                        if (cancelAnswer.booking != null && cancelAnswer.booking.status == "cancelled")
                                        {
                                            success = true;
                                        }
                                    }
                                    else
                                    {
                                        responseJson = response.Content.ReadAsStringAsync().Result;
                                    }

                                    if (success)
                                    {
                                        if (detail.CurrentStatus == TransactionExternalStatus.ReadyToProcessCancel)
                                        {
                                            TransactionManager.Instance.UpdateTransactionExternalInformation(localDb, detail.TransactionExternalId, TransactionExternalStatus.OkCancel, LOGIN_ID, detail.ExternalId, responseJson);
                                        }
                                        else if (detail.CurrentStatus == TransactionExternalStatus.Ok)
                                        {
                                            TransactionManager.Instance.UpdateTransactionExternalInformation(localDb, detail.PreviousTransactionExternalId, TransactionExternalStatus.OkCancel, LOGIN_ID, detail.ExternalId, responseJson);
                                        }
                                    }
                                    else
                                    {
                                    //12.This string is never used
                                       // string combinedLog = string.Format("REQUEST:{0} RESPONSE:{1}", requestUrl, responseJson);

                                        if (detail.CurrentStatus == TransactionExternalStatus.ReadyToProcessCancel)
                                        {
                                            TransactionManager.Instance.UpdateTransactionExternalInformation(localDb, detail.TransactionExternalId, TransactionExternalStatus.ErrorCancel, LOGIN_ID, detail.ExternalId, responseJson);
                                        }
                                        else if (detail.CurrentStatus == TransactionExternalStatus.Ok)
                                        {
                                            TransactionManager.Instance.UpdateTransactionExternalInformation(localDb, detail.PreviousTransactionExternalId, TransactionExternalStatus.ErrorCancel, LOGIN_ID, detail.ExternalId, responseJson);
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                //10.Isn`t this already settled above? And same foreach in the foreach...
                                    //if (details != null && details.Count > 0)
                                    //{
                                        //foreach (var d in details)
                                        //{
                                        //    if (d.CurrentStatus == TransactionExternalStatus.ReadyToProcessCancel)
                                        //    {
                                        //        TransactionManager.Instance.ChangeTransactionExternalsStatus(localDb, d.TransactionExternalId, TransactionExternalStatus.ErrorCancel, null, LOGIN_ID);
                                        //    }
                                        //    else if (d.CurrentStatus == TransactionExternalStatus.Ok)
                                        //    {
                                        //        TransactionManager.Instance.ChangeTransactionExternalsStatus(localDb, d.PreviousTransactionExternalId, TransactionExternalStatus.ErrorCancel, null, LOGIN_ID);
                                        //    }
                                        //}
                                    //}
                                    //13. details[0] will not be correct, just detail, because it`s in the foreach.
                                    AddProcessLog(processingLog, string.Format("Exception cancellation delivery TransactionExternalId->{0}", detail.TransactionExternalId));
                                    //4. StoreExceptionLogAsync - name it async to be clear that there is asynchronous operations going on.
                                    await this._logging.StoreExceptionLogAsync(ex);
                                }
                                //Finally will always occur. So I may move totalErrors there.
                                finally 
                                {
                                    totalErrors++;
                                }
                            }                        
                    }
                    catch (Exception e)
                    {
                        await this._logging.StoreExceptionLogAsync(e);
                        totalErrors++;
                    }
                }

                //update execution time if no critical errors
                AddProcessLog(processingLog, string.Format("Iface:{0} -> Processed: {1}, Errors: {2}", iface.Name, totalProcessed, totalErrors));
            }

            AddProcessLog(processingLog, string.Format("Duration in ms: {0}", (int)(DateTime.UtcNow - startTime).TotalMilliseconds));
            string strResult = processingLog.ToString();
            if (logging)
            {
                await this._logging.StoreSystemLogAsync(Microsoft.Extensions.Logging.LogLevel.Information, string.Empty, strResult);
            }
            return strResult;
        }
    }
}