namespace VisitACity.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Common;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Countries;

    public class CountriesController : AdministrationController
    {
        private readonly ICountriesService countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IActionResult Create()
        {
            var model = new CountryFormModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var countryName = model.Name;
            if (await this.countriesService.DoesCountryExist(countryName))
            {
                this.ModelState.AddModelError(string.Empty, string.Format(ModelConstants.City.CityExists, $"{countryName}"));
                return this.View(model);
            }

            try
            {
                await this.countriesService.CreateAsync(model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            this.TempData["Message"] = string.Format(TempDataMessageConstants.CountryAdded, $"{model.Name}");
            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var model = new CountryFormModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CountryFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var countryName = model.Name;
            if (!await this.countriesService.DoesCountryExist(countryName))
            {
                this.ModelState.AddModelError(string.Empty, string.Format(ModelConstants.Country.CountryDoesNotExists, $"{countryName}"));
                return this.View(model);
            }

            try
            {
                await this.countriesService.DeleteAsync(model.Name);
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
