using SUS.HTTP;
using SUS.MvcFramework;
using System.Text;

namespace MyFirsMvcApp.Controllers
{
    public class UsersControler : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            var resposeHtml = "<h1>Login...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(resposeHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }

        public HttpResponse Register(HttpRequest request)
        {
            var resposeHtml = "<h1>Register...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(resposeHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
    }
}
