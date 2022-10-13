namespace VisitACity.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReviewInputModel
    {
        [Range(1, 5)]
        [Required]
        public byte Rating { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
    }
}
