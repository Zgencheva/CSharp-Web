namespace VisitACity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using VisitACity.Data.Common.Models;

    public class RestaurantReview : BaseDeletableModel<int>
    {
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public int ReviewId { get; set; }

        public virtual Review Review { get; set; }

    }
}
