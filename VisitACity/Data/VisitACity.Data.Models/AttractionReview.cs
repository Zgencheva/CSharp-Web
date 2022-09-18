namespace VisitACity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using VisitACity.Data.Common.Models;

    public class AttractionReview : BaseDeletableModel<int>
    {
        public int AttractionId { get; set; }

        public virtual Attraction Attraction { get; set; }

        public int ReviewId { get; set; }

        public virtual Review Review { get; set; }
    }
}
