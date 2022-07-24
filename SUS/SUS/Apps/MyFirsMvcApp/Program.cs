﻿using MyFirsMvcApp.Controllers;
using SUS.HTTP;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyFirsMvcApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new HttpServer();

            server.AddRoute("/", new HomeController().Index);
            server.AddRoute("/favicon.ico", new StaticFileController().Favicon);
           
            server.AddRoute("/users/login", new UsersControler().Login);
            server.AddRoute("/users/register", new UsersControler().Register);
            server.AddRoute("/cards/all", new CardsController().All);
            server.AddRoute("/cards/add", new CardsController().Add);
            server.AddRoute("/cards/collection", new CardsController().Collection);
            Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", "http://localhost");
            await server.StartAsync(80);
        }

    }
}