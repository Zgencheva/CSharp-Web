namespace VisitACity.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Cities;
    using VisitACity.Web.ViewModels.Cities;
    using Xunit;

    public class CitiesServiceTests : ServiceTests
    {
        private const string Sofia = "Sofia";
        private const string Plovdiv = "Plovdiv";
        private const string Varna = "Varna";
        private const string Asenovgrad = "Asenovgrad";
        private const string Ruse = "Ruse";
        private const string InvalidCity = "Uganda";
        private const string CityRoCreate = "Stara Zagora";
        private const int TestCountryId = 100;
        private const string TestCountryName = "Bulgaria";

        private ICitiesService CitiesService => this.ServiceProvider.GetRequiredService<ICitiesService>();

        private ICountriesService CountriesService => this.ServiceProvider.GetRequiredService<ICountriesService>();

        [Fact]
        public async Task GetCountShouldReturnsCitiesCount()
        {
            await this.SeedTestCitiesAsync();

            Assert.Equal(4, this.CitiesService.GetCount());
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnAllCities()
        {
            await this.SeedTestCitiesAsync();

            var expectedResult = new List<CityViewModel>()
            {
                new CityViewModel { Id = 1, Name = Sofia },
                new CityViewModel { Id = 2, Name = Plovdiv },
                new CityViewModel { Id = 3, Name = Varna },
                new CityViewModel { Id = 4, Name = Ruse },
            };

            var actualResult = await this.CitiesService.GetAllAsync<CityViewModel>();

            Assert.Collection(
                actualResult,
                city1 =>
                {
                    Assert.Equal(expectedResult[0].Id, city1.Id);
                    Assert.Equal(expectedResult[0].Name, city1.Name);
                },
                city2 =>
                {
                    Assert.Equal(expectedResult[1].Id, city2.Id);
                    Assert.Equal(expectedResult[1].Name, city2.Name);
                },
                city3 =>
                {
                    Assert.Equal(expectedResult[2].Id, city3.Id);
                    Assert.Equal(expectedResult[2].Name, city3.Name);
                },
                city4 =>
                {
                    Assert.Equal(expectedResult[3].Id, city4.Id);
                    Assert.Equal(expectedResult[3].Name, city4.Name);
                });

            Assert.Equal(4, actualResult.Count());

            Assert.Equal(expectedResult.Count, actualResult.Count());
        }

        [Fact]
        public async Task CreateAsyncShouldCreateCity()
        {
            await this.SeedTestCountryAsync();
            await this.SeedTestCitiesAsync();
            var cityFormModel = new CityFormModel
            {
                Name = CityRoCreate,
                CountryId = TestCountryId,

            };

            await this.CitiesService.CreateAsync(cityFormModel);

            var lastAddedCity = this.DbContext.Cities.OrderByDescending(r => r.CreatedOn).First();

            Assert.Equal(lastAddedCity.Name, cityFormModel.Name);
            Assert.Equal(5, this.DbContext.Cities.Count());
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenCityExists()
        {
            await this.SeedTestCountryAsync();
            await this.SeedTestCitiesAsync();

            var cityFormModel = new CityFormModel
            {
                Name = Plovdiv,
                CountryId = TestCountryId,

            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                 this.CitiesService.CreateAsync(cityFormModel));
            Assert.Equal(string.Format(ModelConstants.City.CityExists, Plovdiv), exception.Message);
        }

        [Fact]
        public async Task CreateAsyncShouldUndeleteDeletedCityIfExists()
        {
            await this.SeedTestCountryAsync();
            await this.SeedTestCitiesAsync();

            var deletedCountry = await this.DbContext.Cities.FirstOrDefaultAsync(x => x.Name == Sofia);
            deletedCountry.IsDeleted = true;
            await this.DbContext.SaveChangesAsync();

            var cityToAdd = new CityFormModel
            {
                Name = Sofia,
                CountryId = TestCountryId,
            };

            await this.CitiesService.CreateAsync(cityToAdd);
            Assert.False(deletedCountry.IsDeleted);
        }

        private async Task SeedTestCountryAsync()
        {
            this.DbContext.Countries.Add(new Country { Id = TestCountryId, Name = TestCountryName});
            await this.DbContext.SaveChangesAsync();
        }

        private async Task SeedTestCitiesAsync()
        {
            this.DbContext.Cities.Add(new City { Id = 1, Name = Sofia, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 2, Name = Plovdiv, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 3, Name = Varna, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 4, Name = Ruse , CountryId = TestCountryId });

            await this.DbContext.SaveChangesAsync();
        }
    }
}
