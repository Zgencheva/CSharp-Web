namespace VisitACity.Tests.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Data.Models.Enums;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.Controllers;
    using VisitACity.Web.ViewModels.Plans;
    using Xunit;

    public class PlanControllerTests : ServiceTests
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
        private const int PlanId = 1;
        private const string TestCountryName = "Bulgaria";
        private const string TestUserId = "dasdasdasdas-dasdasdas-asdsadas";
        private const string TestUserName = "admin @gmail.com";
        private const string TestUserPassword = "a123456";
        private const string TestUserFirstName = "Admin";
        private const string TestUserLastName = "Admin";
        private const string MuzeikoImageId = "02c5467a-4c9f-4708-86c7-20ca782d8d92";
        private const string HistoryMuseumImageId = "0a4c0be2-e549-49e8-9d4e-d9881080009f";
        private const string SaintSofiaImageId = "0b38c0d5-5a00-4aff-80dc-cfbb692e9db1";
        private const string AuthenticationType = "Test";

        private PlanController controller;

        public PlanControllerTests()
        {
            this.controller = new PlanController(
                this.PlanService,
                this.CitiesService,
                this.CountriesService,
                this.AttractionsService,
                this.RestaurantsService,
                this.userManager);
        }

        private IAttractionsService AttractionsService => this.ServiceProvider
            .GetRequiredService<IAttractionsService>();

        private IPlansService PlanService => this.ServiceProvider
            .GetRequiredService<IPlansService>();

        private ICitiesService CitiesService => this.ServiceProvider
    .GetRequiredService<ICitiesService>();

        private ICountriesService CountriesService => this.ServiceProvider
.GetRequiredService<ICountriesService>();

        private IRestaurantsService RestaurantsService => this.ServiceProvider
.GetRequiredService<IRestaurantsService>();

        private IReviewsService ReviewsService => this.ServiceProvider
.GetRequiredService<IReviewsService>();

        private UserManager<ApplicationUser> userManager => this.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        [Fact]
        public async Task MyPlansShouldGenerateCorrectViewModel()
        {
            await this.SeedDbAsync();

            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                                        new Claim(ClaimTypes.Name, "username"),
                                        new Claim(ClaimTypes.NameIdentifier, TestUserId),
                                        new Claim("name", "John Doe"),
                }, AuthenticationType));
            this.controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user },
            };
            var result = await this.controller.MyPlans();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UserPlansViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task CreateActionShouldGenerateCorrectViewModelWhenCityIdEmpty()
        {
            await this.SeedDbAsync();
            var expectedResult = new CreatePlanInputModel
            {
                FromDate = DateTime.UtcNow,
                ToDate = DateTime.UtcNow,
            };
            var result = await this.controller.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CreatePlanInputModel>(viewResult.ViewData.Model);
            var actualResult = (CreatePlanInputModel)viewResult.ViewData.Model;
            Assert.Equal(expectedResult.FromDate.Date, actualResult.FromDate.Date);
            Assert.Equal(expectedResult.ToDate.Date, actualResult.ToDate.Date);
        }

        //[Fact]
        //public async Task CreateActionShouldGenerateCorrectViewModelWhenCityIdNotEmpty()
        //{
        //    await this.SeedDbAsync();

        //    var expectedResult = new CreatePlanInputModel
        //    {
        //        FromDate = DateTime.UtcNow,
        //        ToDate = DateTime.UtcNow,
        //        CityId = 1,
        //    };
        //    var result = await this.controller.Create();

        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    Assert.IsAssignableFrom<CreatePlanInputModel>(viewResult.ViewData.Model);
        //    var actualResult = (CreatePlanInputModel)viewResult.ViewData.Model;
        //    Assert.Equal(expectedResult.FromDate.Date, actualResult.FromDate.Date);
        //    Assert.Equal(expectedResult.ToDate.Date, actualResult.ToDate.Date);
        //    Assert.Equal(expectedResult.CityId, actualResult.CityId);
        //    Assert.NotEmpty(actualResult.Cities);
        //    Assert.NotEmpty(actualResult.Countries);
        //}

        [Fact]
        public async Task CreateActionShouldCreateUserPlan()
        {
            await this.SeedDbAsync();
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                                        new Claim(ClaimTypes.Name, "username"),
                                        new Claim(ClaimTypes.NameIdentifier, TestUserId),
                                        new Claim("name", "John Doe"),
                },
                AuthenticationType));
            this.controller.ControllerContext = new ControllerContext();
            this.controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            var model = new CreatePlanInputModel
            {
                FromDate = DateTime.UtcNow.AddDays(2),
                ToDate = DateTime.UtcNow.AddDays(7),
                CityId = 2,
                CountryId = 1,
            };

            var result = await this.controller.Create(model);

            var redirectToActionResult =
         Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyPlans", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task CreateActionShouldReturnSameViewWhenModelStateNotValid()
        {
            await this.SeedDbAsync();

            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                                        new Claim(ClaimTypes.Name, "username"),
                                        new Claim(ClaimTypes.NameIdentifier, TestUserId),
                                        new Claim("name", "John Doe"),
                }, AuthenticationType));
            this.controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user },
            };
            var model = new CreatePlanInputModel
            {
                FromDate = DateTime.UtcNow.AddDays(2),
                ToDate = DateTime.UtcNow.AddDays(7),
                CityId = 1,
                CountryId = 1,
            };
            this.controller.ModelState.AddModelError("Test", "wrong test");
            var result = await this.controller.Create(model);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CreatePlanInputModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task AddAttractionToPlanShouldRedirectToCreateActionWhenNoPlan()
        {
            await this.SeedDbAsync();
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            var tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            this.controller = new PlanController(
                this.PlanService,
                this.CitiesService,
                this.CountriesService,
                this.AttractionsService,
                this.RestaurantsService,
                this.userManager)
            {
                TempData = tempData,
            };
            var result = await this.controller.AddAttractionToPlan("aaa", 0);
            Assert.Equal(this.controller.TempData["Message"], TempDataMessageConstants.Plan.NoPlansInTheCity);
            var redirectToActionResult =
                 Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Create", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task AddAttractionToPlanShouldRedirectToMyPlansUponSuccess()
        {
            await this.SeedDbAsync();
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            this.controller = new PlanController(
                this.PlanService,
                this.CitiesService,
                this.CountriesService,
                this.AttractionsService,
                this.RestaurantsService,
                this.userManager)
            {
                TempData = tempData,
            };
            var result = await this.controller.AddAttractionToPlan(AttractionId1, PlanId);
            Assert.Equal(this.controller.TempData["Message"], TempDataMessageConstants.Attraction.AttractionAddedToPlan);
            var redirectToActionResult =
                 Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyPlans", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task AddRestaurantToPlanShouldRedirectToCreateActionWhenNoPlan()
        {
            await this.SeedDbAsync();
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            var tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            this.controller = new PlanController(
                this.PlanService,
                this.CitiesService,
                this.CountriesService,
                this.AttractionsService,
                this.RestaurantsService,
                this.userManager)
            {
                TempData = tempData,
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                                        new Claim(ClaimTypes.Name, "username"),
                                        new Claim(ClaimTypes.NameIdentifier, TestUserId),
                                        new Claim("name", "John Doe"),
                }, AuthenticationType));
            this.controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user },
            };

            var result = await this.controller.AddRestaurantToPlan(2);
            Assert.Equal(this.controller.TempData["Message"], TempDataMessageConstants.Plan.NoPlansInTheCity);
            var redirectToActionResult =
                 Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Create", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task AddRestaurantToPlanShouldRedirectToMyPlansUponSuccess()
        {
            await this.SeedDbAsync();
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            var tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            this.controller = new PlanController(
                this.PlanService,
                this.CitiesService,
                this.CountriesService,
                this.AttractionsService,
                this.RestaurantsService,
                this.userManager)
            {
                TempData = tempData,
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(
               new Claim[]
               {
                                        new Claim(ClaimTypes.Name, "username"),
                                        new Claim(ClaimTypes.NameIdentifier, TestUserId),
                                        new Claim("name", "John Doe"),
               }, AuthenticationType));
            this.controller.ControllerContext = new ControllerContext();
            this.controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var result = await this.controller.AddRestaurantToPlan(1);
            Assert.Equal(
                TempDataMessageConstants.Restaurant.RestaurantAddedToPlan,
                this.controller.TempData["Message"]);
            var redirectToActionResult =
                 Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyPlans", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteAttractionFromPlanShouldRedirectToMyPlansUponSuccess()
        {
            await this.SeedDbAsync();
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            var tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            this.controller = new PlanController(
                this.PlanService,
                this.CitiesService,
                this.CountriesService,
                this.AttractionsService,
                this.RestaurantsService,
                this.userManager)
            {
                TempData = tempData,
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(
               new Claim[]
               {
                                        new Claim(ClaimTypes.Name, "username"),
                                        new Claim(ClaimTypes.NameIdentifier, TestUserId),
                                        new Claim("name", "John Doe"),
               }, AuthenticationType));
            this.controller.ControllerContext = new ControllerContext();
            this.controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            await this.PlanService.AddAttractionToPlanAsync(AttractionId1, PlanId);
            var result = await this.controller.DeleteAttractionFromPlan(AttractionId1);
            Assert.Equal(
                TempDataMessageConstants.Attraction.AttractionDeleted,
                this.controller.TempData["Message"]);
            var redirectToActionResult =
                 Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyPlans", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteRestaurantFromPlanShouldRedirectToMyPlansUponSuccess()
        {
            await this.SeedDbAsync();
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            var tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            this.controller = new PlanController(
                this.PlanService,
                this.CitiesService,
                this.CountriesService,
                this.AttractionsService,
                this.RestaurantsService,
                this.userManager)
            {
                TempData = tempData,
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(
               new Claim[]
               {
                                        new Claim(ClaimTypes.Name, "username"),
                                        new Claim(ClaimTypes.NameIdentifier, TestUserId),
                                        new Claim("name", "John Doe"),
               }, AuthenticationType));
            this.controller.ControllerContext = new ControllerContext();
            this.controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            await this.PlanService.AddRestaurantToPlanAsync(1, PlanId);
            var result = await this.controller.DeleteRestaurantFromPlan(1);

            Assert.Equal(
                TempDataMessageConstants.Restaurant.RestaurantDeleted,
                this.controller.TempData["Message"]);
            var redirectToActionResult =
                 Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyPlans", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteShouldRedirectToMyPlansUponSuccess()
        {
            await this.SeedDbAsync();
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            var tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            this.controller = new PlanController(
                this.PlanService,
                this.CitiesService,
                this.CountriesService,
                this.AttractionsService,
                this.RestaurantsService,
                this.userManager)
            {
                TempData = tempData,
            };

            var result = await this.controller.Delete(PlanId);

            Assert.Equal(
                TempDataMessageConstants.Plan.PlanDeleted,
                this.controller.TempData["Message"]);
            var redirectToActionResult =
                 Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyPlans", redirectToActionResult.ActionName);
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
                UserId = TestUserId,
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
