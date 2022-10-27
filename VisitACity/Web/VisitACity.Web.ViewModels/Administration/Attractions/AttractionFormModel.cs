namespace VisitACity.Web.ViewModels.Administration.Attractions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Cities;

    public class AttractionFormModel : IMapFrom<Attraction>
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        [MaxLength(100)]
        public string Type { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be grater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        [Url]
        public string AttractionUrl { get; set; }

        [Required]
        [MaxLength(800)]
        public string Description { get; set; }

        public int CityId { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}
