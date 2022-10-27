namespace VisitACity.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
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

            try
            {
                await this.countriesService.CreateAsync(model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }
            this.TempData["Message"] = $"Country {model.Name} added successfully.";
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
