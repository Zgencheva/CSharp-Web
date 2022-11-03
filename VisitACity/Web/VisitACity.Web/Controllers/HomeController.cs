namespace VisitACity.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels;
    using VisitACity.Web.ViewModels.Attractions;
    using VisitACity.Web.ViewModels.Home;
    using VisitACity.Web.ViewModels.Restaurants;

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
        public async Task<IActionResult> Index([FromQuery]IndexSearchQueryModel query, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 6;
            var viewModel = new IndexViewModel
            {
                CitiesCount = this.cityService.GetCount(),
                AttractionsCount = this.attractionsService.GetCount(),
                RestaurantCount = this.restaurantsService.GetCount(),
                PageNumber = id,
                ItemsPerPage = ItemsPerPage,
            };
            if (query.CityName == null)
            {
                viewModel.EventsCount = this.attractionsService.GetCount();
                viewModel.List = await this.attractionsService.GetBestAttractionsAsync<AttractionViewModel>(id, ItemsPerPage);
            }
            else
            {
                if (query.RadioOption == "Restaurants")
                {
                    viewModel.IsAttraction = false;
                    viewModel.EventsCount = this.restaurantsService.GetCountByCity(query.CityName);
                    viewModel.List = await this.restaurantsService.GetByCityAsync<RestaurantViewModel>(query.CityName, id, ItemsPerPage);
                    viewModel.queryModel = query;
                }
                else if (query.RadioOption == "Attractions")
                {
                    viewModel.List = await this.attractionsService.GetByCityAsync<AttractionViewModel>(query.CityName, id, ItemsPerPage);
                    viewModel.EventsCount = this.attractionsService.GetCountByCity(query.CityName);
                    viewModel.queryModel = query;
                }
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
