namespace VisitACity.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VisitACity.Common;
    using VisitACity.Data.Common.Models;

    public class Restaurant : BaseDeletableModel<int>
    {
        public Restaurant()
        {
            this.Reviews = new HashSet<Review>();
            this.Plans = new HashSet<Plan>();
        }

        [Required]
        [MaxLength(ModelConstants.Restaurant.NameMaxSize)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ModelConstants.UrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(ModelConstants.Restaurant.AddressMaxSize)]
        public string Address { get; set; }

        [Required]
        [MaxLength(ModelConstants.UrlMaxLength)]
        public string Url { get; set; }

        [Required]
        [MaxLength(ModelConstants.Restaurant.PhoneMaxLength)]
        public string PhoneNumber { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }
    }
}
