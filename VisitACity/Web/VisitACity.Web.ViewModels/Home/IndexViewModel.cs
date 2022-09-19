namespace VisitACity.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using VisitACity.Data.Models;

    public class IndexViewModel
    {
        public int CitiesCount { get; set; }

        public int AttractionsCount { get; set; }

        public int RestaurantCount { get; set; }

        public ICollection<Attraction> BestAttractions { get; set; }
    }
}
