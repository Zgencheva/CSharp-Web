using System.ComponentModel.DataAnnotations;

namespace VisitACity.Web.ViewModels.Reviews
{
    public class ReviewServiceModel
    {
        public string UserId { get; set; }

        [Range(1, 5)]
        public byte Rating { get; set; }

        public string Content { get; set; }

        public int AttractionId { get; set; }
    }
}
