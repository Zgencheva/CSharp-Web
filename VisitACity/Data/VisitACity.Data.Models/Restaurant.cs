namespace VisitACity.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Data.Common.Models;

    public class Restaurant : BaseDeletableModel<int>
    {
        public Restaurant()
        {
            this.Reviews = new HashSet<RestaurantReview>();
            this.Plans = new HashSet<Plan>();
        }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Address { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<RestaurantReview> Reviews { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }
    }

}