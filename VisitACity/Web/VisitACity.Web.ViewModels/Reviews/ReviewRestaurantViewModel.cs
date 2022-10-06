namespace VisitACity.Web.ViewModels.Reviews
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class ReviewRestaurantViewModel : IMapFrom<Review>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Range(1, 5)]
        public byte Rating { get; set; }

        public string Content { get; set; }

        public int RestaurantId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
