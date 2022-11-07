namespace VisitACity.Web.ViewModels.Administration.Restaurants
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Cities;

    public class RestaurantFromModel : IMapFrom<Restaurant>
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Url]
        [Required]
        public string Url { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        [Required]
        public int CityId { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}
