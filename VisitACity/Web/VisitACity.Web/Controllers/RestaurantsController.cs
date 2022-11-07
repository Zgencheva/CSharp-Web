using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web.Mvc;
using VisitACity.Services.Data.Contracts;
using VisitACity.Web.ViewModels.Restaurants;

namespace VisitACity.Web.Controllers
{
    public class RestaurantsController : BaseController
    {
        private readonly IRestaurantsService restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsService)
        {
            this.restaurantsService = restaurantsService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.restaurantsService.GetViewModelByIdAsync<RestaurantViewModel>(id);
            return this.View(viewModel);
        }
    }
}
