namespace VisitACity.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using VisitACity.Data.Models;

    public class IndexViewModel
    {
        public int CitiesCount { get; set; }

        public int AttractionsCount { get; set; }

        public int RestaurantCount { get; set; }

        public IEnumerable<AttractionViewModel> BestAttractions { get; set; }
    }
}
