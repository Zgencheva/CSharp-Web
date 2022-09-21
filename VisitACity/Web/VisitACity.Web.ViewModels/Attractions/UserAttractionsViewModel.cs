using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitACity.Web.ViewModels.Attractions
{
    public class UserAttractionsViewModel
    {
        public IEnumerable<AttractionViewModel> MyAttractions { get; set; }
    }
}
