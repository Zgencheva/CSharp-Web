namespace VisitACity.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Restaurants;

    public class RestaurantsController : BaseController
    {
        private readonly IRestaurantsService restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsService)
        {
            this.restaurantsService = restaurantsService;
        }

        public async Task<IActionResult> Details(int id)
        {
           var viewModel = await this.restaurantsService.GetViewModelByIdAsync<RestaurantViewModel>(id);

           return this.View(viewModel);
        }
    }
}
