﻿
namespace VisitACity.Web.ViewModels.Home
{
    using System.Collections;
    using VisitACity.Web.ViewModels.Restaurants;

    public class IndexViewModel : PagingViewModel
    {
        public int CitiesCount { get; set; }

        public int AttractionsCount { get; set; }

        public int RestaurantCount { get; set; }

        public IEnumerable List { get; set; }

        public bool IsAttraction { get; set; } = true;

    }
}
