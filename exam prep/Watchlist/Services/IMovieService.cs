using System.Security.Claims;
using Watchlist.Models;

namespace Watchlist.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetAllAsync();
        Task<ICollection<GenreInputModel>> GetGenresAsync();
        Task AddAsync(AddMovieViewModel model);
        Task<IEnumerable<MovieViewModel>> GetWatchedMoviesAsync(string userId);
        Task AddToUserCollectionAsync(string userId, int movieId);
        Task RemoveMovieFromUserCollection(string userId, int movieId);
    }
}
