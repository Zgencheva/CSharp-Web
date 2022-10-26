using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitACity.Data.Models;
using VisitACity.Services.Mapping;

namespace VisitACity.Web.ViewModels.Administration.Attractions
{
    public class CreateAttractionInputModel : IMapTo<Attraction>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int Type { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be grater than 0")]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Address { get; set; }

        [Url]
        public string AttractionUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public int CityId { get; set; }

        public IEnumerable<City> Cities { get; set; }
    }
}
