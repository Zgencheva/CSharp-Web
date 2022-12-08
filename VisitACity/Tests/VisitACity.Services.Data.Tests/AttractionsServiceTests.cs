namespace VisitACity.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using VisitACity.Common;
    using VisitACity.Data.Models;
    using VisitACity.Data.Models.Enums;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Attractions;
    using VisitACity.Web.ViewModels.Attractions;
    using VisitACity.Web.ViewModels.Cities;
    using VisitACity.Web.ViewModels.Images;
    using Xunit;

    public class AttractionsServiceTests : ServiceTests
    {
        private const string Sofia = "Sofia";
        private const string Plovdiv = "Plovdiv";
        private const string Varna = "Varna";
        private const string Ruse = "Ruse";
        private const string InvalidCity = "Uganda";
        private const int TestCountryId = 100;
        private const string TestCountryName = "Bulgaria";
        private const string MuzeikoImageId = "02c5467a-4c9f-4708-86c7-20ca782d8d92";
        private const string HistoryMuseumImageId = "0a4c0be2-e549-49e8-9d4e-d9881080009f";
        private const string SaintSofiaImageId = "0b38c0d5-5a00-4aff-80dc-cfbb692e9db1";
        private const string ImagesExtension = "jpg";
        private const string TestImagePath = "testImage.jpg";
        private const string TestImageContentType = "image/jpg";
        private const string TestUserId = "dasdasdasdas-dasdasdas-asdsadas";
        private const string TestUserName = "admin @gmail.com";
        private const string TestUserPassword = "a123456";
        private const string TestUserFirstName = "Admin";
        private const string TestUserLastName = "Admin";

        private IAttractionsService AttractionsService => this.ServiceProvider.GetRequiredService<IAttractionsService>();

        [Fact]
        public async Task GetCountShouldReturnsAttractionsCount()
        {
            await this.SeedTestAttractionsAsync();

            Assert.Equal(3, this.AttractionsService.GetCount());
        }

        [Fact]
        public async Task GetBestAttractionsAsyncShouldReturnViewModelCollection()
        {
            await this.SeedTestAttractionsAsync();

            var expectedResult = new List<AttractionViewModel>()
            {
                new AttractionViewModel
            {
                Id = "aaa",
                Name = "Muzeiko",
                Type = "CulturalTours",
                Price = 10,
                Address = "3 Professor Boyan Kamenov, 1756 Studentski",
                CityId = 1,
                CityName = Sofia,
                Reviews = 0,
                Image = new ImageViewModel { Id = MuzeikoImageId, Extension = ImagesExtension },
                Description = "Children's museum featuring interactive science-related exhibits, a cafe & a gift shop.",
                UserPlan = null,
            },
                new AttractionViewModel
                {
                Id = "bbb",
                Name = "Nacional History Museum",
                Type = "CulturalTours",
                Price = 10,
                Address = "16 Vitoshko lale",
                CityId = 1,
                CityName = Sofia,
                Reviews = 0,
                UserPlan = null,
                Image = new ImageViewModel { Id = HistoryMuseumImageId, Extension = ImagesExtension },
                Description = "The National Historical Museum in Sofia is Bulgaria's largest museum. It was founded on 5 May 1973. A new representative exhibition was opened in the building of the Court of Justice on 2 March 1984, to commemorate the 13th centenary of the Bulgarian state",
                },
                new AttractionViewModel
                {
                Id = "ccc",
                Name = "Sait Sofia",
                Type = "CulturalTours",
                Price = 0,
                Address = "2 Paris",
                CityId = 2,
                CityName = Plovdiv,
                Reviews = 0,
                UserPlan = null,
                Image = new ImageViewModel { Id = SaintSofiaImageId, Extension = ImagesExtension },
                Description = "The church was built on the site of several earlier churches from the 4th century, and places of worship dating back to the days when it was the necropolis of the Roman town of Serdica.",
                },
            };

            var actualResult = await this.AttractionsService.GetBestAttractionsAsync<AttractionViewModel>(1, 6);

            Assert.Collection(
                actualResult,
                el1 =>
                {
                    Assert.Equal(expectedResult[2].Id, el1.Id);
                    Assert.Equal(expectedResult[2].Name, el1.Name);
                    Assert.Equal(expectedResult[2].Description, el1.Description);
                    Assert.Equal(expectedResult[2].Address, el1.Address);
                    Assert.Equal(expectedResult[2].CityName, el1.CityName);
                    Assert.Equal(expectedResult[2].Image.Id, el1.Image.Id);
                    Assert.Equal(expectedResult[2].Image.Extension, el1.Image.Extension);
                    Assert.Equal(expectedResult[2].Type, el1.Type);
                    Assert.Equal(expectedResult[2].Reviews, el1.Reviews);
                    Assert.Equal(expectedResult[2].UserPlan, el1.UserPlan);
                },
                el2 =>
                 {
                     Assert.Equal(expectedResult[1].Id, el2.Id);
                     Assert.Equal(expectedResult[1].Name, el2.Name);
                     Assert.Equal(expectedResult[1].Description, el2.Description);
                     Assert.Equal(expectedResult[1].Address, el2.Address);
                     Assert.Equal(expectedResult[1].CityName, el2.CityName);
                     Assert.Equal(expectedResult[1].Image.Id, el2.Image.Id);
                     Assert.Equal(expectedResult[1].Image.Extension, el2.Image.Extension);
                     Assert.Equal(expectedResult[1].Type, el2.Type);
                     Assert.Equal(expectedResult[1].Reviews, el2.Reviews);
                     Assert.Equal(expectedResult[1].UserPlan, el2.UserPlan);
                 },
                el3 =>
                {
                    Assert.Equal(expectedResult[0].Id, el3.Id);
                    Assert.Equal(expectedResult[0].Name, el3.Name);
                    Assert.Equal(expectedResult[0].Description, el3.Description);
                    Assert.Equal(expectedResult[0].Address, el3.Address);
                    Assert.Equal(expectedResult[0].CityName, el3.CityName);
                    Assert.Equal(expectedResult[0].Image.Id, el3.Image.Id);
                    Assert.Equal(expectedResult[0].Image.Extension, el3.Image.Extension);
                    Assert.Equal(expectedResult[0].Type, el3.Type);
                    Assert.Equal(expectedResult[0].Reviews, el3.Reviews);
                    Assert.Equal(expectedResult[0].UserPlan, el3.UserPlan);
                });

            Assert.Equal(3, actualResult.Count());

            Assert.Equal(expectedResult.Count, actualResult.Count());
        }

        [Fact]
        public async Task GetByCityAsyncShouldReturnViewModelCollectionByCity()
        {
            await this.SeedTestAttractionsAsync();

            var expectedResult = new List<AttractionViewModel>()
            {
                new AttractionViewModel
            {
                Id = "aaa",
                Name = "Muzeiko",
                Type = "CulturalTours",
                Price = 10,
                Address = "3 Professor Boyan Kamenov, 1756 Studentski",
                CityId = 1,
                CityName = Sofia,
                Reviews = 0,
                Image = new ImageViewModel { Id = MuzeikoImageId, Extension = ImagesExtension },
                Description = "Children's museum featuring interactive science-related exhibits, a cafe & a gift shop.",
                UserPlan = null,
            },
                new AttractionViewModel
                {
                Id = "bbb",
                Name = "Nacional History Museum",
                Type = "CulturalTours",
                Price = 10,
                Address = "16 Vitoshko lale",
                CityId = 1,
                CityName = Sofia,
                Reviews = 0,
                UserPlan = null,
                Image = new ImageViewModel { Id = HistoryMuseumImageId, Extension = ImagesExtension },
                Description = "The National Historical Museum in Sofia is Bulgaria's largest museum. It was founded on 5 May 1973. A new representative exhibition was opened in the building of the Court of Justice on 2 March 1984, to commemorate the 13th centenary of the Bulgarian state",
                },
            };

            var actualResult = await this.AttractionsService.GetByCityAsync<AttractionViewModel>(Sofia, 1, 6);

            Assert.Collection(
                actualResult,
                el1 =>
                {
                    Assert.Equal(expectedResult[1].Id, el1.Id);
                    Assert.Equal(expectedResult[1].Name, el1.Name);
                    Assert.Equal(expectedResult[1].Description, el1.Description);
                    Assert.Equal(expectedResult[1].Address, el1.Address);
                    Assert.Equal(expectedResult[1].CityName, el1.CityName);
                    Assert.Equal(expectedResult[1].Image.Id, el1.Image.Id);
                    Assert.Equal(expectedResult[1].Image.Extension, el1.Image.Extension);
                    Assert.Equal(expectedResult[1].Type, el1.Type);
                    Assert.Equal(expectedResult[1].Reviews, el1.Reviews);
                    Assert.Equal(expectedResult[1].UserPlan, el1.UserPlan);
                },
                el2 =>
                {
                    Assert.Equal(expectedResult[0].Id, el2.Id);
                    Assert.Equal(expectedResult[0].Name, el2.Name);
                    Assert.Equal(expectedResult[0].Description, el2.Description);
                    Assert.Equal(expectedResult[0].Address, el2.Address);
                    Assert.Equal(expectedResult[0].CityName, el2.CityName);
                    Assert.Equal(expectedResult[0].Image.Id, el2.Image.Id);
                    Assert.Equal(expectedResult[0].Image.Extension, el2.Image.Extension);
                    Assert.Equal(expectedResult[0].Type, el2.Type);
                    Assert.Equal(expectedResult[0].Reviews, el2.Reviews);
                    Assert.Equal(expectedResult[0].UserPlan, el2.UserPlan);
                });

            Assert.Equal(2, actualResult.Count());

            Assert.Equal(expectedResult.Count, actualResult.Count());
        }

        [Fact]
        public async Task GetByCityAsyncShouldThrowExceptionWhenNullCityPassed()
        {
            await this.SeedTestAttractionsAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.AttractionsService.GetByCityAsync<AttractionViewModel>(null, 1, 6));
            Assert.Equal(ExceptionMessages.City.NotExists, exception.Message);
        }

        [Fact]
        public async Task GetByCityAsyncShouldThrowExceptionWhenInvalidCityPassed()
        {
            await this.SeedTestAttractionsAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.AttractionsService.GetByCityAsync<AttractionViewModel>(InvalidCity, 1, 6));
            Assert.Equal(ExceptionMessages.City.NotExists, exception.Message);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnAttractionViewModel()
        {
            await this.SeedTestAttractionsAsync();
            var expectedResult = new AttractionViewModel
            {
                Id = "ccc",
                Name = "Sait Sofia",
                Type = "CulturalTours",
                Price = 0,
                Address = "2 Paris",
                CityId = 2,
                CityName = Plovdiv,
                Reviews = 0,
                UserPlan = null,
                Image = new ImageViewModel { Id = SaintSofiaImageId, Extension = ImagesExtension },
                Description = "The church was built on the site of several earlier churches from the 4th century, and places of worship dating back to the days when it was the necropolis of the Roman town of Serdica.",
            };
            var actualResult = await this.AttractionsService.GetViewModelByIdAsync<AttractionViewModel>("ccc");

            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Name, actualResult.Name);
            Assert.Equal(expectedResult.Address, actualResult.Address);
            Assert.Equal(expectedResult.Type, actualResult.Type);
            Assert.Equal(expectedResult.Price, actualResult.Price);
            Assert.Equal(expectedResult.Description, actualResult.Description);
            Assert.Equal(expectedResult.CityId, actualResult.CityId);
            Assert.Equal(expectedResult.CityName, actualResult.CityName);
            Assert.Equal(expectedResult.Reviews, actualResult.Reviews);
            Assert.Equal(expectedResult.UserPlan, actualResult.UserPlan);
            Assert.Equal(expectedResult.Image.Id, actualResult.Image.Id);
            Assert.Equal(expectedResult.Image.Extension, actualResult.Image.Extension);
            Assert.Equal(ImagesExtension, actualResult.Image.Extension);
            Assert.Equal(expectedResult.UserPlan, actualResult.UserPlan);
            Assert.Null(actualResult.UserPlan);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnAttractionFormUpdateModel()
        {
            await this.SeedTestAttractionsAsync();
            var expectedResult = new AttractionFormUpdateModel
            {
                Name = "Sait Sofia",
                Type = "CulturalTours",
                Price = 0,
                Address = "2 Paris",
                CityId = 2,
                Description = "The church was built on the site of several earlier churches from the 4th century, and places of worship dating back to the days when it was the necropolis of the Roman town of Serdica.",
            };
            var actualResult = await this.AttractionsService.GetViewModelByIdAsync<AttractionFormUpdateModel>("ccc");

            Assert.Equal(expectedResult.Name, actualResult.Name);
            Assert.Equal(expectedResult.Address, actualResult.Address);
            Assert.Equal(expectedResult.Description, actualResult.Description);
            Assert.Equal(expectedResult.CityId, actualResult.CityId);
            Assert.Equal(expectedResult.Type, actualResult.Type);
        }

        [Fact]
        public async Task GetViewModelByIdAsyncShouldThrowExceptionWhenInvalidAttractionIdPassed()
        {
            await this.SeedTestAttractionsAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.AttractionsService.GetViewModelByIdAsync<AttractionViewModel>("ddd"));
            Assert.Equal(ExceptionMessages.Attraction.InvalidAttraction, exception.Message);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenAttractionTypeNotValid()
        {
            await this.SeedTestAttractionsAsync();
            AttractionFormModel expectedResult = null;
            using (var stream = File.OpenRead(TestImagePath))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = TestImageContentType,
                };

                expectedResult = new AttractionFormModel
                {
                    Name = "ddd",
                    Type = "invalid type",
                    Price = 20,
                    Address = "New address",
                    AttractionUrl = "https://www.muzeiko.bg/bg",
                    Description = "new description for new attraction",
                    CityId = 1,
                    ImageToBlob = file,
                };
            }

            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(
             async () =>
             await this.AttractionsService.CreateAsync(expectedResult));
            Assert.Equal(ExceptionMessages.Attraction.InvalidAttractionType, exception.Message);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowExceptionWhenCityNotValid()
        {
            await this.SeedTestAttractionsAsync();
            AttractionFormModel expectedResult = null;
            using (var stream = File.OpenRead(TestImagePath))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = TestImageContentType,
                };

                expectedResult = new AttractionFormModel
                {
                    Name = "ddd",
                    Type = "CulturalTours",
                    Price = 20,
                    Address = "New address",
                    AttractionUrl = "https://www.muzeiko.bg/bg",
                    Description = "new description for new attraction",
                    CityId = 55,
                    ImageToBlob = file,
                };
            }

            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
             async () =>
             await this.AttractionsService.CreateAsync(expectedResult));
            Assert.Equal(ExceptionMessages.City.NotExists, exception.Message);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateAttraction()
        {
            await this.SeedTestAttractionsAsync();
            var exptectedResult = new AttractionFormUpdateModel
            {
                Name = "Muzeiko2",
                Type = "CulturalTours",
                AttractionUrl = "https://www.muzeiko.bg/bg",
                Price = 50,
                Address = "3 Professor Boyan Kamenov, 1756 Studentski2",
                CityId = 1,
                Description = "Childrenn's museum featuring interactive science-related exhibits, a cafe & a gift shop.",
            };
            await this.AttractionsService.UpdateAsync("aaa", exptectedResult);
            var actualResult = this.DbContext.Attractions.FirstOrDefault(x => x.Id == "aaa");
            Assert.Equal(exptectedResult.Name, actualResult.Name);
            Assert.Equal(exptectedResult.Type, actualResult.Type.ToString());
            Assert.Equal(exptectedResult.AttractionUrl, actualResult.AttractionUrl);
            Assert.Equal(exptectedResult.Price, actualResult.Price);
            Assert.Equal(exptectedResult.Address, actualResult.Address);
            Assert.Equal(exptectedResult.CityId, actualResult.CityId);
            Assert.Equal(exptectedResult.Description, actualResult.Description);
        }

        [Fact]
        public async Task DeleteByIdAsyncShouldMarkAttractionAsDeleted()
        {
            await this.SeedTestAttractionsAsync();
            var attractionToDelete = await this.DbContext.Attractions.FirstOrDefaultAsync(x=> x.Id == "aaa");
            await this.AttractionsService.DeleteByIdAsync("aaa");

            Assert.True(attractionToDelete.IsDeleted);
        }

        [Fact]
        public async Task DeleteByIdAsyncShouldThrowExceptionWhenIdNotValid()
        {
            await this.SeedTestAttractionsAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
             async () =>
             await this.AttractionsService.DeleteByIdAsync("random"));
            Assert.Equal(ExceptionMessages.Attraction.InvalidAttraction, exception.Message);
        }

        [Fact]
        public async Task GetCountByCityShouldReturnAttractionsCountInTheCity()
        {
            await this.SeedTestAttractionsAsync();
            var expectedResult = 2;
            var actualResult = this.AttractionsService.GetCountByCity(Sofia);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task GetAttractionCityAsyncShouldReturnCityViewModel()
        {
            await this.SeedTestAttractionsAsync();
            var expectedResult = new CityViewModel
            {
                Id = 1,
                Name = Sofia,
            };

            var actualResult = await this.AttractionsService.GetAttractionCityAsync("aaa");
            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Name, actualResult.Name);
        }

        [Fact]
        public async Task GetAttractionCityAsyncShouldThrowExceptionWhenInvalidId()
        {
            await this.SeedTestAttractionsAsync();

            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.AttractionsService.GetAttractionCityAsync("random"));
            Assert.Equal(ExceptionMessages.Attraction.InvalidAttraction, exception.Message);
        }

        [Fact]
        public async Task GetCountByCityShouldReturn0WhenInvalidCityPassed()
        {
            await this.SeedTestAttractionsAsync();
            var expectedResult = 0;
            var actualResult = this.AttractionsService.GetCountByCity(InvalidCity);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task AddReviewToUserAsyncShouldAddReviewToUser()
        {
            await this.SeedTestAttractionsAsync();
            var attraction = await this.DbContext.Attractions
                .Include(x => x.UsersReviews)
                .FirstOrDefaultAsync(x => x.Id == "aaa");

            await this.AttractionsService.AddReviewToUserAsync(TestUserId, "aaa");
            Assert.Equal(1, attraction.UsersReviews.Count);
        }

        [Fact]
        public async Task AddReviewToUserAsyncShouldThrowExceptionWhenAttractionNotValid()
        {
            await this.SeedTestAttractionsAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.AttractionsService.AddReviewToUserAsync(TestUserId, "invalid"));
            Assert.Equal(ExceptionMessages.Attraction.InvalidAttraction, exception.Message);
        }

        [Fact]
        public async Task AddReviewToUserAsyncShouldThrowExceptionWhenUserNotValid()
        {
            await this.SeedTestAttractionsAsync();
            var exception = await Assert.ThrowsAnyAsync<NullReferenceException>(
              async () =>
              await this.AttractionsService.AddReviewToUserAsync("invalid", "aaa"));
            Assert.Equal(ExceptionMessages.NotExistingUser, exception.Message);
        }

        private async Task SeedTestAttractionsAsync()
        {
            await this.SeedTestCountriesAsync();
            await this.SeedTestCitiesAsync();
            await this.SeedTestImagesAsync();
            await this.SeedTestUserAsync();

            this.DbContext.Attractions.Add(new Attraction
            {
                Id = "aaa",
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
                Id = "bbb",
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
                Id = "ccc",
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

        private async Task SeedTestImagesAsync()
        {
            this.DbContext.Images.Add(new Image { Id = MuzeikoImageId, Extension = ImagesExtension });
            this.DbContext.Images.Add(new Image { Id = HistoryMuseumImageId, Extension = ImagesExtension });
            this.DbContext.Images.Add(new Image { Id = SaintSofiaImageId, Extension = ImagesExtension });
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
