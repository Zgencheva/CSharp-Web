using BattleCards.Data;
using BattleCards.Services;
using Microsoft.EntityFrameworkCore;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;


namespace MyFirsMvcApp
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            //AddSingleton
            //AddTransient
            //AddScoped
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<ICardsService, CardsService>();
        }
        public void Configure(List<Route> routeTable)
        {
          
        }

       
    }
}
