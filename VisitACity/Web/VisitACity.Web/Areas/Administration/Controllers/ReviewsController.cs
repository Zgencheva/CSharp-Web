﻿namespace VisitACity.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;

    public class ReviewsController : AdministrationController
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.reviewService.DeleteAsync(id);
            this.TempData["Message"] = $"Comment deleted successfully.";
            return this.Ok();
        }

        //public async Task<IActionResult> Delete(int id, int attractionId)
        //{
        //    await this.reviewService.DeleteAsync(id);
        //    this.TempData["Message"] = $"Comment deleted successfully.";
        //    return this.RedirectToAction("Details", "Attractions", new { area = "", id = attractionId });
        //}
    }
}
