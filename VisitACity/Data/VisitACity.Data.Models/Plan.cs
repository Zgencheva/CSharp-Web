namespace VisitACity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using VisitACity.Data.Common.Models;

    public class Plan : BaseDeletableModel<int>
    {
        public Plan()
        {
            this.Attractions = new HashSet<Attraction>();
            this.Restaurants = new HashSet<Restaurant>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }

        public virtual ICollection<Attraction> Attractions { get; set; }

    }
}
