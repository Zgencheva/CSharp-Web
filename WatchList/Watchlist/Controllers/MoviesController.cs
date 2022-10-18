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
            if (!ModelState.IsValid)
            {
                model.Genres = await context.Genres.Select(g => new GenreInputModel
                {
                    Name = g.Name,
                    Id = g.Id,
                })
                .ToListAsync();
                return this.View(model);

            }
            var movie = new Movie()
            {
                Title = model.Title,
                Director = model.Director,
                Rating = model.Rating,
                ImageUrl = model.ImageUrl,
                GenreId = model.GenreId,
            };

            try
            {
                await this.context.Movies.AddAsync(movie);
                await this.context.SaveChangesAsync();

                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                return this.View(model);
            }
        }
        public async Task<IActionResult> All()
        {
            var model = new AllMoviesModel();
            if (this.context.Movies.Count() > 0)
            {
                model.Movies = await this.context.Movies
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
            }
            return View(model);
        }

        public async Task<IActionResult> Watched()
        {
            var userId = this.GetUserId();
            var user = await this.context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.UsersMovies)
                .ThenInclude(x=> x.Movie)
                .ThenInclude(x=> x.Genre)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentException("Ivalid user");
            }
            var model = new AllMoviesModel();
            if (user.UsersMovies.Any())
            {
                model.Movies = user.UsersMovies
                    .Select(x => new MovieViewModel()
                    {
                        Id = x.Movie.Id,
                        Title = x.Movie.Title,
                        Director = x.Movie.Director,
                        ImageUrl = x.Movie.ImageUrl,
                        Rating = x.Movie.Rating,
                        Genre = x.Movie.Genre.Name == null ? null : x.Movie.Genre.Name,

                    }).ToList();
                
            }
            return View("Mine", model);
        }

        public async Task<IActionResult> AddToCollection(int movieId)
        {
            var userId = this.GetUserId();
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
           
            if (user.UsersMovies.Any(x=> x.MovieId == movieId))
            {
                return RedirectToAction(nameof(All));
            }
           
            try
            {
                user.UsersMovies.Add(new UserMovie()
                {
                    MovieId = movieId
                });
                await this.context.SaveChangesAsync();
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            var userMovie = await this.context.UsersMovies
                .Include(x=> x.User)
                .Include(x=> x.Movie)
                .FirstAsync(x => x.MovieId == movieId);
            if (userMovie == null)
            {
                throw new ArgumentException("Invalid operation");
            }
            if (userMovie.User.Id == this.GetUserId())
            {
                try
                {
                    this.context.UsersMovies.Remove(userMovie);
                    await this.context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
            
            return RedirectToAction(nameof(Watched));
        }
        public string GetUserId() 
        {
           return this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
