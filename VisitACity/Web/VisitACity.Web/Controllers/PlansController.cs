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

    public class PlansController : BaseController
    {
        private readonly IPlansService plansService;
        private readonly ICitiesService citiesService;
        private readonly ICountriesService countriesService;
        private readonly IAttractionsService attractionsService;
        private readonly IRestaurantsService restaurantsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PlansController(
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

        public async Task<IActionResult> AddAttractionToPlan(int attractionId, int planId)
        {
            if (planId == 0)
            {
                int cityToViewModelId = await this.attractionsService.GetAttractionCityIdAsync(attractionId);
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
                    int cityToViewModelId = await this.attractionsService.GetAttractionCityIdAsync(attractionId);
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
            string cityName = await this.restaurantsService.GetRestaurantCityNameAsync(restaurantId);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!await this.plansService.DoesUserHavePlanInTheCity(userId, cityName))
            {
                int cityToViewModelId = await this.restaurantsService.GetRestaurantCityIdAsync(restaurantId);
                this.TempData["Message"] = TempDataMessageConstants.Plan.NoPlansInTheCity;
                return this.RedirectToAction("Create", new { cityId = cityToViewModelId });
            }

            int planId = await this.plansService.GerUserPlanIdAsync(cityName, userId);
            if (await this.plansService.DoesRestaurantExistInThePlan(restaurantId, planId))
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
                    int cityToViewModelId = await this.restaurantsService.GetRestaurantCityIdAsync(restaurantId);
                    this.TempData["Message"] = TempDataMessageConstants.Plan.NoPlansInTheCity;
                    return this.RedirectToAction("Create", new { cityId = cityToViewModelId });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteAttractionFromPlan(int id)
        {
            string cityName = await this.attractionsService.GetAttractionCityNameAsync(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                int planId = await this.plansService.GerUserPlanIdAsync(cityName, userId);
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
            string cityName = await this.restaurantsService.GetRestaurantCityNameAsync(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                int planId = await this.plansService.GerUserPlanIdAsync(cityName, userId);
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
