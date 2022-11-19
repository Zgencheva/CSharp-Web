namespace VisitACity.Web.ViewModels.Administration.Restaurants
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Cities;

    public class RestaurantFromModel : IMapFrom<Restaurant>
    {
        [Required]
        [StringLength(
            ModelConstants.Restaurant.NameMaxSize,
            MinimumLength = ModelConstants.Restaurant.NameMinSize,
            ErrorMessage = ModelConstants.NameLengthError)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            ModelConstants.Restaurant.PhoneMaxLength,
            MinimumLength = ModelConstants.Restaurant.PhoneMinLength,
            ErrorMessage = ModelConstants.PhoneLengthError)]
        public string PhoneNumber { get; set; }

        [Url]
        [Required]
        [MaxLength(ModelConstants.UrlMaxLength)]
        public string Url { get; set; }

        [Required]
        [Url]
        [MaxLength(ModelConstants.UrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(
            ModelConstants.Restaurant.AddressMaxSize,
            MinimumLength = ModelConstants.Restaurant.AddressMinSize,
            ErrorMessage = ModelConstants.AddressLengthError)]
        public string Address { get; set; }

        [Required]
        public int CityId { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}
