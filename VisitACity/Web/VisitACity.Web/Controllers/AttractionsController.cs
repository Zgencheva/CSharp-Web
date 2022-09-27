using Microsoft.AspNetCore.Mvc;

namespace VisitACity.Web.Controllers
{
    public class AttractionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
