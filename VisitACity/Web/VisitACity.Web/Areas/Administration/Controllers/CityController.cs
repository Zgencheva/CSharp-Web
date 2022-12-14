namespace VisitACity.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Common;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Cities;
    using VisitACity.Web.ViewModels.Countries;

    public class CityController : AdministrationController
    {
        private readonly ICitiesService citiesService;
        private readonly ICountriesService countriesService;

        public CityController(
            ICitiesService citiesService,
            ICountriesService countriesService)
        {
            this.citiesService = citiesService;
            this.countriesService = countriesService;
        }

        public async Task<IActionResult> Create()
        {
            var model = new CityFormModel();
            model.Countries = await this.countriesService.GetAllAsync<CountryViewModel>();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CityFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Countries = await this.countriesService.GetAllAsync<CountryViewModel>();
                return this.View(model);
            }

            try
            {
                   await this.citiesService.CreateAsync(model);
            }
             catch (Exception ex)
            {
                  this.ModelState.AddModelError(string.Empty, ex.Message);
                  model.Countries = await this.countriesService.GetAllAsync<CountryViewModel>();
                  return this.View(model);
            }

            this.TempData["Message"] = string.Format(TempDataMessageConstants.CityAdded, $"{model.Name}");

            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }

        [HttpGet]
        public IActionResult Delete()
        {
            var model = new CityFormModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CityFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var cityName = model.Name;
            if (!await this.citiesService.DoesCityExist(cityName))
            {
                this.ModelState.AddModelError(string.Empty, string.Format(ModelConstants.City.CityDoesNotExist, $"{cityName}"));
                return this.View(model);
            }

            try
            {
                await this.citiesService.DeleteAsync(model.Name);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            this.TempData["Message"] = string.Format(TempDataMessageConstants.CityDeleted, $"{model.Name}");
            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }
    }
}
