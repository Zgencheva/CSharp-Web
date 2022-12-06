namespace VisitACity.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    using VisitACity.Common;

    public class IndexSearchQueryModel
    {
        [Required]
        [StringLength(
            ModelConstants.City.NameMaxSize,
            MinimumLength = ModelConstants.City.NameMinSize,
            ErrorMessage = ModelConstants.NameLengthError)]
        public string CityName { get; set; }

        [Required]
        public string RadioOption { get; set; }
    }
}
