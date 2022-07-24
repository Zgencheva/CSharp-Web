using SUS.HTTP;
using SUS.MvcFramework;
using System.IO;

namespace MyFirsMvcApp.Controllers
{
    public class StaticFileController : Controller
    {
       public  HttpResponse Favicon(HttpRequest request)
        {
            var fileBytes = File.ReadAllBytes("wwwroot/favicon.ico");
            var response = new HttpResponse("image/vnd.microsoft.icon", fileBytes);
            return response;
        }
    }
}
