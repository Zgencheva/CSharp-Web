namespace VisitACity.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Cities;

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
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.citiesService.CreateAsync(model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }
            this.TempData["Message"] = $"City {model.Name} added successfully.";
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
