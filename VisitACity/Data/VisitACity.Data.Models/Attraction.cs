namespace VisitACity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Common;
    using VisitACity.Data.Common.Models;
    using VisitACity.Data.Models.Enums;

    public class Attraction : BaseDeletableModel<int>
    {
        public Attraction()
        {
            this.UsersReviews = new HashSet<ApplicationUser>();
            this.Plans = new HashSet<Plan>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        [MaxLength(ModelConstants.Attraction.NameMaxSize)]
        public string Name { get; set; }

        [Required]
        public AttractionType Type { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = ModelConstants.PricePositiveNumber)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(ModelConstants.UrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(ModelConstants.Attraction.AddressMaxSize)]
        public string Address { get; set; }

        [MaxLength(ModelConstants.UrlMaxLength)]
        public string AttractionUrl { get; set; }

        [Required]
        [MaxLength(ModelConstants.Attraction.DescriptionMaxLength)]
        public string Description { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<ApplicationUser> UsersReviews { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
