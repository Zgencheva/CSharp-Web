using System.Collections.Generic;

namespace VisitACity.Web.ViewModels.Attractions
{
    public class AttractionsListViewModel : PagingViewModel
    {
        public IEnumerable<AttractionViewModel> Attractions { get; set; }
    }
}
