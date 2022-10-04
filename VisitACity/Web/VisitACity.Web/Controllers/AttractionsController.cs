namespace VisitACity.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AttractionsController : Controller
    {
        public IActionResult ById()
        {
            return this.View();
        }
    }
}
