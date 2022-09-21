using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitACity.Web.ViewModels.Attractions;
using VisitACity.Web.ViewModels.Restaurants;

namespace VisitACity.Web.ViewModels.Plans
{
    public class MyPlansViewModel
    {
        public string Country { get; set; }

        public string City { get; set; }

        public int Days { get; set; }

        public ICollection<MyAttractionsViewModel> MyAttractions { get; set; }

        public ICollection<MyRestaurantsViewModel> MyRestaurants { get; set; }
    }
}
