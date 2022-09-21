namespace VisitACity.Web.ViewModels.Plans
{
    using System;
    using System.Collections.Generic;

    using VisitACity.Web.ViewModels.Attractions;
    using VisitACity.Web.ViewModels.Restaurants;

    public class PlanViewModel
    {
        public string Country { get; set; }

        public string City { get; set; }

        public int Days { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public UserAttractionsViewModel MyAttractions { get; set; }

        public UserRestaurantsViewModel MyRestaurants { get; set; }
    }
}
