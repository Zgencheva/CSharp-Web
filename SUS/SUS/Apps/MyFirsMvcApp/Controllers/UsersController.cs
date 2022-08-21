using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.IO;
using System.Text;

namespace MyFirsMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return this.View();
        }
        
        public HttpResponse Register()
        {
            return this.View();
            
        }
        [HttpPost]
        internal HttpResponse DoLogin()
        {
            //read data
            //check user
            //log user
            //home page

            return this.Redirect("/");
        }
    }
}
