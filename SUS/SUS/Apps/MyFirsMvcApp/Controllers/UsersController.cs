using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.IO;
using System.Text;

namespace MyFirsMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.View();
        }
        
        public HttpResponse Register(HttpRequest request)
        {
            return this.View();
            
        }

        internal HttpResponse DoLogin(HttpRequest arg)
        {
            //read data
            //check user
            //log user
            //home page

            return this.Redirect("/");
        }
    }
}
