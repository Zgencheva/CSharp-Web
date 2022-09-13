using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitACity.Data.Common.Models;

namespace VisitACity.Data.Models
{
    public class AttractionReview : BaseModel<int>, IDeletableEntity
    {
        public int AttractionId { get; set; }

        public virtual Attraction Attraction { get; set; }

        public string ReviewId { get; set; }

        public virtual Review Review { get; set; }

        public bool IsDeleted { get ; set; }

        public DateTime? DeletedOn { get ; set ; }

    }
}
