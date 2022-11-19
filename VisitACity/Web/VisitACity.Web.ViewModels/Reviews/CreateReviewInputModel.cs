namespace VisitACity.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    using VisitACity.Common;

    public class CreateReviewInputModel
    {
        [Range(ModelConstants.Review.RateMin, ModelConstants.Review.RateMax)]
        [Required]
        public byte Rating { get; set; }

        [Required]
        [StringLength(
            ModelConstants.Review.CommentMaxLength,
            MinimumLength = ModelConstants.Review.CommentMinLength,
            ErrorMessage = ModelConstants.Review.CommentLengthError)]
        public string Content { get; set; }
    }
}
