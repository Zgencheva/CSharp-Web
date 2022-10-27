namespace VisitACity.Web.ViewModels.Administration.Cities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Administration.Countries;
    using VisitACity.Web.ViewModels.Countries;

    public class CityFormModel : IMapTo<City>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        public IEnumerable<CountryViewModel> Countries { get; set; }

    }
}
