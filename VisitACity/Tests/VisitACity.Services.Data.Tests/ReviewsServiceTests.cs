namespace VisitACity.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Reviews;
    using Xunit;

    public class ReviewsServiceTests : ServiceTests
    {
        private const string Sofia = "Sofia";
        private const string Plovdiv = "Plovdiv";
        private const string Varna = "Varna";
        private const string Ruse = "Ruse";
        private const int TestCountryId = 1;
        private const int InvalidRestaurantId = 44;
        private const int TestRestaurantId = 1;
        private const string TestCountryName = "Bulgaria";
        private const string TestUserId = "dasdasdasdas-dasdasdas-asdsadas";
        private const string TestUserName = "admin @gmail.com";
        private const string TestUserPassword = "a123456";
        private const string TestUserFirstName = "Admin";
        private const string TestUserLastName = "Admin";
        private const string InvalidUserId = "dasdasdasdas";

        private IReviewsService ReviewsService => this.ServiceProvider.GetRequiredService<IReviewsService>();

        [Fact]
        public async Task AddReviewToUserAsyncShouldAddReviewToUser()
        {
            await this.SeedDbAsync();
            var inputModel = new CreateReviewInputModel
            {
                Rating = 3,
                Content = "Very good place to eat",
            };
            var restaurant = await this.DbContext.Restaurants.FindAsync(TestRestaurantId);
            var user = await this.DbContext.Users.FindAsync(TestUserId);
            await this.ReviewsService.AddReviewToRestaurantAsync(inputModel, TestUserId, TestRestaurantId);
            Assert.Equal(1, restaurant.Reviews.Count);
            Assert.Equal(1, user.Reviews.Count);

            var review = user.Reviews
                .FirstOrDefault();
            Assert.Equal(inputModel.Rating, review.Rating);
            Assert.Equal(inputModel.Content, review.Content);
            Assert.Equal(review.RestaurantId, TestRestaurantId);
            Assert.Equal(review.Restaurant.Id, TestRestaurantId);
            Assert.Equal(review.UserId, TestUserId);
            Assert.Equal(review.User.Id, TestUserId);
        }

        [Fact]
        public async Task AddReviewToUserAsyncShouldThrowExceptionWhenInvalidUserID()
        {
            await this.SeedDbAsync();
            var inputModel = new CreateReviewInputModel
            {
                Rating = 3,
                Content = "Very good place to eat",
            };
            var exception = await Assert.ThrowsAsync<NullReferenceException>(async () =>
            await this.ReviewsService.AddReviewToRestaurantAsync(inputModel, InvalidUserId, TestRestaurantId));
            Assert.Equal(ExceptionMessages.NotExistingUser, exception.Message);
        }

        [Fact]
        public async Task AddReviewToUserAsyncShouldThrowExceptionWhenInvalidRestaurantId()
        {
            await this.SeedDbAsync();
            var inputModel = new CreateReviewInputModel
            {
                Rating = 3,
                Content = "Very good place to eat",
            };
            var exception = await Assert.ThrowsAsync<NullReferenceException>(async () =>
            await this.ReviewsService.AddReviewToRestaurantAsync(inputModel, TestUserId, InvalidRestaurantId));
            Assert.Equal(ExceptionMessages.Restaurant.InvalidRestaurant, exception.Message);
        }

        [Fact]
        public async Task DeleteAsyncShouldMarkReviewAsDeleted()
        {
            await this.SeedDbAsync();
            var inputModel = new CreateReviewInputModel
            {
                Rating = 3,
                Content = "Very good place to eat",
            };
            var restaurant = await this.DbContext.Restaurants.FindAsync(TestRestaurantId);
            var user = await this.DbContext.Users.FindAsync(TestUserId);
            await this.ReviewsService.AddReviewToRestaurantAsync(inputModel, TestUserId, TestRestaurantId);
            var review = user.Reviews.FirstOrDefault();

            await this.ReviewsService.DeleteAsync(review.Id);
            Assert.True(review.IsDeleted);
        }

        private async Task SeedDbAsync()
        {
            await this.SeedTestCountriesAsync();
            await this.SeedTestCitiesAsync();
            await this.SeedTestUserAsync();
            await this.SeedTestRestaurantsAsync();
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

        private async Task SeedTestRestaurantsAsync()
        {
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
    }
}
