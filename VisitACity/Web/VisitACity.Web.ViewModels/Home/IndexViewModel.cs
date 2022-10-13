
namespace VisitACity.Web.ViewModels.Home
{
    using VisitACity.Web.ViewModels.Attractions;

    public class IndexViewModel
    {
        public int CitiesCount { get; set; }

        public int AttractionsCount { get; set; }

        public int RestaurantCount { get; set; }

        public AttractionsListViewModel AttractionList { get; set; }
    }
}
