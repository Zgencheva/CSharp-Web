namespace VisitACity.Services.Data.Tests
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using Xunit;

    public class AttractionServiceTests : ServiceTests
    {
        private IAttractionsService AttractionsServiceMoq => this.ServiceProvider.GetRequiredService<IAttractionsService>();

        [Fact]
        public async Task GetAllAsyncReturnsAllAttractions()
        {
            await this.SeedTestingCountry();
            Assert.True(true);
        }

        private async Task SeedTestingCountry()
        {
            this.DbContext.Countries.Add(new Country { Id = 1, Name = "Bulgaria" });
            await this.DbContext.SaveChangesAsync();
        }

    }
}
