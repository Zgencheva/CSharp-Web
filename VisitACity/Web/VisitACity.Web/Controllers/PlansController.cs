using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VisitACity.Web.ViewModels.Plans;

namespace VisitACity.Web.Controllers
{
    public class PlansController : BaseController
    {
        public IActionResult MyPlans()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> Create()
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
                return this.View();
            }
            return this.RedirectToAction(nameof(MyPlans));
        }
    }
}
