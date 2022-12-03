using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using VisitACity.Services.Data.Contracts;
using Xunit;

namespace VisitACity.Services.Data.Tests
{
    public class ImageServiceTests : ServiceTests
    {
        private const string ExtensionJpg = "jpg";

        private IImageService ImagesService => this.ServiceProvider.GetRequiredService<IImageService>();

        [Fact]
        public async Task CreateAsyncAddImageToCollection()
        {
            var expectedResult = 1;
            var exptectedExtension = ExtensionJpg;
            var exptectedImageId = await this.ImagesService.CreateAsync(ExtensionJpg);

            var result = this.DbContext.Images.Count();
            var resultImage = this.DbContext.Images.First();
            Assert.Equal(expectedResult, result);
            Assert.Equal(exptectedExtension, resultImage.Extension);
            Assert.Equal(exptectedImageId, resultImage.Id);
        }
    }
}
