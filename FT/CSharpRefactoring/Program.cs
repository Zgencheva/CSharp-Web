using System.Text;

namespace CSharpRefactoring
{
    internal class Program
    {
        public async Task<string> CancelationDelivery(bool hasLogging,
                                                string ignoreList = null,
                                                HeaderEvent rabbitEvent = null,
                                                IfaceInterfaceManager faceInterfaceManager)
        {
            StringBuilder processingLog = new StringBuilder();
            AddProcessLog(processingLog, "CancelationDelivery started!");

            DateTime startTime = DateTime.UtcNow();

            List<IfaceInterface> interfaces = faceInterfaceManager?.LoadIfaceInterfacesByType(this._db.IfaceDatabase(), DbConstants.IFACE_TYPE_API_ID);
            IgnoreCheckInterfaces(interfaces, ignoreList);

            foreach (IfaceInterface iface in interfaces)
            {
                int totalProcessed = 0;
                int totalErrors = 0;

                if (iface.IfaceInterfaceOptions.OTARemoteSystem == null)
                {
                    AddProcessLog(processingLog, $"Invalid/Missing  options for {iface.Name}");
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
                        var localDb = this._db.GetLocaDB(dbCode);
                        var spRemoteId = obj.objMap.RemoteId;
                        Guid spId = obj.Id;

                        if (rabbitEvent != null && rabbitEvent.DatabaseCode != dbCode)
                        {
                            continue; //rabbit message: skip, not my db
                        }
                        List<ExpCancelationDelivery> cancelInfos = Manager.Instance.GetCencelationInformation(localDb, spId, rabbitEvent?.parentId);

                        if (cancelInfos == null || cancelInfos.Count == 0)
                        {
                            continue;
                        }
                        totalProcessed++;
                        List<ExpCancelationDelivery> details = cancelInfos
                                        .Where(c => c.CurrentStatus == TransactionExternalStatus.ReadyToProcessCancel
                                        || c.CurrentStatus == TransactionExternalStatus.Ok).toList();
                        if (details.Count == 0)
                        {
                            continue;
                        }


                        foreach (var detail in details)
                        {
                            totalErrors++;
                            try
                            {
                                HttpClient httpClient = new HttpClient();
                                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                httpClient.BaseAddress = new Uri(domain);
                                string requestUrl = $"{domain}companies/{spRemoteId}/bookings/{detail.ExternalId}/";
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
                                AddProcessLog(processingLog, $"Exception cancellation delivery TransactionExternalId->{detail.TransactionExternalId}");
                                await this._logging.StoreExceptionLogAsync(ex);
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
                AddProcessLog(processingLog, $"Iface:{iface.Name} -> Processed: {totalProcessed}, Errors: {totalErrors}");
            }
            var duration = (int)(DateTime.UtcNow - startTime).TotalMilliseconds;
            AddProcessLog(processingLog, $"Duration in ms: {duration}";
            string strResult = processingLog.ToString();
            if (hasLogging)
            {
                await this._logging.StoreSystemLogAsync(Microsoft.Extensions.Logging.LogLevel.Information, string.Empty, strResult);
            }
            return strResult;
        }
    }
}