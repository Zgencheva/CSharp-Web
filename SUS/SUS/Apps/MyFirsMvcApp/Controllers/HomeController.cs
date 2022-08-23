using MyFirsMvcApp.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace MyFirsMvcApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }

            return this.View();
        }

        public HttpResponse About()
        {
          
            return this.View();
        }
    }
}