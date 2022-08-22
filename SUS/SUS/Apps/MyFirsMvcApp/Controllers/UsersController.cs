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
        [HttpPost("Users/Register")]
        internal HttpResponse DoRegister()
        {
            //read data
            //check user
            //log user
            //home page

            return this.Redirect("/");
        }
        [HttpPost("Users/Login")]
        internal HttpResponse DoLogin()
        {
            //read data
            //check user
            //log user
            //home page

            return this.Redirect("/");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("Only logged-in users can logout.");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
