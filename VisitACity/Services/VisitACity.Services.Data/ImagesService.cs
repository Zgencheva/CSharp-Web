namespace VisitACity.Services.Data
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Azure.Storage.Blobs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using VisitACity.Common;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;

    public class ImagesService : IImagesService
    {
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly BlobServiceClient blobService;

        public ImagesService(
           IDeletableEntityRepository<Image> imageRepository,
           BlobServiceClient blobService)
        {
            this.imageRepository = imageRepository;
            this.blobService = blobService;
        }

        public async Task<string> CreateAsync(IFormFile imageModel)
        {
            var imageId = Guid.NewGuid().ToString();
            string imageExtension = Path.GetExtension(imageModel.FileName);
            await this.UploadImageToBlob(imageModel, imageId, imageExtension);

            await this.imageRepository.AddAsync(new Image { Id = imageId, Extension = imageExtension });
            await this.imageRepository.SaveChangesAsync();
            return imageId;
        }

        public async Task<string> UpdateAsync(IFormFile imageModel, string imageId)
        {
            var image = await this.imageRepository.All().FirstOrDefaultAsync(x => x.Id == imageId);
            var oldImageExtension = image.Extension;
            string newImageExtension = Path.GetExtension(imageModel.FileName);
            await this.DeleteImageFromBlob(imageId);
            await this.UploadImageToBlob(imageModel, imageId, newImageExtension);
            if (oldImageExtension != newImageExtension)
            {
                image.Extension = newImageExtension;
                this.imageRepository.Update(image);
                await this.imageRepository.SaveChangesAsync();
            }

            return imageId;
        }

        private async Task UploadImageToBlob(IFormFile file, string imageId, string imageExtension)
        {
            var stream = file.OpenReadStream();
            var container = this.blobService.GetBlobContainerClient(GlobalConstants.BlobContainerName);
            await container.UploadBlobAsync(imageId + "." + imageExtension, stream);
        }

        private async Task DeleteImageFromBlob(string imageId)
        {
            var container = this.blobService.GetBlobContainerClient(GlobalConstants.BlobContainerName);

            var blob = container.GetBlobClient(imageId + GlobalConstants.JpgFormat);
            var blob2 = container.GetBlobClient(imageId + GlobalConstants.JpgFormat);
            var blob3 = container.GetBlobClient(imageId + GlobalConstants.PngFormat);
            await blob.DeleteIfExistsAsync();
            await blob2.DeleteIfExistsAsync();
            await blob3.DeleteIfExistsAsync();
        }
    }
}
