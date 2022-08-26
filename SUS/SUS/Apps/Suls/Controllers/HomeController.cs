using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace Suls.Controllers
{

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
          
            return this.View();
        }
    }
}
