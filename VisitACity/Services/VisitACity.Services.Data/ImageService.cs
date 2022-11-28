namespace VisitACity.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Attractions;

    public class ImageService : IImageService
    {
        private readonly IDeletableEntityRepository<Image> imageRepository;

        public ImageService(
           IDeletableEntityRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public async Task<string> CreateAsync(string extension)
        {
            var imageId = Guid.NewGuid().ToString();
            await this.imageRepository.AddAsync(new Image { Id = imageId, Extension = extension });
            await this.imageRepository.SaveChangesAsync();
            return imageId;
        }

    }
}
