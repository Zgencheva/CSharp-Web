﻿using System.ComponentModel.DataAnnotations;
using Watchlist.Models.Genres;

namespace Watchlist.Models.Movies
{
    public class AddMovieModel
    {
        public AddMovieModel()
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
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), "0.0", "10.0")]
        public decimal Rating { get; set; }

        public int? GenreId { get; set; }

        public ICollection<GenreInputModel> Genres { get; set; }
    }
}
