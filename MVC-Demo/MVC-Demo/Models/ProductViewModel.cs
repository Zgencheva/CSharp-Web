using System.ComponentModel.DataAnnotations;

namespace MVC_Demo.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public double Price { get; set; }
    }
}
