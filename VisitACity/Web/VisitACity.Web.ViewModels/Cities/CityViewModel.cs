namespace VisitACity.Web.ViewModels.Cities
{
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class CityViewModel : IMapFrom<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
