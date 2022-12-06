namespace VisitACity.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;

    public class ReviewController : AdministrationController
    {
        private readonly IReviewsService reviewService;

        public ReviewController(IReviewsService reviewService)
        {
            this.reviewService = reviewService;
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.reviewService.DeleteAsync(id);
            return this.Ok();
        }
    }
}
