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
        public static async Task CreateHostAsync(List<Route> routeTable, int port)
        {
            IHttpServer server = new HttpServer();
            foreach (var route in routeTable)
            {
                server.AddRoute(route.Path, route.Action);
            }
            Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", "http://localhost");
            await server.StartAsync(port);
        }
    }
}
