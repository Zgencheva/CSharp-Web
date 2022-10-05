namespace VisitACity.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using VisitACity.Services.Data.Contracts;

    public class AttractionsController : Controller
    {
        private readonly IAttractionsService attractionsService;

        public AttractionsController(IAttractionsService attractionsService)
        {
            this.attractionsService = attractionsService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.attractionsService.GetAttractionById(id);
            return this.View(viewModel);
        }
    }
}
