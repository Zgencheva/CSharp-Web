using System;
using System.Collections.Generic;
using VisitACity.Data.Common.Models;

namespace VisitACity.Data.Models
{
    public class Review : BaseModel<int>, IDeletableEntity
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

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set ; }

        public virtual ICollection<RestaurantReview> RestaurantReviews { get; set; }

        public virtual ICollection<AttractionReview> AttractionReviews { get; set; }
    }
}