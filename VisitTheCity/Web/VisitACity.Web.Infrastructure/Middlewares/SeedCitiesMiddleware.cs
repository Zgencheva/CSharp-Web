namespace VisitACity.Web.Infrastructure.Middlewares
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using VisitACity.Data;
    using VisitACity.Data.Models;

    public class SeedCitiesMiddleware
    {
        private readonly RequestDelegate next;
        private ApplicationDbContext dbContext;

        public SeedCitiesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            await this.SeedCities();
            await this.next(context);
        }

        private async Task SeedCities()
        {
            var seedCities = this.dbContext.Cities;
            if (seedCities.Any())
            {
                return;
            }

            var jsonString = File.ReadAllText("citiesInBulgaria.json");
            var cities = JsonConvert.DeserializeObject<City[]>(jsonString);
            var country = this.dbContext.Countries.FirstOrDefault(x => x.Name == "Bulgaria");
            if (country == null)
            {
                country = new Country { Name = "Bulgaria" };
                await this.dbContext.Countries.AddAsync(country);
            }

            foreach (var city in cities)
            {
               country.Cities.Add(city);
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
