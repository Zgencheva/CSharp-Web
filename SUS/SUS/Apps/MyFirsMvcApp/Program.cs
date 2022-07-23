using SUS.HTTP;
using System;

namespace MyFirsMvcApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IHttpServer server = new HttpServer();


            server.AddRoute("/", HomePage);

            server.AddRoute("/about", About);

            server.AddRoute("/users/login", Login);

            server.Start(80);
        }

        static HttpResponse HomePage(HttpRequest request)
        {
            throw new NotImplementedException();
            //return new HttpResponse();
        }

        static HttpResponse About(HttpRequest request)
        {
            throw new NotImplementedException();
            //return new HttpResponse();
        }

        static HttpResponse Login(HttpRequest request)
        {
            throw new NotImplementedException();
            //return new HttpResponse();
        }
    }
}
