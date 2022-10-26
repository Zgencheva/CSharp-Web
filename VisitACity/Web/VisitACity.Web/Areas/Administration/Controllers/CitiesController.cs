using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VisitACity.Services.Data.Contracts;
using VisitACity.Web.ViewModels.Administration.Cities;

namespace VisitACity.Web.Areas.Administration.Controllers
{
    public class CitiesController : AdministrationController
    {
        private readonly ICitiesService citiesService;

        public CitiesController(ICitiesService citiesService)
        {
            this.citiesService = citiesService;
        }

        public IActionResult Create()
        {
            var model = new CreateCityInputModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCityInputModel model)
        {
            await this.citiesService.CreateAsync(model);
            this.TempData["Message"] = "City added successfully.";
            return Ok();
        }
    }
}
