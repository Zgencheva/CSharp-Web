using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitACity.Data.Models
{
    public class AttractionReview : Review
    {
        public int AttractionId { get; set; }

        public virtual Attraction Attraction { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
