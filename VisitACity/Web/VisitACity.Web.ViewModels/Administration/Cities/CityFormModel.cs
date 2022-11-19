namespace VisitACity.Web.ViewModels.Administration.Cities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Countries;

    public class CityFormModel : IMapTo<City>
    {
        [Required]
        [StringLength(
            ModelConstants.City.NameMaxSize,
            MinimumLength = ModelConstants.City.NameMinSize,
            ErrorMessage = ModelConstants.NameLengthError)]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        public IEnumerable<CountryViewModel> Countries { get; set; }
    }
}
