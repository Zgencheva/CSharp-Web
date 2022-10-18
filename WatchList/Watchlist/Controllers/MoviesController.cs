using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Models;

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
            return Ok();
        }

        public async Task<IActionResult> AddToCollection(int movieId)
        {
            return Ok();
        }

        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            return Ok();
        }
    }
}
