using Suls.Sevices;
using SUS.HTTP;
using SUS.MvcFramework;

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
                var viewModel = problemService.GetAll();
                return this.View(viewModel, "IndexLoggedIn");
            }
          return this.View();
        }
      
    }
}
