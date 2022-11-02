using System.ComponentModel.DataAnnotations;

namespace VisitACity.Web.ViewModels.Home
{
    public class IndexSearchQueryModel
    {
        [Required]
        [MaxLength(50)]
        public string CityName { get; set; }

        [Required]
        public string RadioOption { get; set; }
    }
}
