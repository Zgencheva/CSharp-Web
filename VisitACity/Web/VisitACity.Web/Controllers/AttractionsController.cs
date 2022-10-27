namespace VisitACity.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Attractions;

    public class AttractionsController : BaseController
    {
        private readonly IAttractionsService attractionsService;

        public AttractionsController(IAttractionsService attractionsService)
        {
            this.attractionsService = attractionsService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.attractionsService.GetViewModelByIdAsync<AttractionViewModel>(id);
            return this.View(viewModel);
        }
    }
}
