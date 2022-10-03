namespace VisitACity.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

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

        public IActionResult Index(string cityName, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            };
            const int ItemsPerPage = 6;
            var viewModel = new IndexViewModel
            {
                CitiesCount = this.cityService.GetCitiesCount(),
                AttractionsCount = this.attractionsService.GetAttractionsCount(),
                RestaurantCount = this.restaurantsService.GetRestaurantsCount(),
            };

            if (cityName == null)
            {
                viewModel.AttractionList = new AttractionsListViewModel
                {
                    Attractions = this.attractionsService.GetBestAttractions(id, ItemsPerPage),
                    PageNumber = id,
                    AttractionsCount = this.attractionsService.GetAttractionsCount(),
                    ItemsPerPage = ItemsPerPage,
                };
            }
            else
            {
                viewModel.AttractionList = new AttractionsListViewModel
                {
                    Attractions = this.attractionsService.GetAttractionsByCity(cityName, id, ItemsPerPage),
                    PageNumber = id,
                    AttractionsCount = this.attractionsService.GetAttractionsCount(),
                    ItemsPerPage = ItemsPerPage,
                };
            }

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
