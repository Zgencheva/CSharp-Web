using Microsoft.AspNetCore.Identity;

namespace Watchlist.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.UsesrMovies = new HashSet<UserMovie>();
        }
        public ICollection<UserMovie> UsesrMovies { get; set; }
    }
}