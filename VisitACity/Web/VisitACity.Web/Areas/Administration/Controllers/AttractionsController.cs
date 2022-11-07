namespace VisitACity.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Attractions;
    using VisitACity.Web.ViewModels.Cities;

    public class AttractionsController : AdministrationController
    {
        private readonly IAttractionsService attractionsService;
        private readonly ICitiesService citiesService;

        public AttractionsController(
            IAttractionsService attractionsService,
            ICitiesService citiesService)
        {
            this.attractionsService = attractionsService;
            this.citiesService = citiesService;
        }

        public async Task<IActionResult> Create()
        {
            var model = new AttractionFormModel();
            model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AttractionFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            try
            {
                await this.attractionsService.CreateAsync(model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            this.TempData["Message"] = "Attraction added successfully.";
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var modelToEdit = await this.attractionsService.GetViewModelByIdAsync<AttractionFormModel>(id);
            modelToEdit.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
            return this.View(modelToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AttractionFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            try
            {
                await this.attractionsService.UpdateAsync(id, model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            this.TempData["Message"] = "Attraction updated successfully.";
            var city = await this.citiesService.GetByIdAsync<CityViewModel>(model.CityId);
            return this.RedirectToAction("Details", "Attractions", new { area = "", id});
        }

        public async Task<IActionResult> Delete(int id)
        {

            await this.attractionsService.DeleteByIdAsync(id);

            this.TempData["Message"] = "Attraction deleted successfully.";
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
