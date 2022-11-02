using VisitACity.Services.Data.Contracts;

namespace VisitACity.Web.Controllers
{
    public class RestaurantsController : BaseController
    {
        private readonly IRestaurantsService restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsService)
        {
            this.restaurantsService = restaurantsService;
        }
    }
}
