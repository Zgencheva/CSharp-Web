namespace VisitACity.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using VisitACity.Data.Models;

    public class CountriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Countries.Any())
            {
                return;
            }

            await dbContext.Countries.AddAsync(new Country { Name = "Bulgaria" });
            await dbContext.Countries.AddAsync(new Country { Name = "Croatia" });
            await dbContext.Countries.AddAsync(new Country { Name = "USA" });
            await dbContext.Countries.AddAsync(new Country { Name = "Thailand" });

            await dbContext.SaveChangesAsync();
        }
    }
}
