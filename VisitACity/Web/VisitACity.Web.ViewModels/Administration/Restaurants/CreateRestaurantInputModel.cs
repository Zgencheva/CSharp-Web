namespace VisitACity.Web.ViewModels.Administration.Restaurants
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Cities;

    public class CreateRestaurantInputModel : IMapTo<Restaurant>
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        public int CityId { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}
