namespace VisitACity.Tests
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
    using VisitACity.Web.ViewModels.Administration.Countries;
    using VisitACity.Web.ViewModels.Countries;
    using Xunit;

    public class CountriesServiceTests : ServiceTests
    {
        private const string Bulgaria = "Bulgaria";
        private const string Egypt = "Egypt";
        private const string Spain = "Spain";
        private const string Uganda = "Uganda";
        private const string Brazil = "Brazil";

        private ICountriesService CountriesService => this.ServiceProvider.GetRequiredService<ICountriesService>();

        [Fact]
        public async Task GetAllAsyncShouldReturnAllCountries()
        {
            await this.SeedTestCountriesAsync();

            var expected = new List<CountryViewModel>()
            {
                new CountryViewModel { Id = 1, Name = Bulgaria },
                new CountryViewModel { Id = 2, Name = Egypt },
                new CountryViewModel { Id = 3, Name = Spain },
                new CountryViewModel { Id = 4, Name = Brazil },
            };

            var actual = await this.CountriesService.GetAllAsync<CountryViewModel>();

            Assert.Collection(
                actual,
                country1 =>
                {
                    Assert.Equal(expected[0].Id, country1.Id);
                    Assert.Equal(expected[0].Name, country1.Name);
                },
                country2 =>
                {
                    Assert.Equal(expected[1].Id, country2.Id);
                    Assert.Equal(expected[1].Name, country2.Name);
                },
                country3 =>
                {
                    Assert.Equal(expected[2].Id, country3.Id);
                    Assert.Equal(expected[2].Name, country3.Name);
                },
                country4 =>
                {
                    Assert.Equal(expected[3].Id, country4.Id);
                    Assert.Equal(expected[3].Name, country4.Name);
                });

            Assert.Equal(4, actual.Count());

            Assert.Equal(expected.Count, actual.Count());
        }

        [Fact]
        public async Task CreateAsyncShouldCreateCountry()
        {
            await this.SeedTestCountriesAsync();

            var countryFormModel = new CountryFormModel
            {
                Id = 5,
                Name = "Italy",
            };

            await this.CountriesService.CreateAsync(countryFormModel);

            var lastAddedCountry = this.DbContext.Countries.OrderByDescending(r => r.CreatedOn).First();

            Assert.Equal(lastAddedCountry.Id, countryFormModel.Id);
            Assert.Equal(lastAddedCountry.Name, countryFormModel.Name);
            Assert.Equal(5, this.DbContext.Countries.Count());
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenCountryExists()
        {
            await this.SeedTestCountriesAsync();

            var countryFormModel = new CountryFormModel
            {
                Id = 1,
                Name = "Bulgaria",
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                 this.CountriesService.CreateAsync(countryFormModel));
            Assert.Equal(string.Format(ModelConstants.Country.CountryExists, Bulgaria), exception.Message);
        }

        [Fact]
        public async Task CreateAsyncShouldUndeleteDeletedCountryIfExists()
        {
            await this.SeedTestCountriesAsync();

            var deletedCountry = await this.DbContext.Countries.FirstOrDefaultAsync(x => x.Name == Bulgaria);
            deletedCountry.IsDeleted = true;
            await this.DbContext.SaveChangesAsync();

            var countryToAdd = new CountryFormModel
            {
                Id = 1,
                Name = Bulgaria,
            };

            await this.CountriesService.CreateAsync(countryToAdd);
            Assert.False(deletedCountry.IsDeleted);
        }

        [Fact]
        public async Task DoesCountryExistShouldReturnTrueIfCountryExists()
        {
            await this.SeedTestCountriesAsync();
            var countryName = Bulgaria;

            var result = this.CountriesService.DoesCountryExist(countryName);
            Assert.True(result.Result);
        }

        [Fact]
        public async Task DoesCountryExistShouldReturnFalseIfCountryExists()
        {
            await this.SeedTestCountriesAsync();
            var countryName = Uganda;

            var result = this.CountriesService.DoesCountryExist(countryName);
            Assert.False(result.Result);
        }

        [Fact]
        public async Task DeleteAsyncShouldMarkCountryAsDeleted()
        {
            await this.SeedTestCountriesAsync();

            var countryToDelete = await this.DbContext.Countries.FirstOrDefaultAsync(x => x.Name == Bulgaria);

            await this.CountriesService.DeleteAsync(Bulgaria);

            Assert.True(countryToDelete.IsDeleted);
        }

        [Fact]
        public async Task DeleteAsyncShouldThrowExceptionWhenInvalidNamePassed()
        {
            await this.SeedTestCountriesAsync();

            var countryToDelete = await this.DbContext.Countries.FirstOrDefaultAsync(x => x.Name == Bulgaria);
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(async () => await this.CountriesService.DeleteAsync(Uganda));
            Assert.Equal(ExceptionMessages.Country.NotExists, exception.Message);
        }

        private async Task SeedTestCountriesAsync()
        {
            this.DbContext.Countries.Add(new Country { Id = 1, Name = Bulgaria });
            this.DbContext.Countries.Add(new Country { Id = 2, Name = Egypt });
            this.DbContext.Countries.Add(new Country { Id = 3, Name = Spain });
            this.DbContext.Countries.Add(new Country { Id = 4, Name = Brazil });
            await this.DbContext.SaveChangesAsync();
        }
    }
}
