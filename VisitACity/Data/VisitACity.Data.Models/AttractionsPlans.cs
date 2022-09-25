namespace VisitACity.Data.Models
{

    using VisitACity.Data.Common.Models;

    public class AttractionsPlans : BaseDeletableModel<int>
    {
        public int AttractionId { get; set; }

        public virtual Attraction Attraction { get; set; }

        public int PlanId { get; set; }

        public virtual Plan Plan { get; set; }
    }
}
