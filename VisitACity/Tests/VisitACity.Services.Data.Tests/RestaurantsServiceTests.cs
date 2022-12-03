namespace VisitACity.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Restaurants;
    using Xunit;

    public class RestaurantsServiceTests : ServiceTests
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

        private IRestaurantsService restaurantService => this.ServiceProvider.GetRequiredService<IRestaurantsService>();

        [Fact]
        public async Task CreateAsyncShouldCreateRestaurant()
        {
            await this.SeedTestRestaurantsAsync();
            var restaurantTestModel = new RestaurantFromModel
            {
                Name = NewRestaurant,
                CityId = 1,
                Address = NewAddress,
                ImageUrl = "https://zavedenia.com/zimages/varna/big/1831/1831lTCxEeMFBeksgU8XJbyhdYnWr22FvK7RDJp.jpg",
                Url = "https://chasovnikavarna.com/",
                PhoneNumber = NewPhoneNum,
            };
            var exptectedCount = 4;

            await this.restaurantService.CreateAsync(restaurantTestModel);

            var restaurantActual = this.DbContext.Restaurants.OrderByDescending(x => x.CreatedOn).First();
            var countAfterAdd = this.DbContext.Restaurants.Count();

            Assert.Equal(restaurantActual.Name, restaurantTestModel.Name);
            Assert.Equal(restaurantActual.CityId, restaurantTestModel.CityId);
            Assert.Equal(restaurantActual.Url, restaurantTestModel.Url);
            Assert.Equal(restaurantActual.ImageUrl, restaurantTestModel.ImageUrl);
            Assert.Equal(restaurantActual.PhoneNumber, restaurantTestModel.PhoneNumber);
            Assert.Equal(exptectedCount, countAfterAdd);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenInvalidCityPassed()
        {
            await this.SeedTestRestaurantsAsync();
            var restaurantTestModel = new RestaurantFromModel
            {
                Name = NewRestaurant,
                CityId = 55,
                Address = NewAddress,
                ImageUrl = "https://zavedenia.com/zimages/varna/big/1831/1831lTCxEeMFBeksgU8XJbyhdYnWr22FvK7RDJp.jpg",
                Url = "https://chasovnikavarna.com/",
                PhoneNumber = NewPhoneNum,
            };
            var exception = await Assert.ThrowsAsync<NullReferenceException>(async
                () => await this.restaurantService.CreateAsync(restaurantTestModel));
            Assert.Equal(ExceptionMessages.City.NotExists, exception.Message);
        }

        private async Task SeedTestRestaurantsAsync()
        {
            this.DbContext.Countries.Add(new Country { Id = TestCountryId, Name = TestCountryName });
            await this.DbContext.SaveChangesAsync();

            this.DbContext.Cities.Add(new City { Id = 1, Name = Sofia, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 2, Name = Plovdiv, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 3, Name = Varna, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 4, Name = Ruse, CountryId = TestCountryId });
            await this.DbContext.SaveChangesAsync();

            this.DbContext.Restaurants.Add(new Restaurant
            {
                Name = "ChasovnikA",
                Url = "https://chasovnikavarna.com/",
                Address = "4 Knjaz N. Nikolaevich",
                CityId = 1,
                ImageUrl = "https://zavedenia.com/zimages/varna/big/1831/1831lTCxEeMFBeksgU8XJbyhdYnWr22FvK7RDJp.jpg",
                PhoneNumber = "+35979541589",
            });
            this.DbContext.Restaurants.Add(new Restaurant
            {
                Name = "Staria chinar",
                Url = "http://www.stariachinar.com/cherno-more/",
                Address = "Port Varna",
                CityId = 2,
                ImageUrl = "https://static.pochivka.bg/restaurants.bgstay.com/images/restaurants/01/1279/320x230/5617902937b23.JPG",
                PhoneNumber = "+35979549494",
            });
            this.DbContext.Restaurants.Add(new Restaurant
            {
                Name = "Hebros",
                Url = "https://hebros-hotel.com/restorant/",
                Address = "51 Konstantin Stoilov",
                CityId = 1,
                ImageUrl = "https://image9003.dineout.bg/isxKn_YSeTTq1vbQpQ2GRd-m6p4=/800x/places/f4e9e6b2b90317c6bcf870333323b2e0/thumb_3624600e2c556972043123f939140ba0.jpeg",
                PhoneNumber = "+359886511459",
            });

            await this.DbContext.SaveChangesAsync();
        }
    }
}
