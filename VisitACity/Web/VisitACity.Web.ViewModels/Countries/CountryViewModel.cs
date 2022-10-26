namespace VisitACity.Web.ViewModels.Countries
{
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class CountryViewModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
