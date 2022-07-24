using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Linq;
using System.Text;

namespace MyFirsMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
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
        public HttpResponse About(HttpRequest request)
        {
            var resposeHtml = "<h1>About...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(resposeHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
    }
}
