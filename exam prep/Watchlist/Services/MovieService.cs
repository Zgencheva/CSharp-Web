using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Watchlist.Data;
using Watchlist.Data.Entities;
using Watchlist.Models;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;

        public MovieService(WatchlistDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(AddMovieViewModel model)
        {
            var movie = new Movie
            {
                Title = model.Title,
                Director = model.Director,
                Rating = model.Rating,
                GenreId = model.GenreId,
                ImageUrl = model.ImageUrl,
            };
            await this.context.Movies.AddAsync(movie);
            await this.context.SaveChangesAsync();
        }

        public async Task AddToUserCollectionAsync(string userId, int movieId)
        {
            var user = await this.context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.UsersMovies)
                .ThenInclude(x => x.Movie)
                .ThenInclude(x => x.Genre)
                .FirstOrDefaultAsync();
            var movie = await context.Movies.FirstOrDefaultAsync(u => u.Id == movieId);

            if (movie == null)
            {
                throw new ArgumentException("Invalid Movie ID");
            }
            try
            {
                user.UsersMovies.Add(new UserMovie()
                {
                    MovieId = movieId
                });
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
            
        }


        public async Task<IEnumerable<MovieViewModel>> GetAllAsync()
        {
            return await this.context.Movies
                .Include(m=> m.Genre)
                .Select(x => new MovieViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Director = x.Director,
                    Genre = x.Genre.Name == null ? null : x.Genre.Name,
                    Rating = x.Rating,
                    ImageUrl = x.ImageUrl,
                    
                })
                .ToListAsync();
        }

        public async Task<ICollection<GenreInputModel>> GetGenresAsync()
        {
            return await this.context.Genres.Select(g => new GenreInputModel
            {
                Name = g.Name,
                Id = g.Id
            })
            .ToListAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetWatchedMoviesAsync(string userId)
        {
            var user = await this.context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.UsersMovies)
                .ThenInclude(x => x.Movie)
                .ThenInclude(x => x.Genre)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentException("Ivalid user");
            }

            if (user.UsersMovies.Any())
            {
                return user.UsersMovies
                    .Select(x => new MovieViewModel()
                    {
                        Id = x.Movie.Id,
                        Title = x.Movie.Title,
                        Director = x.Movie.Director,
                        ImageUrl = x.Movie.ImageUrl,
                        Rating = x.Movie.Rating,
                        Genre = x.Movie.Genre.Name == null ? null : x.Movie.Genre.Name,
                    })
                    .ToList();
            }
            else
            {
                return new List<MovieViewModel>();

            }
           
        }

        public async Task RemoveMovieFromUserCollection(string userId, int movieId)
        {
            var user = await this.context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.UsersMovies)
                .ThenInclude(x => x.Movie)
                .ThenInclude(x => x.Genre)
                .FirstOrDefaultAsync();
            var movie = user.UsersMovies.FirstOrDefault(m => m.MovieId == movieId);

            if (movie == null)
            {
                throw new ArgumentException("Invalid Movie ID");
            }
            try
            {
                user.UsersMovies.Remove(movie);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }
    }
}
