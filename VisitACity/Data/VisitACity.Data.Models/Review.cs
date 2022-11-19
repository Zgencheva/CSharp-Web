namespace VisitACity.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Common;
    using VisitACity.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Rating { get; set; }

        [MaxLength(ModelConstants.Review.CommentMaxLength)]
        [Required]
        public string Content { get; set; }

        public int? RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
