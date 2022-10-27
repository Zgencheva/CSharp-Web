namespace VisitACity.Web.ViewModels.Administration.Countries
{
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class CountryFormModel : IMapTo<Country>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
