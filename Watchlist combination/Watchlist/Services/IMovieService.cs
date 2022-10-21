using Watchlist.Data.Entities;
using Watchlist.Models;

namespace Watchlist.Services
{
    public interface IMovieService
    {
        Task<ICollection<GenreInputModel>> GetGenresAsync();

        Task<ICollection<MovieViewModel>> GetAllAsync();

        Task AddMovieAsync(AddMovieViewModel model);

        Task<ICollection<MovieViewModel>> GetUserMoviesAsync(string userId);

        Task UpdateMovieAsync(int movieId, AddMovieViewModel model);

        Task<Movie> GetMovieAsync(int movieId);

        Task AddMovieToUserAsync(string userId, int movieId);

        Task RemovieMovieFromUser(string userId, int movieId);

    }
}
