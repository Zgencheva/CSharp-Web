namespace VisitACity.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using VisitACity.Common;
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
        private readonly ILogger<HomeController> logger;

        public HomeController(
            ICitiesService cityService,
            IAttractionsService attractionsService,
            IRestaurantsService restaurantsService,
            ILogger<HomeController> logger)
        {
            this.cityService = cityService;
            this.attractionsService = attractionsService;
            this.restaurantsService = restaurantsService;
            this.logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery] IndexSearchQueryModel query, int id = ModelConstants.DefaultPageNumber)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = ModelConstants.DefaultPageSize;
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
                if (query.RadioOption == ModelConstants.RestaurantsRadioOption)
                {
                    viewModel.IsAttraction = false;
                    viewModel.EventsCount = this.restaurantsService.GetCountByCity(query.CityName);
                    viewModel.List = await this.restaurantsService.GetByCityAsync<RestaurantViewModel>(query.CityName, id, ItemsPerPage);
                    viewModel.queryModel = query;
                }
                else if (query.RadioOption == ModelConstants.AttractionsRadioOption)
                {
                    viewModel.IsAttraction = true;
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
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            this.logger.LogError(feature.Error, "TraceIdentifier: {0}", Activity.Current?.Id ?? this.HttpContext.TraceIdentifier);

            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
