using MyFirsMvcApp.Controllers;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyFirsMvcApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<Route> routeTable = new List<Route>();
            routeTable.Add(new Route("/", new HomeController().Index));
            routeTable.Add(new Route("/favicon.ico", new StaticFileController().Favicon));
            routeTable.Add(new Route("/users/login", new UsersControler().Login));
            routeTable.Add(new Route("/users/register", new UsersControler().Register));
            routeTable.Add(new Route("/cards/add", new CardsController().Add));
            routeTable.Add(new Route("/cards/all", new CardsController().All));
            routeTable.Add(new Route("/cards/collection", new CardsController().Collection));

            await Host.CreateHostAsync(routeTable, 80);
        }

    }
}
