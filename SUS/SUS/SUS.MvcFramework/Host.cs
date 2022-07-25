using SUS.HTTP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            application.ConfigureServices();
            application.Configure(routeTable);
            IHttpServer server = new HttpServer(routeTable);
            
            Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", "http://localhost");
            await server.StartAsync(port);
        }
    }
}
