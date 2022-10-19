using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Watchlist.Models;
using Watchlist.Services;

namespace Watchlist.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.movieService.GetAllAsync();

            return this.View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddMovieViewModel model = new AddMovieViewModel();
            model.Genres = await this.movieService.GetGenresAsync();
            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Genres = await this.movieService.GetGenresAsync();
                return this.View(model);
            }
            try
            {
                await this.movieService.AddAsync(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(String.Empty, ex.Message);
                model.Genres = await this.movieService.GetGenresAsync();
                return this.View(model);
            }
           
        }

        public async Task<IActionResult> Watched()
        {
            var userId = this.GetUserId();
            var model = await this.movieService.GetWatchedMoviesAsync(userId);

            return this.View(model);
        }

        public async Task<IActionResult> AddToCollection(int movieId)
        {
            var userId = this.GetUserId();
            var userMvoies = await this.movieService.GetWatchedMoviesAsync(userId);
            if (!userMvoies.Any(x=> x.Id == movieId))
            {
                await this.movieService.AddToUserCollectionAsync(userId, movieId);
            }
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            var userId = this.GetUserId();
            var userMvoies = await this.movieService.GetWatchedMoviesAsync(userId);
            if (userMvoies.Any(x=> x.Id == movieId))
            {
                await this.movieService.RemoveMovieFromUserCollection(userId, movieId);
            }
            return RedirectToAction(nameof(Watched));
        }

        public string GetUserId()
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
