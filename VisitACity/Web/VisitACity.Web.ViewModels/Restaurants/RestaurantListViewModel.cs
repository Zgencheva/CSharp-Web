namespace VisitACity.Web.ViewModels.Restaurants
{
    using System.Collections.Generic;

    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class RestaurantListViewModel : PagingViewModel, IMapFrom<Restaurant>
    {
        public IEnumerable<RestaurantViewModel> Restaurants { get; set; } = new List<RestaurantViewModel>();
    }
}
