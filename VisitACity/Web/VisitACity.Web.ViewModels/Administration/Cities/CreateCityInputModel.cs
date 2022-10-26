namespace VisitACity.Web.ViewModels.Administration.Cities
{
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class CreateCityInputModel : IMapTo<City>
    {
        public string Name { get; set; }

        public int CountryId { get; set; }

    }
}
