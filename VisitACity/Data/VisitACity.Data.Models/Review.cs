﻿namespace VisitACity.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {

        public Review()
        {
            this.RestaurantReviews = new HashSet<RestaurantReview>();
            this.AttractionReviews = new HashSet<AttractionReview>();
        }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public double Rating { get; set; }

        public string Content { get; set; }

        public virtual ICollection<RestaurantReview> RestaurantReviews { get; set; }

        public virtual ICollection<AttractionReview> AttractionReviews { get; set; }
    }
}