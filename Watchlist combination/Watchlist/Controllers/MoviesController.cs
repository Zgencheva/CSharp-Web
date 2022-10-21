using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Watchlist.Data;
using Watchlist.Models;
using Watchlist.Services;

namespace Watchlist.Controllers
{
    public class MoviesController : Controller
    {
        private readonly WatchlistDbContext context;
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        public async Task<IActionResult> All()
        {
            ICollection<MovieViewModel> model = await this.movieService.GetAllAsync();
            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddMovieViewModel();
            model.Genres = await this.movieService.GetGenresAsync();
            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await this.movieService.GetGenresAsync();
                return this.View(model);
            }
            try
            {
                await this.movieService.AddMovieAsync(model);
            }
            catch (Exception ex)
            {

                this.ModelState.AddModelError(String.Empty,
                    ex.Message);
                model.Genres = await this.movieService.GetGenresAsync();
                return this.View(model);
            }
           
            return RedirectToAction(nameof(All));
        }
        public async Task<IActionResult> Watched()
        {
            var userId = this.GetUserId();
            ICollection<MovieViewModel> model = await this.movieService.GetUserMoviesAsync(userId);
            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int movieId)
        {
            try
            {
                var movie = await this.movieService.GetMovieAsync(movieId);
                var model = new AddMovieViewModel
                {

                    Title = movie.Title,
                    Director = movie.Director,
                    ImageUrl = movie.ImageUrl,
                    Rating = movie.Rating,
                    GenreId = movie.GenreId,
                    Genres = await this.movieService.GetGenresAsync(),
                };
                return this.View(model);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
                
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int movieId, AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await this.movieService.GetGenresAsync();
                return this.View(model);
            }
            try
            {
                await this.movieService.UpdateMovieAsync(movieId, model);
            }
            catch (Exception ex)
            {

                this.ModelState.AddModelError(String.Empty,
                    ex.Message);
                model.Genres = await this.movieService.GetGenresAsync();
                return this.View(model);
            }

            return RedirectToAction(nameof(All));
        }
        [HttpPost]
        public async Task<IActionResult> AddToCollection(int movieId) 
        {
            var userId = this.GetUserId();
            try
            {
                await this.movieService.AddMovieToUserAsync(userId, movieId);
                return this.RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);                
            }
        }

        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            var userId = this.GetUserId();
            try
            {
                await this.movieService.RemovieMovieFromUser(userId, movieId);
                return this.RedirectToAction(nameof(Watched));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        private string GetUserId()
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        }
    }
}
