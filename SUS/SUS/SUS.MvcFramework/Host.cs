using SUS.HTTP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(IMvcApplication application, int port)
        {
            List<Route> routeTable = new List<Route>();
            var staticFiles = Directory.GetFiles("wwwroot", "*", SearchOption.AllDirectories);
            foreach (var staticFile in staticFiles)
            {
                var url = staticFile.Replace("wwwroot", String.Empty)
                    .Replace("\\", "/");
                routeTable.Add(new Route(url, HttpMethod.Get, (request) => 
                {
                    var fileContent = File.ReadAllBytes(staticFile);
                    var fileExtension = new FileInfo(staticFile).Extension;
                    var contentType = fileExtension switch
                    {
                        ".txt" => "text/plain",
                        ".js" => "text/javascript",
                        ".css" => "text/css",
                        ".jpg" => "image/jpg",
                        ".jpeg" => "image/jpg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        ".ico" => "image/vnd.microsoft.icon",
                        ".html" => "text/html",
                        _ => "text/plain",
                    };
                    return new HttpResponse(contentType, fileContent, HttpStatusCode.Ok);

                }));
            }

            application.ConfigureServices();
            application.Configure(routeTable);
            IHttpServer server = new HttpServer(routeTable);
            
            Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", "http://localhost");
            await server.StartAsync(port);
        }
    }
}
