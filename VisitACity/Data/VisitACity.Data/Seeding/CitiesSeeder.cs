namespace VisitACity.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using VisitACity.Data.Models;

    public class CitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cities.Any())
            {
                return;
            }

            var country = dbContext.Countries.FirstOrDefault(x => x.Name == "Bulgaria");

            if (country == null)
            {
                return;
            }

            await dbContext.Cities.AddAsync(new City { Name = "Plovdiv", Country = country });
            await dbContext.Cities.AddAsync(new City { Name = "Sofia", Country = country });
            await dbContext.Cities.AddAsync(new City { Name = "Tryavna", Country = country });
            await dbContext.Cities.AddAsync(new City { Name = "Hisarya", Country = country });
            await dbContext.Cities.AddAsync(new City { Name = "Velingrad", Country = country });
        }
    }
}
