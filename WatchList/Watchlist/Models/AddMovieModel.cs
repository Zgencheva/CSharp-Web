using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class AddMovieModel
    {
        [StringLength(50, MinimumLength = 10)]
        [Required]
        public string Title { get; set; }

        [StringLength(50, MinimumLength = 5)]
        [Required]
        public string Director { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), "0.0", "10.00")]
        public decimal Rating { get; set; }

        public int? GenreId { get; set; }

        public ICollection<GenreInputModel> Genres { get; set; }
    }
}
