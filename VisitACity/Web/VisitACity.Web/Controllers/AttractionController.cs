namespace VisitACity.Web.Controllers
{
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Services.Messaging;
    using VisitACity.Web.ViewModels.Attractions;

    public class AttractionController : BaseController
    {
        private readonly IAttractionsService attractionsService;
        private readonly IPlansService plansService;
        private readonly IEmailSender emailSender;
        private readonly UserManager<ApplicationUser> userManager;

        public AttractionController(
            IAttractionsService attractionsService,
            IPlansService plansService,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager)
        {
            this.attractionsService = attractionsService;
            this.plansService = plansService;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            var user = this.User.FindFirst(ClaimTypes.NameIdentifier);
            var viewModel = await this.attractionsService.GetViewModelByIdAsync<AttractionViewModel>(id);

            if (user != null)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await this.attractionsService.AddReviewToUserAsync(userId, id);
                viewModel.UserPlan = await this.plansService.GetUserUpcomingPlansByCityAsync(viewModel.CityName, userId);

            }

            return this.View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendToEmail(string id)
        {
            var viewModel = await this.attractionsService.GetViewModelByIdAsync<AttractionViewModel>(id);
            var html = new StringBuilder();
            html.AppendLine($"<h1>{viewModel.Name}</h1>");
            html.AppendLine($"<h3>{viewModel.Type.ToString()}</h3>");
            html.AppendLine($"<div>{viewModel.Description}</div>");
            var user = await this.userManager.GetUserAsync(this.User);
            var userEmail = user.Email;
            await this.emailSender.SendEmailAsync("visitacity@visitacity.bg", "Visit a city", userEmail, viewModel.Name, html.ToString());
            return this.RedirectToAction(nameof(this.Details), new { id });
        }
    }
}
