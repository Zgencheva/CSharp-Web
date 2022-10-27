﻿namespace VisitACity.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Attractions;

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
            model.Cities = await this.citiesService.GetAllAsync();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AttractionFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Cities = await this.citiesService.GetAllAsync();
                return this.View(model);
            }

            try
            {
                await this.attractionsService.CreateAsync(model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.Cities = await this.citiesService.GetAllAsync();
                return this.View(model);
            }

            this.TempData["Message"] = "Attraction added successfully.";
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = new AttractionFormModel();
            model.Cities = await this.citiesService.GetAllAsync();
            return this.View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = new AttractionFormModel();
            model.Cities = await this.citiesService.GetAllAsync();
            return this.View(model);
        }
    }
}
