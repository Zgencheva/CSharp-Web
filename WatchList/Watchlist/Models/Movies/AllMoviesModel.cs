namespace Watchlist.Models.Movies
{
    public class AllMoviesModel
    {
        public AllMoviesModel()
        {
            this.Movies = new List<MovieViewModel>();
        }
        public ICollection<MovieViewModel> Movies { get; set; }
    }
}
