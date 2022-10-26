namespace VisitACity.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;

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
            var viewModel = await this.attractionsService.GetByIdAsync(id);
            return this.View(viewModel);
        }
    }
}
