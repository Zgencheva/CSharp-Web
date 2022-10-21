using Microsoft.EntityFrameworkCore;
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

        public async Task AddMovieAsync(AddMovieViewModel model)
        {
            var movie = new Movie()
            {
                Title = model.Title,
                Director = model.Director,
                Rating = model.Rating,
                ImageUrl = model.ImageUrl,
                GenreId = model.GenreId,
            };

            await this.context.Movies.AddAsync(movie);
            await this.context.SaveChangesAsync();
        }

        public async Task AddMovieToUserAsync(string userId, int movieId)
        {
            var user = await this.context
                .Users
                .Where(x => x.Id == userId)
                .Include(u => u.UsersMovies)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentException("Invalid movie Id");
            }
            var movie = await this.context.Movies
               .FirstOrDefaultAsync(x => x.Id == movieId);
            //.ToListAsync();
            if (movie == null)
            {
                throw new ArgumentException("Invalid movie Id");
            }
            if (user.UsersMovies.Any(x => x.MovieId == movieId))
            {
                return;
            }
            user.UsersMovies.Add(new UserMovie()
            {
                MovieId = movieId
            });
            await this.context.SaveChangesAsync();
        }

        public async Task<ICollection<MovieViewModel>> GetAllAsync()
        {

            return await this.context
                .Movies
                .Include(x => x.Genre)
                .Select(m => new MovieViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Director = m.Director,
                    ImageUrl = m.ImageUrl,
                    Rating = m.Rating,
                    Genre = m.Genre.Name
                })
                .ToListAsync();
        }

        public async Task<ICollection<GenreInputModel>> GetGenresAsync()
        {
            return await this.context.Genres
                .Select(g=> new GenreInputModel
                {
                    Name = g.Name,
                    Id = g.Id,
                })
                .ToListAsync();
        }

        public async Task<Movie> GetMovieAsync(int movieId)
        {
            return await this.context.Movies.FindAsync(movieId);
        }

        public async Task<ICollection<MovieViewModel>> GetUserMoviesAsync(string userId)
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
            var model = new List<MovieViewModel>();
            if (user.UsersMovies.Any())
            {
                model = user.UsersMovies
                    .Select(x => new MovieViewModel()
                    {
                        Id = x.Movie.Id,
                        Title = x.Movie.Title,
                        Director = x.Movie.Director,
                        ImageUrl = x.Movie.ImageUrl,
                        Rating = x.Movie.Rating,
                        Genre = x.Movie.Genre.Name,

                    }).ToList();
            }
            return model;
        }

        public async Task RemovieMovieFromUser(string userId, int movieId)
        {
            var user = await this.context.Users
                .Include(x=> x.UsersMovies)
                .ThenInclude(x=>x.Movie)
                .FirstOrDefaultAsync(x=> x.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("Invalid user");
            }
            if (!user.UsersMovies.Any(x=> x.Movie.Id == movieId))
            {
                throw new ArgumentException("Invalid operation");

            }
            var userMovie = user.UsersMovies.FirstOrDefault(x => x.Movie.Id == movieId);
            user.UsersMovies.Remove(userMovie);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(int movieId, AddMovieViewModel model)
        {
            var movie = await this.context.Movies.FindAsync(movieId);
            movie.Rating = model.Rating;
            movie.Title = model.Title;
            movie.Director = model.Director;
            movie.ImageUrl = model.ImageUrl;
            movie.GenreId = model.GenreId;

            await this.context.SaveChangesAsync();
               
        }
    }
}
