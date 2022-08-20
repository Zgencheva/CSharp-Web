﻿using MyFirsMvcApp.Controllers;
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
            routeTable.Add(new Route("/home/about", HttpMethod.Get, new HomeController().About));
            routeTable.Add(new Route("/users/login", HttpMethod.Get, new UsersController().Login));
            routeTable.Add(new Route("/users/login", HttpMethod.Post, new UsersController().DoLogin));
            routeTable.Add(new Route("/users/register", HttpMethod.Get, new UsersController().Register));
            routeTable.Add(new Route("/cards/add", HttpMethod.Get, new CardsController().Add));
            routeTable.Add(new Route("/cards/all", HttpMethod.Get, new CardsController().All));
            routeTable.Add(new Route("/cards/collection", HttpMethod.Get, new CardsController().Collection));
        }

        public void ConfigureServices()
        {
            
        }
    }
}