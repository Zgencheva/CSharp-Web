namespace VisitACity.Web.ViewModels.Administration.Attractions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.Attributes;
    using VisitACity.Web.Infrastructure.ValidationAttributes;
    using VisitACity.Web.ViewModels.Cities;

    public class AttractionFormModel : IMapFrom<Attraction>
    {
        [Required]
        [StringLength(
            ModelConstants.Attraction.NameMaxSize,
            MinimumLength = ModelConstants.Attraction.NameMinSize,
            ErrorMessage = ModelConstants.NameLengthError)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        [MaxLength(ModelConstants.Attraction.TypeMaxLength)]
        public string Type { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = ModelConstants.PricePositiveNumber)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Image")]
        [AllowedExtensions(
            new string[]
            {
                GlobalConstants.JpegFormat,
                GlobalConstants.JpgFormat,
                GlobalConstants.PngFormat,
            })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile ImageToBlob { get; set; }

        [Required]
        [StringLength(
            ModelConstants.Attraction.AddressMaxSize,
            MinimumLength = ModelConstants.Attraction.AddressMinSize,
            ErrorMessage = ModelConstants.AddressLengthError)]
        public string Address { get; set; }

        [Url]
        [MaxLength(ModelConstants.UrlMaxLength)]
        public string AttractionUrl { get; set; }

        [Required]
        [StringLength(
            ModelConstants.Attraction.DescriptionMaxLength,
            MinimumLength = ModelConstants.Attraction.DescriptionMinLength,
            ErrorMessage = ModelConstants.DescriptionLengthError)]
        public string Description { get; set; }

        public int CityId { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}
