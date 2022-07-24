using SUS.HTTP;
using SUS.MvcFramework;
using System.IO;
using System.Text;

namespace MyFirsMvcApp.Controllers
{
    public class UsersControler : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.View("Views/Users/Login.html");
        }
        
        public HttpResponse Register(HttpRequest request)
        {
            return this.View("Views/Users/Register.html");
            
        }
    }
}
