namespace VisitACity.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VisitACity.Common;
    using VisitACity.Data.Common.Models;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            this.Attractions = new HashSet<Attraction>();
            this.Restaurants = new HashSet<Restaurant>();
        }

        [Required]
        [MaxLength(ModelConstants.City.NameMaxSize)]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Attraction> Attractions { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }
    }
}
