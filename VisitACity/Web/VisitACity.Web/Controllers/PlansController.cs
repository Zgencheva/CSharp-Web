namespace VisitACity.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Plans;

    public class PlansController : BaseController
    {
        private readonly IPlansService plansService;

        public PlansController(IPlansService plansService)
        {
            this.plansService = plansService;
        }

        public IActionResult MyPlans()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreatePlanInputModel();
            viewModel.FromDate = System.DateTime.UtcNow;
            viewModel.ToDate = System.DateTime.UtcNow;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePlanInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.plansService.CreateAsync(input, userId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(MyPlans));
        }
    }
}
