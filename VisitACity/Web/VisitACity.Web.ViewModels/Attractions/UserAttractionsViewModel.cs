namespace VisitACity.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using VisitACity.Web.ViewModels.Attractions;

    public class UserAttractionsViewModel
    {
        public IEnumerable<AttractionViewModel> MyAttractions { get; set; }
    }
}
