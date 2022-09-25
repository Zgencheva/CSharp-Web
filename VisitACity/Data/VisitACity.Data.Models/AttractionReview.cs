namespace VisitACity.Data.Models
{

    using VisitACity.Data.Common.Models;

    public class AttractionReview : BaseDeletableModel<int>
    {
        public int AttractionId { get; set; }

        public virtual Attraction Attraction { get; set; }

        public int ReviewId { get; set; }

        public virtual Review Review { get; set; }
    }
}
