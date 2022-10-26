namespace VisitACity.Web.ViewModels.Administration.Cities
{
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class CreateCityInputModel : IMapTo<City>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

    }
}
