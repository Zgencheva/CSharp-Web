namespace VisitACity.Data.Models
{
    using VisitACity.Data.Common.Models;

    public class RestaurantReview : BaseDeletableModel<int>
    {
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public int ReviewId { get; set; }

        public virtual Review Review { get; set; }
    }
}
