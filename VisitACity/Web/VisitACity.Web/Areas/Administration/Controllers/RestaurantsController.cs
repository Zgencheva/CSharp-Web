using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VisitACity.Services.Data.Contracts;
using VisitACity.Web.ViewModels.Administration.Restaurants;

namespace VisitACity.Web.Areas.Administration.Controllers
{
    public class RestaurantsController : AdministrationController
    {
        private readonly IRestaurantsService restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsService)
        {
            this.restaurantsService = restaurantsService;
        }

        public IActionResult Create()
        {
            var model = new CreateRestaurantInputModel();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRestaurantInputModel model)
        {
            await this.restaurantsService.CreateAsync(model);
            this.TempData["Message"] = "Restaurant added successfully.";
            return Ok();
        }
    }
}
