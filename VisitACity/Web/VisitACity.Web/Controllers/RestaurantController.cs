namespace VisitACity.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Restaurants;

    public class RestaurantController : BaseController
    {
        private readonly IRestaurantsService restaurantsService;

        public RestaurantController(IRestaurantsService restaurantsService)
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
