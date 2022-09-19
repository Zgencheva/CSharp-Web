namespace VisitACity.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Web.ViewModels;
    using VisitACity.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                CitiesCount = 0,
                AttractionsCount = 0,
                RestaurantCount = 0,
                BestAttractions = null,
            };
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
