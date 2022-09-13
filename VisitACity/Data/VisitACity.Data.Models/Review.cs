using System;
using System.Collections.Generic;
using VisitACity.Data.Common.Models;

namespace VisitACity.Data.Models
{
    public class Review : BaseDeletableModel<int>
    {

        public Review()
        {
            this.RestaurantReviews = new HashSet<RestaurantReview>();
            this.AttractionReviews = new HashSet<AttractionReview>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public double Rating { get; set; }

        public string Content { get; set; }

        public virtual ICollection<RestaurantReview> RestaurantReviews { get; set; }

        public virtual ICollection<AttractionReview> AttractionReviews { get; set; }
    }
}