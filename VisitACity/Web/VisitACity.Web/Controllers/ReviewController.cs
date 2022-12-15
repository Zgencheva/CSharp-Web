namespace VisitACity.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using VisitACity.Common;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Reviews;

    public class ReviewController : BaseController
    {
        private readonly IReviewsService reviewService;
        private readonly ILogger<ReviewController> logger;

        public ReviewController(
            IReviewsService reviewService,
            ILogger<ReviewController> logger)
        {
            this.reviewService = reviewService;
            this.logger = logger;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateReviewInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.reviewService.AddReviewToRestaurantAsync(input, userId, id);
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(ExceptionMessages.DbFailedUponAddingReview, ex);
                this.ModelState.AddModelError(string.Empty, ExceptionMessages.DbException);
                return this.View(input);
            }

            this.TempData["Message"] = TempDataMessageConstants.ThankForComment;
            return this.RedirectToAction("Details", "Restaurant", new { id });
        }
    }
}
