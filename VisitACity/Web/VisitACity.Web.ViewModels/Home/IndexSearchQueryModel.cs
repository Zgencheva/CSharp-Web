namespace VisitACity.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    public class IndexSearchQueryModel
    {
        [Required]
        [MaxLength(50)]
        public string CityName { get; set; }

        [Required]
        public string RadioOption { get; set; }
    }
}
