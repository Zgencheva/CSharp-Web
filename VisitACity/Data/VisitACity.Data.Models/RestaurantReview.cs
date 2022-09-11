using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitACity.Data.Models
{
    public class RestaurantReview : Review
    {
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
