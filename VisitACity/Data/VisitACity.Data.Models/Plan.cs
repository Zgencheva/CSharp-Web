using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitACity.Data.Common.Models;

namespace VisitACity.Data.Models
{
    public class Plan : BaseModel<int>, IDeletableEntity
    {
        public Plan()
        {
            this.Attractions = new HashSet<Attraction>();
            this.Restaurants = new HashSet<Restaurant>();
            this.Cities = new HashSet<City>();
        }

        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int Days { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<City> Cities { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }

        public virtual ICollection<Attraction> Attractions { get; set; }

    }
}
