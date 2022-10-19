using System.ComponentModel.DataAnnotations;

namespace Watchlist.Data.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
