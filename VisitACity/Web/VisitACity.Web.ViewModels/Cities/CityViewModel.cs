using VisitACity.Services.Mapping;
using VisitACity.Data.Models;

namespace VisitACity.Web.ViewModels.Cities
{
    public class CityViewModel : IMapFrom<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
