using Microsoft.AspNetCore.Identity;

namespace Watchlist.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.UsersMovies = new HashSet<UserMovie>();
        }
        public ICollection<UserMovie> UsersMovies { get; set; }
    }
}