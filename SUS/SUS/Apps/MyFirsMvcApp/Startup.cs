using BattleCards.Data;
using Microsoft.EntityFrameworkCore;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;


namespace MyFirsMvcApp
{
    public class Startup : IMvcApplication
    {
 
       public void Configure(List<Route> routeTable)
        {
          
        }

        public void ConfigureServices()
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
