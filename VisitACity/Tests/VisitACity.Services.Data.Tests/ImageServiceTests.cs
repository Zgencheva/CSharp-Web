namespace VisitACity.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using VisitACity.Services.Data.Contracts;
    using Xunit;

    public class ImageServiceTests : ServiceTests
    {
        private const string ExtensionJpg = "jpg";

        private IImagesService ImagesService => this.ServiceProvider.GetRequiredService<IImagesService>();

        [Fact]
        public async Task CreateAsyncAddImageToCollection()
        {
            //var expectedResult = 1;
            //var exptectedExtension = ExtensionJpg;
            //var exptectedImageId = await this.ImagesService.CreateAsync(ExtensionJpg);

            //var result = this.DbContext.Images.Count();
            //var resultImage = this.DbContext.Images.First();
            //Assert.Equal(expectedResult, result);
            //Assert.Equal(exptectedExtension, resultImage.Extension);
            //Assert.Equal(exptectedImageId, resultImage.Id);
            Assert.False(true);
        }
    }
}
