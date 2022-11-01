namespace VisitACity.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Restaurants;
    using VisitACity.Web.ViewModels.Cities;

    public class RestaurantsController : AdministrationController
    {
        private readonly IRestaurantsService restaurantsService;
        private readonly ICitiesService citiesService;

        public RestaurantsController(
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

            this.TempData["Message"] = "Restaurant added successfully.";
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
