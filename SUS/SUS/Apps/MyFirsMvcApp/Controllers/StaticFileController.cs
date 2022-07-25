using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.IO;

namespace MyFirsMvcApp.Controllers
{
    public class StaticFileController : Controller
    {
       public  HttpResponse Favicon(HttpRequest request)
        {
            return this.File("wwwroot/favicon.ico", "image/vnd.microsoft.icon");
        }

        public HttpResponse BootstrapCss(HttpRequest request)
        {
            return this.File("wwwroot/css/bootstrap.min.css", "text/css");
        }

        internal HttpResponse CustomJs(HttpRequest arg)
        {
            return this.File("wwwroot/js/custom.js", "text/javascript");

        }

        internal HttpResponse CustomCss(HttpRequest arg)
        {
            return this.File("wwwroot/css/custom.css", "text/css");

        }

        internal HttpResponse BootstrapJs(HttpRequest arg)
        {
            return this.File("wwwroot/css/bootstrap.bundle.min.js", "text/javascript");

        }
    }
}
