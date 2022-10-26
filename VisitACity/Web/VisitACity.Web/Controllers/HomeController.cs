namespace VisitACity.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels;
    using VisitACity.Web.ViewModels.Attractions;
    using VisitACity.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ICitiesService cityService;
        private readonly IAttractionsService attractionsService;
        private readonly IRestaurantsService restaurantsService;

        public HomeController(
            ICitiesService cityService,
            IAttractionsService attractionsService,
            IRestaurantsService restaurantsService)
        {
            this.cityService = cityService;
            this.attractionsService = attractionsService;
            this.restaurantsService = restaurantsService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string cityName, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            };
            const int ItemsPerPage = 6;
            var viewModel = new IndexViewModel
            {
                CitiesCount = this.cityService.GetCount(),
                AttractionsCount = this.attractionsService.GetCount(),
                RestaurantCount = this.restaurantsService.GetCount(),
            };

            if (cityName == null)
            {
                viewModel.AttractionList = new AttractionsListViewModel
                {
                    Attractions = await this.attractionsService.GetBestAttractionsAsync(id, ItemsPerPage),
                    PageNumber = id,
                    AttractionsCount = this.attractionsService.GetCount(),
                    ItemsPerPage = ItemsPerPage,
                };
            }
            else
            {
                viewModel.AttractionList = new AttractionsListViewModel
                {
                    Attractions = await this.attractionsService.GetByCityAsync(cityName, id, ItemsPerPage),
                    PageNumber = id,
                    AttractionsCount = this.attractionsService.GetCount(),
                    ItemsPerPage = ItemsPerPage,
                };
            }

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
