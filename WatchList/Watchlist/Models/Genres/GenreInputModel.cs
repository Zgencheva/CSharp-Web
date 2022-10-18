using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models.Genres
{
    public class GenreInputModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

    }
}