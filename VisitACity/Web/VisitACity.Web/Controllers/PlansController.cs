namespace VisitACity.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Plans;

    [Authorize]
    public class PlansController : BaseController
    {
        private readonly IPlansService plansService;
        private readonly ICitiesService citiesService;
        private readonly ICountriesService countriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public PlansController(
            IPlansService plansService,
            ICitiesService citiesService,
            ICountriesService countriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.plansService = plansService;
            this.citiesService = citiesService;
            this.countriesService = countriesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> MyPlans()
        {
            //var user = await this.userManager.GetUserAsync(this.User);
            //var userId = user.Id;

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var plansViewModel = await this.plansService.GetUserPlansAsync(userId);
            var viewModel = new UserPlansViewModel
            {
                Plans = plansViewModel,
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new CreatePlanInputModel();
            viewModel.FromDate = DateTime.UtcNow;
            viewModel.ToDate = DateTime.UtcNow;
            viewModel.CitiesItems = await this.citiesService.GetAllAsKeyValuePairs();
            viewModel.CountriesItems = await this.countriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlanInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var user = await this.userManager.GetUserAsync(this.User);
            //var userId = user.Id;

            try
            {
                await this.plansService.CreateAsync(input, userId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.MyPlans));
        }
        public IActionResult AddAttractionToPlan(int attractionId, int planId)
        {
            return this.RedirectToAction(nameof(this.MyPlans));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.plansService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.MyPlans));
        }
    }
}
