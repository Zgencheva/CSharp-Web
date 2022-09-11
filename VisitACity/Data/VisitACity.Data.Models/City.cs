using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitACity.Data.Common.Models;

namespace VisitACity.Data.Models
{
    public class City : BaseModel<int>, IDeletableEntity
    {
        public City()
        {
            this.Attractions = new HashSet<Attraction>();
            this.Restaurants = new HashSet<Restaurant>();
        }

        public string Name { get; set; }

        public virtual ICollection<Attraction> Attractions { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }

        public bool IsDeleted { get; set ; }

        public DateTime? DeletedOn { get; set ; }
    }
}
