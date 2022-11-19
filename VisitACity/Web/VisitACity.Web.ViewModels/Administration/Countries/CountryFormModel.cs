namespace VisitACity.Web.ViewModels.Administration.Countries
{
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class CountryFormModel : IMapTo<Country>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(
            ModelConstants.Country.NameMaxSize,
            MinimumLength = ModelConstants.Country.NameMinSize,
            ErrorMessage = ModelConstants.NameLengthError)]
        public string Name { get; set; }
    }
}
