namespace VisitACity.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Data.Models.Enums;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Attractions;
    using VisitACity.Web.ViewModels.Images;
    using VisitACity.Web.ViewModels.Plans;
    using VisitACity.Web.ViewModels.Restaurants;
    using Xunit;

    public class PlansServiceTests : ServiceTests
    {
        private const string Sofia = "Sofia";
        private const string AttractionId1 = "aaa";
        private const string AttractionId2 = "bbb";
        private const string AttractionId3 = "ccc";
        private const string Plovdiv = "Plovdiv";
        private const string Varna = "Varna";
        private const string Ruse = "Ruse";
        private const string ImagesExtension = "jpg";
        private const int TestCountryId = 1;
        private const int TestRestaurantId = 1;
        private const int PlanId = 1;
        private const int InvalidPlanId = 44;
        private const int InvalidCountryId = 66;
        private const int InvalidCityId = 66;
        private const string InvalidAttractionId = "ddd";
        private const int InvalidRestaurantId = 66;
        private const int InvaliRestaurantId = 44;
        private const string TestCountryName = "Bulgaria";
        private const string TestUserId = "dasdasdasdas-dasdasdas-asdsadas";
        private const string InvalidUserId = "Inavlid-user-id";
        private const string TestUserName = "admin @gmail.com";
        private const string TestUserPassword = "a123456";
        private const string TestUserFirstName = "Admin";
        private const string TestUserLastName = "Admin";
        private const string MuzeikoImageId = "02c5467a-4c9f-4708-86c7-20ca782d8d92";
        private const string HistoryMuseumImageId = "0a4c0be2-e549-49e8-9d4e-d9881080009f";
        private const string SaintSofiaImageId = "0b38c0d5-5a00-4aff-80dc-cfbb692e9db1";

        private IPlansService PlansService => this.ServiceProvider.GetRequiredService<IPlansService>();

        [Fact]
        public async Task AddAttractionToPlanAsyncShouldAdAttractionToPlanAndReturnTrue()
        {
            await this.SeedDbAsync();
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            var result = await this.PlansService.AddAttractionToPlanAsync(AttractionId1, PlanId);
            Assert.True(result);
            Assert.True(plan.Attractions.Count == 1);
            Assert.Contains(plan.Attractions, x => x.Id == AttractionId1);
        }

        [Fact]
        public async Task AddAttractionToPlanAsyncShouldReturnFalseInCaseUserDoesNotHavePlan()
        {
            await this.SeedDbAsync();
            var result = await this.PlansService.AddAttractionToPlanAsync(AttractionId1, InvalidPlanId);
            Assert.False(result);
        }

        [Fact]
        public async Task AddAttractionToPlanAsyncShouldThrowExceptionWhenInvalidAttractionIdPassed()
        {
            await this.SeedDbAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.PlansService.AddAttractionToPlanAsync(InvalidAttractionId, PlanId));
            Assert.Equal(ExceptionMessages.Attraction.InvalidAttraction, exception.Message);
        }

        [Fact]
        public async Task AddRestaurantToPlanAsyncShouldAddRestaurantToPlanAndReturnTrue()
        {
            await this.SeedDbAsync();
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            var result = await this.PlansService.AddRestaurantToPlanAsync(TestRestaurantId, PlanId);
            Assert.True(result);
            Assert.True(plan.Restaurants.Count == 1);
            Assert.Contains(plan.Restaurants, x => x.Id == TestRestaurantId);
        }

        [Fact]
        public async Task AddRestaurantToPlanAsyncShouldReturnFalseInCaseUserDoesNotHavePlan()
        {
            await this.SeedDbAsync();
            var result = await this.PlansService.AddRestaurantToPlanAsync(TestRestaurantId, InvalidPlanId);
            Assert.False(result);
        }

        [Fact]
        public async Task AddRestaurantToPlanAsyncShouldThrowExceptionWhenInvaliRestaurantIdPassed()
        {
            await this.SeedDbAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.PlansService.AddRestaurantToPlanAsync(InvaliRestaurantId, PlanId));
            Assert.Equal(ExceptionMessages.Restaurant.InvalidRestaurant, exception.Message);
        }

        [Fact]
        public async Task CreateAsyncShouldAddPlanToDb()
        {
            await this.SeedDbAsync();
            var plan = new CreatePlanInputModel
            {
                FromDate = DateTime.Now.AddDays(5),
                ToDate = DateTime.Now.AddDays(10),
                CityId = 2,
                CountryId = TestCountryId,
            };
            await this.PlansService.CreateAsync(plan, TestUserId);
            Assert.Equal(2, this.DbContext.Plans.Count());
            Assert.Contains(this.DbContext.Plans, x => x.CityId == 2);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenInvaliCountryIdPassed()
        {
            await this.SeedDbAsync();
            var plan = new CreatePlanInputModel
            {
                FromDate = DateTime.Now.AddDays(5),
                ToDate = DateTime.Now.AddDays(10),
                CityId = 2,
                CountryId = InvalidCountryId,
            };
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.PlansService.CreateAsync(plan, TestUserId));
            Assert.Equal(ExceptionMessages.Country.NotExists, exception.Message);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenInvaliCityIdPassed()
        {
            await this.SeedDbAsync();
            var plan = new CreatePlanInputModel
            {
                FromDate = DateTime.Now.AddDays(5),
                ToDate = DateTime.Now.AddDays(10),
                CityId = InvalidCityId,
                CountryId = TestCountryId,
            };
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.PlansService.CreateAsync(plan, TestUserId));
            Assert.Equal(ExceptionMessages.City.NotExists, exception.Message);
        }

        [Fact]
        public async Task DeleteAsyncShouldMarkPlanAsDeleted()
        {
            await this.SeedDbAsync();
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            await this.PlansService.DeleteAsync(PlanId);
            Assert.True(plan.IsDeleted);
        }

        [Fact]
        public async Task DeleteAsyncShouldThrowExceptionWhenInvaliPlanIdPassed()
        {
            await this.SeedDbAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.PlansService.DeleteAsync(InvalidPlanId));
            Assert.Equal(ExceptionMessages.Plan.NotExists, exception.Message);
        }

        [Fact]
        public async Task DeleteAttractionFromPlanAsyncShouldRemoveAttractionFromPlan()
        {
            await this.SeedDbAsync();
            var attraction = await this.DbContext.Attractions.FindAsync(AttractionId1);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            plan.Attractions.Add(attraction);
            await this.DbContext.SaveChangesAsync();
            Assert.Single(plan.Attractions);
            await this.PlansService.DeleteAttractionFromPlanAsync(AttractionId1, PlanId);

            Assert.Empty(plan.Attractions);
        }

        [Fact]
        public async Task DeleteAttractionFromPlanAsyncShouldThrowExceptionWhenInvaliPlanIdPassed()
        {
            await this.SeedDbAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.PlansService.DeleteAttractionFromPlanAsync(AttractionId1, InvalidPlanId));
            Assert.Equal(ExceptionMessages.Plan.NotExists, exception.Message);
        }

        [Fact]
        public async Task DeleteAttractionFromPlanAsyncShouldThrowExceptionWhenInvaliAttractionIdPassed()
        {
            await this.SeedDbAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.PlansService.DeleteAttractionFromPlanAsync(InvalidAttractionId, PlanId));
            Assert.Equal(ExceptionMessages.Plan.NotExistingAttraction, exception.Message);
        }

        [Fact]
        public async Task DeleteRestaurantFromPlanAsyncShouldRemoveRestaurantFromPlan()
        {
            await this.SeedDbAsync();
            var restaurant = await this.DbContext.Restaurants.FindAsync(TestRestaurantId);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            plan.Restaurants.Add(restaurant);
            await this.DbContext.SaveChangesAsync();
            Assert.Single(plan.Restaurants);
            await this.PlansService.DeleteRestaurantFromPlanAsync(TestRestaurantId, PlanId);

            Assert.Empty(plan.Restaurants);
        }

        [Fact]
        public async Task DeleteRestaurantFromPlanAsyncThrowExceptionWhenInvaliPlanIdPassed()
        {
            await this.SeedDbAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.PlansService.DeleteRestaurantFromPlanAsync(TestRestaurantId, InvalidPlanId));
            Assert.Equal(ExceptionMessages.Plan.NotExists, exception.Message);
        }

        [Fact]
        public async Task DeleteRestaurantFromPlanAsyncShouldThrowExceptionWhenInvaliRestairantIdPassed()
        {
            await this.SeedDbAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.PlansService.DeleteRestaurantFromPlanAsync(InvalidRestaurantId, PlanId));
            Assert.Equal(ExceptionMessages.Plan.NotExistingRestaurant, exception.Message);
        }

        [Fact]
        public async Task DoesAttractionExistShouldReturnTrueIfExists()
        {
            await this.SeedDbAsync();
            var attraction = await this.DbContext.Attractions.FindAsync(AttractionId1);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            plan.Attractions.Add(attraction);
            await this.DbContext.SaveChangesAsync();

            var result = await this.PlansService.DoesAttractionExist(AttractionId1, PlanId);

            Assert.True(result);
        }

        [Fact]
        public async Task DoesAttractionExistShouldReturnFalseIfNotExists()
        {
            await this.SeedDbAsync();

            var result = await this.PlansService.DoesAttractionExist(AttractionId1, PlanId);

            Assert.False(result);
        }

        [Fact]
        public async Task DoesRestaurantExistShouldReturnTrueIfExists()
        {
            await this.SeedDbAsync();
            var restaurant = await this.DbContext.Restaurants.FindAsync(TestRestaurantId);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            plan.Restaurants.Add(restaurant);
            await this.DbContext.SaveChangesAsync();

            var result = await this.PlansService.DoesRestaurantExist(TestRestaurantId, PlanId);

            Assert.True(result);
        }

        [Fact]
        public async Task DoesRestaurantExistShouldReturnFalseIfNotExists()
        {
            await this.SeedDbAsync();

            var result = await this.PlansService.DoesRestaurantExist(TestRestaurantId, PlanId);

            Assert.False(result);
        }

        [Fact]
        public async Task DoesUserHavePlanInTheCityShouldReturnTrueIfHas()
        {
            await this.SeedDbAsync();
            var user = await this.DbContext.Users.FindAsync(TestUserId);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            user.Plans.Add(plan);
            await this.DbContext.SaveChangesAsync();

            var result = await this.PlansService.DoesUserHavePlanInTheCity(TestUserId, Sofia);

            Assert.True(result);
        }

        [Fact]
        public async Task DoesUserHavePlanInTheCityShouldReturnFalseIfHasNot()
        {
            await this.SeedDbAsync();
            var user = await this.DbContext.Users.FindAsync(TestUserId);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            user.Plans.Add(plan);
            await this.DbContext.SaveChangesAsync();

            var result = await this.PlansService.DoesUserHavePlanInTheCity(TestUserId, Plovdiv);

            Assert.False(result);
        }

        [Fact]
        public async Task GetUserPlanIdAsyncShouldReturnUserPlanId()
        {
            await this.SeedDbAsync();
            var user = await this.DbContext.Users.FindAsync(TestUserId);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            user.Plans.Add(plan);
            await this.DbContext.SaveChangesAsync();

            var actualResult = await this.PlansService.GetUserPlanIdAsync(Sofia, TestUserId);

            var exptectedResult = PlanId;

            Assert.Equal(exptectedResult, actualResult);
        }

        [Fact]
        public async Task GetUserPlanIdAsyncShouldThrowExceptionWhenInvalidUserIdPassed()
        {
            await this.SeedDbAsync();
            var user = await this.DbContext.Users.FindAsync(TestUserId);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            user.Plans.Add(plan);
            await this.DbContext.SaveChangesAsync();

            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
             async () =>
             await this.PlansService.GetUserPlanIdAsync(Sofia, InvalidUserId));
            Assert.Equal(ExceptionMessages.Plan.NotExists, exception.Message);
        }

        [Fact]
        public async Task GetUserPlanIdAsyncShouldThrowExceptionWhenInvalidCityIdPassed()
        {
            await this.SeedDbAsync();
            var user = await this.DbContext.Users.FindAsync(TestUserId);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            user.Plans.Add(plan);
            await this.DbContext.SaveChangesAsync();

            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
             async () =>
             await this.PlansService.GetUserPlanIdAsync("Uganda", TestUserId));
            Assert.Equal(ExceptionMessages.Plan.NotExists, exception.Message);
        }

        [Fact]
        public async Task GetUpcomingUserPlansAsyncShouldReturnCollectionOfPlanViewModel()
        {
            await this.SeedDbAsync();
            var user = await this.DbContext.Users.FindAsync(TestUserId);
            var attraction = await this.DbContext.Attractions.FindAsync(AttractionId1);
            var image = attraction.Image;
            var restaurant = await this.DbContext.Restaurants.FindAsync(1);
            var country = await this.DbContext.Countries.FindAsync(1);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            plan.Attractions.Add(attraction);
            plan.Restaurants.Add(restaurant);
            await this.DbContext.SaveChangesAsync();
            user.Plans.Add(plan);
            await this.DbContext.SaveChangesAsync();

            var expectedResult = new List<PlanViewModel>()
            {
                new PlanViewModel
                {
                    Id = PlanId,
                    CityCountryName = country.Name,
                    CityName = Sofia,
                    CityId = 1,
                    Days = 4,
                    FromDate = plan.FromDate,
                    ToDate = plan.ToDate,
                    Attractions = new List<AttractionViewModel>
                    {
                        new AttractionViewModel
                        {
                            Id = AttractionId1,
                            Name = attraction.Name,
                            Type = attraction.Type.ToString(),
                            Description = attraction.Description,
                            Image = new ImageViewModel
                            {
                                Id = image.Id,
                                Extension = image.Extension,
                            },
                        },
                    },
                    Restaurants = new List<RestaurantViewModel>
                    {
                        new RestaurantViewModel
                        {
                            Id = 1,
                            Name = restaurant.Name,
                            CityName = Sofia,
                        },
                    },
                },
            };

            var actualResult = await this.PlansService.GetUpcomingUserPlansAsync(TestUserId);
            Assert.Collection(
               actualResult,
               el1 =>
               {
                   Assert.Equal(expectedResult[0].Id, el1.Id);
                   Assert.Equal(expectedResult[0].CityCountryName, el1.CityCountryName);
                   Assert.Equal(expectedResult[0].CityId, el1.CityId);
                   Assert.Equal(expectedResult[0].CityName, el1.CityName);
                   Assert.Equal(expectedResult[0].Days, el1.Days);
                   Assert.Equal(expectedResult[0].FromDate, el1.FromDate);
                   Assert.Equal(expectedResult[0].ToDate, el1.ToDate);
                   Assert.Equal(expectedResult[0].Attractions.First().Id, el1.Attractions.First().Id);
                   Assert.Equal(expectedResult[0].Attractions.First().Name, el1.Attractions.First().Name);
                   Assert.Equal(expectedResult[0].Attractions.First().Description, el1.Attractions.First().Description);
                   Assert.Equal(expectedResult[0].Attractions.First().Type, el1.Attractions.First().Type);
                   Assert.Equal(expectedResult[0].Attractions.First().Image.Id, el1.Attractions.First().Image.Id);
                   Assert.Equal(expectedResult[0].Attractions.First().Image.Extension, el1.Attractions.First().Image.Extension);
                   Assert.Equal(expectedResult[0].Attractions.Count(), el1.Attractions.Count());
                   Assert.Single(el1.Attractions);
                   Assert.Equal(expectedResult[0].Restaurants.First().Id, el1.Restaurants.First().Id);
                   Assert.Equal(expectedResult[0].Restaurants.First().CityName, el1.Restaurants.First().CityName);
                   Assert.Equal(expectedResult[0].Restaurants.First().Name, el1.Restaurants.First().Name);
                   Assert.Equal(expectedResult[0].Restaurants.Count(), el1.Restaurants.Count());
                   Assert.Single(el1.Restaurants);
               });

            Assert.Single(actualResult);

            Assert.Equal(expectedResult.Count, actualResult.Count());
        }

        [Fact]
        public async Task GetUserPlansAsyncShouldReturnCollectionOfPlanViewModel()
        {
            await this.SeedDbAsync();
            var user = await this.DbContext.Users.FindAsync(TestUserId);
            var attraction = await this.DbContext.Attractions.FindAsync(AttractionId1);
            var image = attraction.Image;
            var restaurant = await this.DbContext.Restaurants.FindAsync(1);
            var country = await this.DbContext.Countries.FindAsync(1);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            plan.Attractions.Add(attraction);
            plan.Restaurants.Add(restaurant);
            await this.DbContext.SaveChangesAsync();
            user.Plans.Add(plan);
            await this.DbContext.SaveChangesAsync();

            var expectedResult = new List<PlanViewModel>()
            {
                new PlanViewModel
                {
                    Id = PlanId,
                    CityCountryName = country.Name,
                    CityName = "Sofia",
                    CityId = 1,
                    Days = 4,
                    FromDate = plan.FromDate,
                    ToDate = plan.ToDate,
                    Attractions = new List<AttractionViewModel>
                    {
                        new AttractionViewModel
                        {
                            Id = AttractionId1,
                            Name = attraction.Name,
                            Type = attraction.Type.ToString(),
                            Description = attraction.Description,
                            Image = new ImageViewModel
                            {
                                Id = image.Id,
                                Extension = image.Extension,
                            },
                        },
                    },
                    Restaurants = new List<RestaurantViewModel>
                    {
                        new RestaurantViewModel
                        {
                            Id = 1,
                            Name = restaurant.Name,
                            CityName = Sofia,
                        },
                    },
                },
            };

            var actualResult = await this.PlansService.GetUserPlansAsync(TestUserId);
            Assert.Collection(
               actualResult,
               el1 =>
               {
                   Assert.Equal(expectedResult[0].Id, el1.Id);
                   Assert.Equal(expectedResult[0].CityCountryName, el1.CityCountryName);
                   Assert.Equal(expectedResult[0].CityId, el1.CityId);
                   Assert.Equal(expectedResult[0].CityName, el1.CityName);
                   Assert.Equal(expectedResult[0].Days, el1.Days);
                   Assert.Equal(expectedResult[0].FromDate, el1.FromDate);
                   Assert.Equal(expectedResult[0].ToDate, el1.ToDate);
                   Assert.Equal(expectedResult[0].Attractions.First().Id, el1.Attractions.First().Id);
                   Assert.Equal(expectedResult[0].Attractions.First().Name, el1.Attractions.First().Name);
                   Assert.Equal(expectedResult[0].Attractions.First().Description, el1.Attractions.First().Description);
                   Assert.Equal(expectedResult[0].Attractions.First().Type, el1.Attractions.First().Type);
                   Assert.Equal(expectedResult[0].Attractions.First().Image.Id, el1.Attractions.First().Image.Id);
                   Assert.Equal(expectedResult[0].Attractions.First().Image.Extension, el1.Attractions.First().Image.Extension);
                   Assert.Equal(expectedResult[0].Attractions.Count(), el1.Attractions.Count());
                   Assert.Single(el1.Attractions);
                   Assert.Equal(expectedResult[0].Restaurants.First().Id, el1.Restaurants.First().Id);
                   Assert.Equal(expectedResult[0].Restaurants.First().CityName, el1.Restaurants.First().CityName);
                   Assert.Equal(expectedResult[0].Restaurants.First().Name, el1.Restaurants.First().Name);
                   Assert.Equal(expectedResult[0].Restaurants.Count(), el1.Restaurants.Count());
                   Assert.Single(el1.Restaurants);
               });

            Assert.Single(actualResult);

            Assert.Equal(expectedResult.Count, actualResult.Count);
        }

        [Fact]
        public async Task GetUserUpcomingPlansByCityAsyncShouldReturnCollection()
        {
            await this.SeedDbAsync();
            var user = await this.DbContext.Users.FindAsync(TestUserId);
            var attraction = await this.DbContext.Attractions.FindAsync(AttractionId1);
            var image = attraction.Image;
            var restaurant = await this.DbContext.Restaurants.FindAsync(1);
            var country = await this.DbContext.Countries.FindAsync(1);
            var plan = await this.DbContext.Plans.FindAsync(PlanId);
            plan.Attractions.Add(attraction);
            plan.Restaurants.Add(restaurant);
            await this.DbContext.SaveChangesAsync();
            user.Plans.Add(plan);
            await this.DbContext.SaveChangesAsync();

            var expectedResult = new PlanQueryModel
            {
                Id = PlanId,
            };

            var actualResult = await this.PlansService.GetUserUpcomingPlansByCityAsync<PlanQueryModel>(Sofia, TestUserId);

            Assert.Equal(expectedResult.Id, actualResult.Id);
        }

        private async Task SeedDbAsync()
        {
            await this.SeedTestCountriesAsync();
            await this.SeedTestCitiesAsync();
            await this.SeedTestUserAsync();
            await this.SeedTestImagesAsync();
            await this.SeedTestAttractionsAsync();
            await this.SeedTestRestaurantsAsync();
            await this.SeedTestPlanAsync();
        }

        private async Task SeedTestPlanAsync()
        {
            this.DbContext.Plans.Add(new Plan
            {
                Id = PlanId,
                UserId = TestUserName,
                CityId = 1,
                FromDate = DateTime.UtcNow.AddDays(2),
                ToDate = DateTime.UtcNow.AddDays(6),
            });
            await this.DbContext.SaveChangesAsync();
        }

        private async Task SeedTestImagesAsync()
        {
            this.DbContext.Images.Add(new Image { Id = MuzeikoImageId, Extension = ImagesExtension });
            this.DbContext.Images.Add(new Image { Id = HistoryMuseumImageId, Extension = ImagesExtension });
            this.DbContext.Images.Add(new Image { Id = SaintSofiaImageId, Extension = ImagesExtension });
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

        private async Task SeedTestAttractionsAsync()
        {
            this.DbContext.Attractions.Add(new Attraction
            {
                Id = AttractionId1,
                Name = "Muzeiko",
                Type = (AttractionType)30,
                AttractionUrl = "https://www.muzeiko.bg/bg",
                Price = 10,
                Address = "3 Professor Boyan Kamenov, 1756 Studentski",
                CityId = 1,
                Image = await this.DbContext.Images.FindAsync(MuzeikoImageId),
                Description = "Children's museum featuring interactive science-related exhibits, a cafe & a gift shop.",
            });
            this.DbContext.Attractions.Add(new Attraction
            {
                Id = AttractionId2,
                Name = "Nacional History Museum",
                Type = (AttractionType)30,
                AttractionUrl = "https://historymuseum.org/",
                Price = 10,
                Address = "16 Vitoshko lale",
                CityId = 1,
                Image = await this.DbContext.Images.FindAsync(HistoryMuseumImageId),
                Description = "The National Historical Museum in Sofia is Bulgaria's largest museum. It was founded on 5 May 1973. A new representative exhibition was opened in the building of the Court of Justice on 2 March 1984, to commemorate the 13th centenary of the Bulgarian state",
            });
            this.DbContext.Attractions.Add(new Attraction
            {
                Id = AttractionId3,
                Name = "Sait Sofia",
                Type = (AttractionType)30,
                AttractionUrl = "https://stolica.bg/sofia-tur/hramove/tsarkva-sveta-sofiya",
                Price = 0,
                Address = "2 Paris",
                CityId = 2,
                Image = await this.DbContext.Images.FindAsync(SaintSofiaImageId),
                Description = "The church was built on the site of several earlier churches from the 4th century, and places of worship dating back to the days when it was the necropolis of the Roman town of Serdica.",
            });
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
