namespace VisitACity.Services.Data.Tests
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using VisitACity.Data.Models;
    using VisitACity.Data.Models.Enums;
    using VisitACity.Services.Data.Contracts;
    using Xunit;

    public class AttractionsServiceTests : ServiceTests
    {
        private const string Sofia = "Sofia";
        private const string Plovdiv = "Plovdiv";
        private const string Varna = "Varna";
        private const string Ruse = "Ruse";
        private const string NewRestaurant = "New Restaurant";
        private const string NewAddress = "New address";
        private const string NewPhoneNum = "88587224224";
        private const int TestCountryId = 100;
        private const string TestCountryName = "Bulgaria";

        private IAttractionsService AttractionsService => this.ServiceProvider.GetRequiredService<IAttractionsService>();

        [Fact]
        public async Task GetCountShouldReturnsAttractionsCount()
        {
            await this.SeedTestAttractionsAsync();

            Assert.Equal(3, this.AttractionsService.GetCount());
        }

        private async Task SeedTestAttractionsAsync()
        {
            this.DbContext.Countries.Add(new Country { Id = TestCountryId, Name = TestCountryName });
            await this.DbContext.SaveChangesAsync();

            this.DbContext.Cities.Add(new City { Id = 1, Name = Sofia, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 2, Name = Plovdiv, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 3, Name = Varna, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 4, Name = Ruse, CountryId = TestCountryId });
            await this.DbContext.SaveChangesAsync();

            this.DbContext.Attractions.Add(new Attraction
            {
                Name = "Muzeiko",
                Type = (AttractionType)30,
                AttractionUrl = "https://www.muzeiko.bg/bg",
                Price = 10,
                Address = "3 Professor Boyan Kamenov, 1756 Studentski",
                CityId = 1,
                Image = new Image { Id = "02c5467a-4c9f-4708-86c7-20ca782d8d92", Extension = "jpg" },
                Description = "Children's museum featuring interactive science-related exhibits, a cafe & a gift shop.",
            });
            this.DbContext.Attractions.Add(new Attraction
            {
                Name = "Nacional History Museum",
                Type = (AttractionType)30,
                AttractionUrl = "https://historymuseum.org/",
                Price = 10,
                Address = "16 Vitoshko lale",
                CityId = 1,
                Image = new Image { Id = "0a4c0be2-e549-49e8-9d4e-d9881080009f", Extension = "jpg" },
                Description = "The National Historical Museum in Sofia is Bulgaria's largest museum. It was founded on 5 May 1973. A new representative exhibition was opened in the building of the Court of Justice on 2 March 1984, to commemorate the 13th centenary of the Bulgarian state",
            });
            this.DbContext.Attractions.Add(new Attraction
            {
                Name = "Sait Sofia",
                Type = (AttractionType)30,
                AttractionUrl = "https://stolica.bg/sofia-tur/hramove/tsarkva-sveta-sofiya",
                Price = 0,
                Address = "2 Paris",
                CityId = 2,
                Image = new Image { Id = "0b38c0d5-5a00-4aff-80dc-cfbb692e9db1", Extension = "jpg" },
                Description = "The church was built on the site of several earlier churches from the 4th century, and places of worship dating back to the days when it was the necropolis of the Roman town of Serdica.",
            });

            await this.DbContext.SaveChangesAsync();
        }

    }
}
