﻿namespace VisitACity.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Common;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Restaurants;
    using VisitACity.Web.ViewModels.Cities;

    public class RestaurantController : AdministrationController
    {
        private readonly IRestaurantsService restaurantsService;
        private readonly ICitiesService citiesService;

        public RestaurantController(
            IRestaurantsService restaurantsService,
            ICitiesService citiesService)
        {
            this.restaurantsService = restaurantsService;
            this.citiesService = citiesService;
        }

        public async Task<IActionResult> Create()
        {
            var model = new RestaurantFromModel();
            model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RestaurantFromModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            try
            {
                await this.restaurantsService.CreateAsync(model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            this.TempData["Message"] = TempDataMessageConstants.Restaurant.RestaurantAdded;
            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var modelToEdit = await this.restaurantsService.GetViewModelByIdAsync<RestaurantFromModel>(id);
            modelToEdit.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
            return this.View(modelToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RestaurantFromModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            try
            {
                await this.restaurantsService.UpdateAsync(id, model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            this.TempData["Message"] = TempDataMessageConstants.Restaurant.RestaurantUpdated;
            return this.RedirectToAction("Details", "Restaurant", new { area = string.Empty, id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.restaurantsService.DeleteByIdAsync(id);

            this.TempData["Message"] = TempDataMessageConstants.Restaurant.RestaurantDeleted;
            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }
    }
}
