namespace VisitACity.Web.ViewModels.Administration.Countries
{
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class CreateCountryInputModel : IMapTo<Country>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
