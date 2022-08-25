using SUS.MvcFramework;
using System;
using System.Collections.Generic;

namespace BattleCards.Data
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cards = new HashSet<UserCard>();
        }
     

        public virtual ICollection<UserCard> Cards { get; set; }
    }
}
