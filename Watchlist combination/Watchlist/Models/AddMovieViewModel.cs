using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class AddMovieViewModel
    {
        public AddMovieViewModel()
        {
            this.Genres = new List<GenreInputModel>();
        }
        [StringLength(50, MinimumLength = 10)]
        [Required]
        public string Title { get; set; }

        [StringLength(50, MinimumLength = 5)]
        [Required]
        public string Director { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), "0.0", "10.0")]
        public decimal Rating { get; set; }

        public int GenreId { get; set; }

        public ICollection<GenreInputModel> Genres { get; set; }
    }
}
