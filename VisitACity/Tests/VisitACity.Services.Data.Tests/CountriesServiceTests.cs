using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using VisitACity.Data.Models;
using VisitACity.Services.Data.Contracts;
using VisitACity.Web.ViewModels.Countries;
using Xunit;

namespace VisitACity.Services.Data.Tests
{
    public class CountriesServiceTests : ServiceTests
    {
        private const string Bulgaria = "Bulgaria";
        private const string Egypt = "Egypt";
        private const string Spain = "Spain";

        private ICountriesService CountriesServiceMoq => this.ServiceProvider.GetRequiredService<ICountriesService>();

        [Fact]
        public async Task GetAllAsyncReturnsAllCountries()
        {
            this.DbContext.Countries.Add(new Country { Id = 1, Name = Bulgaria });
            this.DbContext.Countries.Add(new Country { Id = 2, Name = Egypt });
            this.DbContext.Countries.Add(new Country { Id = 3, Name = Spain });
            await this.DbContext.SaveChangesAsync();

            var expected = new CountryViewModel[]
            {
                new CountryViewModel { Id = 1, Name = Bulgaria },
                new CountryViewModel { Id = 2, Name = Egypt },
                new CountryViewModel { Id = 3, Name = Spain },
            };

            var actual = await this.CountriesServiceMoq.GetAllAsync<CountryViewModel>();

            Assert.Collection(actual,
                elem1 =>
                {
                    Assert.Equal(expected[0].Id, elem1.Id);
                    Assert.Equal(expected[0].Name, elem1.Name);
                },
                elem2 =>
                {
                    Assert.Equal(expected[1].Id, elem2.Id);
                    Assert.Equal(expected[1].Name, elem2.Name);
                },
                elem3 =>
                {
                    Assert.Equal(expected[2].Id, elem3.Id);
                    Assert.Equal(expected[2].Name, elem3.Name);
                });

            Assert.Equal(expected.Count(), actual.Count());
        }
    }
}
