namespace VisitACity.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels;
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

        public IActionResult Index(string cityName)
        {
            var viewModel = new IndexViewModel
            {
                CitiesCount = this.cityService.GetCitiesCount(),
                AttractionsCount = this.attractionsService.GetAttractionsCount(),
                RestaurantCount = this.restaurantsService.GetRestaurantsCount(),
            };

            if (cityName == null)
            {

                viewModel.AttractionList = this.attractionsService.GetBestAttractions()
                .Select(x => new AttractionViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Type = x.Type.ToString(),
                });
            }
            else
            {
                viewModel.AttractionList = this.attractionsService.GetAttractionsByCity(cityName)
                     .Select(x => new AttractionViewModel
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Description = x.Description,
                         ImageUrl = x.ImageUrl,
                         Type = x.Type.ToString(),
                     });
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
