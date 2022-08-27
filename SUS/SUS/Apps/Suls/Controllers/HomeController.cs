using Suls.Sevices;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace Suls.Controllers
{

    public class HomeController : Controller
    {
        private readonly IProblemService problemService;

        public HomeController(IProblemService problemService)
        {
            this.problemService = problemService;
        }
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Home/IndexLoggedIn");
            }
          return this.View();
        }
        [HttpGet]
        public HttpResponse IndexLoggedIn()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
             
            var problems = problemService.GetAll();
            return this.View(problems);
        }
    }
}
