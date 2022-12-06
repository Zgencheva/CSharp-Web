namespace VisitACity.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Restaurants;
    using VisitACity.Web.ViewModels.Cities;
    using VisitACity.Web.ViewModels.Restaurants;
    using VisitACity.Web.ViewModels.Reviews;
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
        private const string TestUserId = "dasdasdasdas-dasdasdas-asdsadas";
        private const string TestUserName = "admin @gmail.com";
        private const string TestUserPassword = "a123456";
        private const string TestUserFirstName = "Admin";
        private const string TestUserLastName = "Admin";

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

        [Fact]
        public async Task DeleteAsyncShouldMarkRestaurantAsDeleted()
        {
            await this.SeedTestRestaurantsAsync();
            var restaurantToDelete = await this.DbContext.Restaurants.FirstOrDefaultAsync(x => x.Name == "ChasovnikA");

            await this.restaurantService.DeleteByIdAsync(restaurantToDelete.Id);

            Assert.True(restaurantToDelete.IsDeleted);
        }

        [Fact]
        public async Task DeleteAsyncShouldThrowExceptionWhenInvalidNamePassed()
        {
            await this.SeedTestRestaurantsAsync();

            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
                async () =>
                await this.restaurantService.DeleteByIdAsync(55));
            Assert.Equal(ExceptionMessages.Restaurant.InvalidRestaurant, exception.Message);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnCorrectICollection()
        {
            await this.SeedTestRestaurantsAsync();

            var expectedResult = new List<RestaurantViewModel>()
            {
                new RestaurantViewModel
            {
                Id = 1,
                Name = "ChasovnikA",
                Url = "https://chasovnikavarna.com/",
                Address = "4 Knjaz N. Nikolaevich",
                CityName = Sofia,
                ImageUrl = "https://zavedenia.com/zimages/varna/big/1831/1831lTCxEeMFBeksgU8XJbyhdYnWr22FvK7RDJp.jpg",
                PhoneNumber = "+35979541589",
                Rating = 0,
                UserPlan = null,
                Reviews = new List<ReviewRestaurantViewModel>(),
            },
                new RestaurantViewModel
                {
                Id = 3,
                Name = "Hebros",
                Url = "https://hebros-hotel.com/restorant/",
                Address = "51 Konstantin Stoilov",
                CityName = Sofia,
                ImageUrl = "https://image9003.dineout.bg/isxKn_YSeTTq1vbQpQ2GRd-m6p4=/800x/places/f4e9e6b2b90317c6bcf870333323b2e0/thumb_3624600e2c556972043123f939140ba0.jpeg",
                PhoneNumber = "+359886511459",
                Rating = 0,
                UserPlan = null,
                Reviews = new List<ReviewRestaurantViewModel>(),
                },
            };

            var actualResult = await this.restaurantService.GetByCityAsync<RestaurantViewModel>(Sofia, 1, 6);

            Assert.Collection(
                actualResult,
                city1 =>
                {
                    Assert.Equal(expectedResult[1].Id, city1.Id);
                    Assert.Equal(expectedResult[1].Name, city1.Name);
                    Assert.Equal(expectedResult[1].Url, city1.Url);
                    Assert.Equal(expectedResult[1].Address, city1.Address);
                    Assert.Equal(expectedResult[1].CityName, city1.CityName);
                    Assert.Equal(expectedResult[1].ImageUrl, city1.ImageUrl);
                    Assert.Equal(expectedResult[1].PhoneNumber, city1.PhoneNumber);
                    Assert.Equal(expectedResult[1].Reviews.Count(), city1.Reviews.Count());
                    Assert.Equal(expectedResult[1].Rating, city1.Rating);
                    Assert.Equal(expectedResult[1].UserPlan, city1.UserPlan);
                },
                city2 =>
                {
                    Assert.Equal(expectedResult[0].Address, city2.Address);
                    Assert.Equal(expectedResult[0].CityName, city2.CityName);
                    Assert.Equal(expectedResult[0].Id, city2.Id);
                    Assert.Equal(expectedResult[0].ImageUrl, city2.ImageUrl);
                    Assert.Equal(expectedResult[0].Name, city2.Name);
                    Assert.Equal(expectedResult[0].Url, city2.Url);
                    Assert.Equal(expectedResult[0].PhoneNumber, city2.PhoneNumber);
                    Assert.Equal(expectedResult[0].Reviews.Count(), city2.Reviews.Count());
                    Assert.Equal(expectedResult[0].Rating, city2.Rating);
                    Assert.Equal(expectedResult[0].UserPlan, city2.UserPlan);
                });

            Assert.Equal(2, actualResult.Count());

            Assert.Equal(expectedResult.Count, actualResult.Count());
        }

        [Fact]
        public async Task GetCountShouldReturnsRestaurantsCount()
        {
            await this.SeedTestRestaurantsAsync();

            Assert.Equal(3, this.restaurantService.GetCount());
        }

        [Fact]
        public async Task GetCountByCityShouldReturns0WhenCityNotFound()
        {
            await this.SeedTestRestaurantsAsync();

            Assert.Equal(0, this.restaurantService.GetCountByCity("Uganda"));
        }

        [Fact]
        public async Task GetCountByCityShouldReturnsRestaurantsCountByCity()
        {
            await this.SeedTestRestaurantsAsync();

            Assert.Equal(2, this.restaurantService.GetCountByCity(Sofia));
        }

        [Fact]
        public async Task GetRestaurantCityAsyncShouldReturnsCity()
        {
            await this.SeedTestRestaurantsAsync();
            var expectedResult = new CityViewModel
            {
                Id = 1,
                Name = Sofia,
            };
            var actualResult = await this.restaurantService.GetRestaurantCityAsync(1);
            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Name, actualResult.Name);
        }

        [Fact]
        public async Task GetRestaurantCityAsyncShouldThrowExceptionWhenInvalidRestaurantPassed()
        {
            await this.SeedTestRestaurantsAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.restaurantService.GetRestaurantCityAsync(55));
            Assert.Equal(ExceptionMessages.Restaurant.InvalidRestaurant, exception.Message);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnCorrectViewModel()
        {
            await this.SeedTestRestaurantsAsync();
            var expectedResult = new RestaurantViewModel
            {
                Id = 1,
                Name = "ChasovnikA",
                Url = "https://chasovnikavarna.com/",
                Address = "4 Knjaz N. Nikolaevich",
                CityName = Sofia,
                ImageUrl = "https://zavedenia.com/zimages/varna/big/1831/1831lTCxEeMFBeksgU8XJbyhdYnWr22FvK7RDJp.jpg",
                PhoneNumber = "+35979541589",
                Rating = 5,
                UserPlan = null,
                Reviews = new List<ReviewRestaurantViewModel>()
                {
                    new ReviewRestaurantViewModel
                    {
                        Id = 1,
                        UserUserName = TestUserId,
                        Rating = 5,
                        Content = "test content review",
                        RestaurantId = 1,
                        CreatedOn = DateTime.UtcNow.Date,
                    },
                },
            };
            var restaurant = await this.DbContext.Restaurants.FindAsync(1);
            restaurant.Reviews.Add(new Review
            {
                UserId = TestUserId,
                Content = "test content review",
                Rating = 5,
            });
            await this.DbContext.SaveChangesAsync();
            var actualResult = await this.restaurantService.GetViewModelByIdAsync<RestaurantViewModel>(1);

            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Name, actualResult.Name);
            Assert.Equal(expectedResult.Url, actualResult.Url);
            Assert.Equal(expectedResult.Address, actualResult.Address);
            Assert.Equal(expectedResult.CityName, actualResult.CityName);
            Assert.Equal(expectedResult.ImageUrl, actualResult.ImageUrl);
            Assert.Equal(expectedResult.PhoneNumber, actualResult.PhoneNumber);
            Assert.Equal(expectedResult.Rating, actualResult.Rating);
            Assert.Equal(expectedResult.UserPlan, actualResult.UserPlan);
            Assert.Equal(expectedResult.Reviews.Count(), actualResult.Reviews.Count());
            Assert.Equal(expectedResult.Reviews.FirstOrDefault().Content, actualResult.Reviews.FirstOrDefault().Content);
            Assert.Equal(expectedResult.Reviews.FirstOrDefault().Rating, actualResult.Reviews.FirstOrDefault().Rating);
            Assert.Equal(expectedResult.Reviews.FirstOrDefault().CreatedOn.Date, actualResult.Reviews.FirstOrDefault().CreatedOn.Date);
            Assert.Equal(expectedResult.Reviews.FirstOrDefault().RestaurantId, actualResult.Reviews.FirstOrDefault().RestaurantId);
            Assert.Equal(expectedResult.Reviews.FirstOrDefault().Id, actualResult.Reviews.FirstOrDefault().Id);


            Assert.Equal(1, actualResult.Reviews.Count());
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateRestaurant()
        {
            await this.SeedTestRestaurantsAsync();
            var expectedResult = new RestaurantFromModel
            {
                Name = "New name",
                PhoneNumber = "New Phone",
                Url = "http://www.stariachinar.com/cherno-more/",
                ImageUrl = "https://zavedenia.com/zimages/varna/big/1831/1831lTCxEeMFBeksgU8XJbyhdYnWr22FvK7RDJp.jpg",
                Address = "New address",
                CityId = 1,
            };

            var restaurantToUpdate = await this.DbContext.Restaurants.FirstOrDefaultAsync(x => x.City.Name == Plovdiv);

            await this.restaurantService.UpdateAsync(restaurantToUpdate.Id, expectedResult);

            Assert.Equal(expectedResult.Name, restaurantToUpdate.Name);
            Assert.Equal(expectedResult.Url, restaurantToUpdate.Url);
            Assert.Equal(expectedResult.Address, restaurantToUpdate.Address);
            Assert.Equal(expectedResult.CityId, restaurantToUpdate.CityId);
            Assert.Equal(expectedResult.ImageUrl, restaurantToUpdate.ImageUrl);
            Assert.Equal(expectedResult.PhoneNumber, restaurantToUpdate.PhoneNumber);
            Assert.Equal(restaurantToUpdate.ModifiedOn.Value.Date,DateTime.UtcNow.Date);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldThrowExceptionWhenInvalidRestaurantPassed()
        {
            await this.SeedTestRestaurantsAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.restaurantService.GetViewModelByIdAsync<RestaurantViewModel>(55));
            Assert.Equal(ExceptionMessages.Restaurant.InvalidRestaurant, exception.Message);
        }

        [Fact]
        public async Task UpdateAsyncShouldThrowExceptionUponInvalidRestaurantPassed()
        {
            await this.SeedTestRestaurantsAsync();
            var testRestaurant = new RestaurantFromModel
            {
                Name = "New name",
                PhoneNumber = "New Phone",
                Url = "http://www.stariachinar.com/cherno-more/",
                ImageUrl = "https://zavedenia.com/zimages/varna/big/1831/1831lTCxEeMFBeksgU8XJbyhdYnWr22FvK7RDJp.jpg",
                Address = "New address",
                CityId = 1,
            };
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.restaurantService.UpdateAsync(55, testRestaurant));
            Assert.Equal(ExceptionMessages.Restaurant.InvalidRestaurant, exception.Message);
        }

        [Fact]
        public async Task UpdateAsyncShouldThrowExceptionUponInvalidCityPassed()
        {
            await this.SeedTestRestaurantsAsync();
            var testRestaurant = new RestaurantFromModel
            {
                Name = "New name",
                PhoneNumber = "New Phone",
                Url = "http://www.stariachinar.com/cherno-more/",
                ImageUrl = "https://zavedenia.com/zimages/varna/big/1831/1831lTCxEeMFBeksgU8XJbyhdYnWr22FvK7RDJp.jpg",
                Address = "New address",
                CityId = 55,
            };
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.restaurantService.UpdateAsync(1, testRestaurant));
            Assert.Equal(ExceptionMessages.City.NotExists, exception.Message);
        }

        private async Task SeedTestRestaurantsAsync()
        {
            await this.SeedTestCountriesAsync();
            await this.SeedTestCitiesAsync();
            await this.SeedTestUserAsync();

            this.DbContext.Restaurants.Add(new Restaurant
            {
                Id = 1,
                Name = "ChasovnikA",
                Url = "https://chasovnikavarna.com/",
                Address = "4 Knjaz N. Nikolaevich",
                CityId = 1,
                ImageUrl = "https://zavedenia.com/zimages/varna/big/1831/1831lTCxEeMFBeksgU8XJbyhdYnWr22FvK7RDJp.jpg",
                PhoneNumber = "+35979541589",
            });
            this.DbContext.Restaurants.Add(new Restaurant
            {
                Id = 2,
                Name = "Staria chinar",
                Url = "http://www.stariachinar.com/cherno-more/",
                Address = "Port Varna",
                CityId = 2,
                ImageUrl = "https://static.pochivka.bg/restaurants.bgstay.com/images/restaurants/01/1279/320x230/5617902937b23.JPG",
                PhoneNumber = "+35979549494",
            });
            this.DbContext.Restaurants.Add(new Restaurant
            {
                Id = 3,
                Name = "Hebros",
                Url = "https://hebros-hotel.com/restorant/",
                Address = "51 Konstantin Stoilov",
                CityId = 1,
                ImageUrl = "https://image9003.dineout.bg/isxKn_YSeTTq1vbQpQ2GRd-m6p4=/800x/places/f4e9e6b2b90317c6bcf870333323b2e0/thumb_3624600e2c556972043123f939140ba0.jpeg",
                PhoneNumber = "+359886511459",
            });

            await this.DbContext.SaveChangesAsync();
        }

        private async Task SeedTestCountriesAsync()
        {
            this.DbContext.Countries.Add(new Country { Id = TestCountryId, Name = TestCountryName });
            await this.DbContext.SaveChangesAsync();
        }

        private async Task SeedTestCitiesAsync()
        {
            this.DbContext.Cities.Add(new City { Id = 1, Name = Sofia, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 2, Name = Plovdiv, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 3, Name = Varna, CountryId = TestCountryId });
            this.DbContext.Cities.Add(new City { Id = 4, Name = Ruse, CountryId = TestCountryId });
            await this.DbContext.SaveChangesAsync();
        }

        private async Task SeedTestUserAsync()
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser
            {
                Id = TestUserId,
                UserName = TestUserName,
                NormalizedUserName = TestUserName.ToUpper(),
                Email = TestUserName,
                NormalizedEmail = TestUserName.ToUpper(),
                FirstName = TestUserFirstName,
                LastName = TestUserLastName,
                EmailConfirmed = true,
            };
            user.PasswordHash = hasher.HashPassword(user, TestUserPassword);
            this.DbContext.Users.Add(user);
            await this.DbContext.SaveChangesAsync();
        }
    }
}
