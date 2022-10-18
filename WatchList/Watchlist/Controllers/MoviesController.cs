using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Models;
using Watchlist.Models.Genres;
using Watchlist.Models.Movies;

namespace Watchlist.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly WatchlistDbContext context;

        public MoviesController(WatchlistDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddMovieModel();
            model.Genres = await context.Genres.Select(g => new GenreInputModel
            {
                Name = g.Name,
                Id = g.Id,
            })
                .ToListAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddMovieModel model)
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

            return RedirectToAction("All");
        }
        public async Task<IActionResult> All()
        {
            var model = new AllMoviesModel();
           model.Movies =  await this.context.Movies
                .Select(m => new MovieViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Director = m.Director,
                    ImageUrl = m.ImageUrl,
                    Rating = m.Rating,
                    Genre = m.Genre.Name,

                })
                .ToListAsync();

            return View(model);
        }

        public async Task<IActionResult> Watched()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allMovies = await this.context.Movies
                .Include(x=> x.Genre)
                .Include(m => m.UsersMovies)
                .ThenInclude(x => x.User)
                .ToListAsync();
            var userMovies = allMovies.SelectMany(x => x.UsersMovies.Where(u => u.UserId == userId)).ToList();
            var model = new AllMoviesModel();
            if (userMovies.Any())
            {
                model.Movies = userMovies.Select(x => new MovieViewModel()
                {
                    Id = x.Movie.Id,
                    Title = x.Movie.Title,
                    Director = x.Movie.Director,
                    ImageUrl = x.Movie.ImageUrl,
                    Rating = x.Movie.Rating,
                    Genre = x.Movie.Genre.Name == null ? null : x.Movie.Genre.Name,

                }).ToList();

            }
            

            return View(model);
        }

        public async Task<IActionResult> AddToCollection(int movieId)
        {
            var movie = this.context.Movies
                .Include(x => x.UsersMovies)
                .ThenInclude(x => x.User)
               .Where(x => x.Id == movieId);
               //.ToListAsync();
            var userId = this.GetUserId();
            if (movie.Any(x=>x.UsersMovies.Any(m=> m.UserId == userId)))
            {
                return RedirectToAction("All");
            }
            var userMovie = new UserMovie()
            {
                UserId = userId,
                MovieId = movieId
            };
            await this.context.UsersMovies.AddAsync(userMovie);
            await this.context.SaveChangesAsync();
            return RedirectToAction("All");
        }

        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            var userMovie = await this.context.UsersMovies.FirstAsync(x => x.MovieId == movieId);
            this.context.UsersMovies.Remove(userMovie);
            await this.context.SaveChangesAsync();
            return RedirectToAction("Watched");
        }
        public string GetUserId() 
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
