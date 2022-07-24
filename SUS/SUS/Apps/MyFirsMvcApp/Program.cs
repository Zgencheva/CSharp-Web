using SUS.HTTP;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirsMvcApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new HttpServer();


            server.AddRoute("/", HomePage);
            server.AddRoute("/favicon.ico", Favicon);
            server.AddRoute("/about", About);

            server.AddRoute("/users/login", Login);

            await server.StartAsync(80);
        }

        static HttpResponse HomePage(HttpRequest request)
        {
            var resposeHtml = "<h1>Welcome!</h1>" +
                request.Headers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;
            var responseBodyBytes = Encoding.UTF8.GetBytes(resposeHtml);
            //var resposeHttp = "HTTP/1.1 200 OK" + HttpConstants.NewLine +
            //                   "Server: SUS Server 1.0" + HttpConstants.NewLine +
            //                   "Content-Type: text/html" + HttpConstants.NewLine +
            //                   "Content-Length: " + responseBodyBytes.Length + HttpConstants.NewLine +
            //                    HttpConstants.NewLine;
            var response = new HttpResponse("text/html", responseBodyBytes);
            response.Headers.Add(new Header("Server", "SUS Server 1.0"));
            response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
            { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });
            return response;
        }
        static HttpResponse Favicon(HttpRequest request)
        {
            var fileBytes = File.ReadAllBytes("wwwroot/favicon.ico");
            var response = new HttpResponse("image/vnd.microsoft.icon",fileBytes);
            return response;
        }

        static HttpResponse About(HttpRequest request)
        {
            var resposeHtml = "<h1>About...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(resposeHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);       
            return response;
        }

        static HttpResponse Login(HttpRequest request)
        {
            var resposeHtml = "<h1>Login...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(resposeHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
    }
}
