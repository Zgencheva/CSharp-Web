namespace VisitACity.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Cities;
    using VisitACity.Web.ViewModels.Countries;
    using VisitACity.Web.ViewModels.Plans;

    public class PlanController : BaseController
    {
        private readonly IPlansService plansService;
        private readonly ICitiesService citiesService;
        private readonly ICountriesService countriesService;
        private readonly IAttractionsService attractionsService;
        private readonly IRestaurantsService restaurantsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PlanController(
            IPlansService plansService,
            ICitiesService citiesService,
            ICountriesService countriesService,
            IAttractionsService attractionsService,
            IRestaurantsService restaurantsService,
            UserManager<ApplicationUser> userManager)
        {
            this.plansService = plansService;
            this.citiesService = citiesService;
            this.countriesService = countriesService;
            this.attractionsService = attractionsService;
            this.restaurantsService = restaurantsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> MyPlans()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var plansViewModel = await this.plansService.GetUserPlansAsync(userId);
            var viewModel = new UserPlansViewModel
            {
                Plans = plansViewModel,
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Create(int cityId)
        {
            var viewModel = new CreatePlanInputModel();
            viewModel.FromDate = DateTime.UtcNow;
            viewModel.ToDate = DateTime.UtcNow;
            viewModel.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
            viewModel.Countries = await this.countriesService.GetAllAsync<CountryViewModel>();
            if (cityId != 0)
            {
                viewModel.CityId = cityId;
                viewModel.CountryId = await this.citiesService.GetCountryIdAsync(cityId);
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlanInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                input.Countries = await this.countriesService.GetAllAsync<CountryViewModel>();
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userPlans = await this.plansService.GetUpcomingUserPlansAsync(userId);
            if (userPlans.Any(x => x.CityId == input.CityId))
            {
                this.ModelState.AddModelError(string.Empty, TempDataMessageConstants.Plan.ExistingUpcomingPlanToTheCity);
                input.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                input.Countries = await this.countriesService.GetAllAsync<CountryViewModel>();
                return this.View(input);
            }

            try
            {
                await this.plansService.CreateAsync(input, userId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                input.Countries = await this.countriesService.GetAllAsync<CountryViewModel>();
                return this.View(input);
            }

            // this.TempData["Message"] = "Plan added successfully.";
            return this.RedirectToAction(nameof(this.MyPlans));
        }

        public async Task<IActionResult> AddAttractionToPlan(string attractionId, int planId)
        {
            if (planId == 0)
            {
                var city = await this.attractionsService.GetAttractionCityAsync(attractionId);
                var cityToViewModelId = city.Id;
                this.TempData["Message"] = TempDataMessageConstants.Plan.NoPlansInTheCity;
                return this.RedirectToAction("Create", new { cityId = cityToViewModelId });
            }

            if (await this.plansService.DoesAttractionExist(attractionId, planId))
            {
                this.TempData["Message"] = TempDataMessageConstants.Attraction.ExistingAttractionToThePlan;
                return this.RedirectToAction(nameof(this.MyPlans));
            }

            try
            {
                bool result = await this.plansService.AddAttractionToPlanAsync(attractionId, planId);
                if (result == true)
                {
                    this.TempData["Message"] = TempDataMessageConstants.Attraction.AttractionAddedToPlan;
                    return this.RedirectToAction(nameof(this.MyPlans));
                }
                else
                {
                    var cityToViewModel = await this.attractionsService.GetAttractionCityAsync(attractionId);
                    int cityToViewModelId = cityToViewModel.Id;
                    this.TempData["Message"] = TempDataMessageConstants.Plan.NoPlansInTheCity;
                    return this.RedirectToAction("Create", new { cityId = cityToViewModelId });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> AddRestaurantToPlan(int restaurantId)
        {
            var city = await this.restaurantsService.GetRestaurantCityAsync(restaurantId);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!await this.plansService.DoesUserHavePlanInTheCity(userId, city.Name))
            {
                this.TempData["Message"] = TempDataMessageConstants.Plan.NoPlansInTheCity;
                return this.RedirectToAction("Create", new { cityId = city.Id });
            }

            int planId = await this.plansService.GetUserPlanIdAsync(city.Name, userId);
            if (await this.plansService.DoesRestaurantExist(restaurantId, planId))
            {
                this.TempData["Message"] = TempDataMessageConstants.Restaurant.ExistingRestaurantToThePlan;
                return this.RedirectToAction(nameof(this.MyPlans));
            }

            try
            {
                bool result = await this.plansService.AddRestaurantToPlanAsync(restaurantId, planId);
                if (result == true)
                {
                    this.TempData["Message"] = TempDataMessageConstants.Restaurant.RestaurantAddedToPlan;
                    return this.RedirectToAction(nameof(this.MyPlans));
                }
                else
                {
                    this.TempData["Message"] = TempDataMessageConstants.Plan.NoPlansInTheCity;
                    return this.RedirectToAction("Create", new { cityId = city.Id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteAttractionFromPlan(string id)
        {
            var city = await this.attractionsService.GetAttractionCityAsync(id);
            var cityName = city.Name;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                int planId = await this.plansService.GetUserPlanIdAsync(cityName, userId);
                await this.plansService.DeleteAttractionFromPlanAsync(id, planId);
                this.TempData["Message"] = TempDataMessageConstants.Attraction.AttractionDeleted;
                return this.RedirectToAction(nameof(this.MyPlans));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteRestaurantFromPlan(int id)
        {
            var city = await this.restaurantsService.GetRestaurantCityAsync(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                int planId = await this.plansService.GetUserPlanIdAsync(city.Name, userId);
                await this.plansService.DeleteRestaurantFromPlanAsync(id, planId);
                this.TempData["Message"] = TempDataMessageConstants.Restaurant.RestaurantDeleted;
                return this.RedirectToAction(nameof(this.MyPlans));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
             await this.plansService.DeleteAsync(id);
             this.TempData["Message"] = TempDataMessageConstants.Plan.PlanDeleted;
             return this.RedirectToAction(nameof(this.MyPlans));
        }
    }
}
