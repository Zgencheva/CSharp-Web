using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Watchlist.Data.Entities
{
    public class Movie
    {
        public Movie()
        {
            this.UsersMovies = new HashSet<UserMovie>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Director { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Rating { get; set; }

        public virtual int? GenreId { get; set; }
        [ForeignKey(nameof(GenreId))]
        public virtual Genre? Genre { get; set; }

        public virtual ICollection<UserMovie> UsersMovies { get; set; }
    }
}
