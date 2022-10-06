using System.ComponentModel.DataAnnotations;

namespace VisitACity.Web.ViewModels.Reviews
{
    public class CreateReviewInputModel
    {
        [Range(1, 5)]
        [Required]
        public byte Rating { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
