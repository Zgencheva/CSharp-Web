using MyFirsMvcApp.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace MyFirsMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            var viewModel = new IndexViewModel();
            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "Welcome to Battle Cards";
            return this.View(viewModel);
        }

        public HttpResponse About(HttpRequest request)
        {
          
            return this.View();
        }
    }
}