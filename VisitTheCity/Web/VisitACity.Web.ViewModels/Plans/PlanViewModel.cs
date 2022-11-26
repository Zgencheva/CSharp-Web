﻿namespace VisitACity.Web.ViewModels.Plans
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VisitACity.Web.ViewModels.Attractions;
    using VisitACity.Web.ViewModels.Restaurants;

    public class PlanViewModel
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int CityId { get; set; }

        public int Days { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime FromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime ToDate { get; set; }

        public IEnumerable<AttractionViewModel> MyAttractions { get; set; }

        public IEnumerable<RestaurantViewModel> MyRestaurants { get; set; }
    }
}
