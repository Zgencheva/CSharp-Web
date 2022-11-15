namespace VisitACity.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Attractions;
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
                this.ModelState.AddModelError(string.Empty, "You already have upcoming plan to this city!");
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
                this.TempData["Message"] = "You have no plans in this city. Please create it";
                return this.RedirectToAction("Create", new { cityId = cityToViewModelId });
            }

            if (await this.plansService.DoesAttractionExist(attractionId, planId))
            {
                this.TempData["Message"] = "Attraction already in your plan.";
                return this.RedirectToAction(nameof(this.MyPlans));
            }

            bool result = await this.plansService.AddAttractionToPlanAsync(attractionId, planId);
            if (result == true)
            {
                this.TempData["Message"] = "Attraction added successfully to your plan.";
                return this.RedirectToAction(nameof(this.MyPlans));
            }
            else
            {
                int cityToViewModelId = await this.attractionsService.GetAttractionCityIdAsync(attractionId);
                this.TempData["Message"] = "You have no plans in this city. Please create it";
                return this.RedirectToAction("Create", new { cityId = cityToViewModelId });
            }
        }

        public async Task<IActionResult> AddRestaurantToPlan(int restaurantId, int planId)
        {
            bool result = await this.plansService.AddRestaurantToPlanAsync(restaurantId, planId);
            if (result == true)
            {
                this.TempData["Message"] = "Restaurant added successfully to your plan.";
                return this.RedirectToAction(nameof(this.MyPlans));
            }
            else
            {
                int cityToViewModelId = await this.restaurantsService.GetRestaurantCityIdAsync(restaurantId);
                this.TempData["Message"] = "You have no plans in this city. Please create it";
                return this.RedirectToAction("Create", new { cityId = cityToViewModelId });
            }
        }

        public async Task<IActionResult> DeleteAttractionFromPlan(int attractionId, int planId)
        {
            await this.plansService.DeleteAttractionFromPlanAsync(attractionId, planId);
            this.TempData["Message"] = "Attraction deleted successfully";
            return this.RedirectToAction(nameof(this.MyPlans));
        }

        public async Task<IActionResult> DeleteRestaurantFromPlan(int restaurantId, int planId)
        {
            await this.plansService.DeleteRestaurantFromPlanAsync(restaurantId, planId);
            this.TempData["Message"] = "Restaurant deleted successfully";
            return this.RedirectToAction(nameof(this.MyPlans));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.plansService.DeleteAsync(id);
            this.TempData["Message"] = "Plan deleted successfully.";
            return this.RedirectToAction(nameof(this.MyPlans));
        }
    }
}
