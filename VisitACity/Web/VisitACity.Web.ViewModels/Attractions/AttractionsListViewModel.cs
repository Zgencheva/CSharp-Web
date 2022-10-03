using System.Collections.Generic;
using VisitACity.Data.Models;
using VisitACity.Services.Mapping;

namespace VisitACity.Web.ViewModels.Attractions
{
    public class AttractionsListViewModel : PagingViewModel, IMapFrom<Attraction>
    {
        public IEnumerable<AttractionViewModel> Attractions { get; set; }
    }
}
