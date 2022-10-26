using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VisitACity.Services.Data.Contracts;
using VisitACity.Web.ViewModels.Administration.Attractions;

namespace VisitACity.Web.Areas.Administration.Controllers
{
    public class AttractionsController : AdministrationController
    {
        private readonly IAttractionsService attractionsService;

        public AttractionsController(IAttractionsService attractionsService)
        {
            this.attractionsService = attractionsService;
        }

        public IActionResult Create()
        {
            var model = new CreateAttractionInputModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAttractionInputModel model)
        {
            await this.attractionsService.CreateAsync(model);
            return Ok();
        }
    }
}
