namespace VisitACity.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AttractionsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
