using MyFirsMvcApp.Controllers;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;


namespace MyFirsMvcApp
{
    public class Startup : IMvcApplication
    {
 
       public void Configure(List<Route> routeTable)
        {
            routeTable.Add(new Route("/", HttpMethod.Get, new HomeController().Index));
            routeTable.Add(new Route("/users/login", HttpMethod.Get, new UsersController().Login));
            routeTable.Add(new Route("/users/login", HttpMethod.Post, new UsersController().DoLogin));
            routeTable.Add(new Route("/users/register", HttpMethod.Get, new UsersController().Register));
            routeTable.Add(new Route("/cards/add", HttpMethod.Get, new CardsController().Add));
            routeTable.Add(new Route("/cards/all", HttpMethod.Get, new CardsController().All));
            routeTable.Add(new Route("/cards/collection", HttpMethod.Get, new CardsController().Collection));

            //static files
            routeTable.Add(new Route("/favicon.ico", HttpMethod.Get, new StaticFileController().Favicon));
            routeTable.Add(new Route("/css/bootstrap.min.css", HttpMethod.Get, new StaticFileController().BootstrapCss));
            routeTable.Add(new Route("/js/custom.js", HttpMethod.Get, new StaticFileController().CustomJs));
            routeTable.Add(new Route("/css/custom.css", HttpMethod.Get, new StaticFileController().CustomCss));
            routeTable.Add(new Route("/js/bootstrap.bundle.min.css", HttpMethod.Get, new StaticFileController().BootstrapJs));
        }

        public void ConfigureServices()
        {
            
        }
    }
}
