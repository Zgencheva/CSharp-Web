﻿namespace VisitACity.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Azure.Storage.Blobs;
    using Microsoft.EntityFrameworkCore;
    using VisitACity.Common;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Data.Models.Enums;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Administration.Attractions;

    public class AttractionsService : IAttractionsService
    {
        private readonly IDeletableEntityRepository<Attraction> attractionRepository;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly BlobServiceClient blobService;

        public AttractionsService(
            IDeletableEntityRepository<Attraction> attractionRepository,
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Image> imageRepository,
            BlobServiceClient blobService)
        {
            this.attractionRepository = attractionRepository;
            this.cityRepository = cityRepository;
            this.userRepository = userRepository;
            this.imageRepository = imageRepository;
            this.blobService = blobService;
        }

        public int GetCount()
        {
            return this.attractionRepository.AllAsNoTracking().ToArray().Length;
        }

        public async Task<IEnumerable<T>> GetBestAttractionsAsync<T>(int page, int itemsPage)
        {
            return await this.attractionRepository.AllAsNoTracking()
                .OrderByDescending(x => x.UsersReviews.Count)
                .Skip((page - 1) * itemsPage).Take(itemsPage)
                .To<T>()
               .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByCityAsync<T>(string cityName, int page, int itemsPage)
        {
            return await this.attractionRepository.AllAsNoTracking()
            .Where(x => x.City.Name == cityName)
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPage).Take(itemsPage)
            .To<T>()
           .ToListAsync();
        }

        public async Task<T> GetViewModelByIdAsync<T>(string id)
        {
            var attraction = await this.attractionRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
            if (attraction == null)
            {
                throw new NullReferenceException(ExceptionMessages.Attraction.InvalidAttraction);
            }

            return attraction;
        }

        public async Task CreateAsync(AttractionFormModel model, string imageId, string imageExtension)
        {
            if (!Enum.TryParse(model.Type, true, out AttractionType activityTypeEnum))
            {
                throw new ArgumentException(ExceptionMessages.Attraction.InvalidAttractionType);
            }

            var city = await this.cityRepository.All().FirstOrDefaultAsync(x => x.Id == model.CityId);
            if (city == null)
            {
                throw new NullReferenceException(ExceptionMessages.City.NotExists);
            }

            await UploadImateToBlob(model, imageId, imageExtension);

            var container = blobService.GetBlobContainerClient("images");

            var blob = container.GetBlobClient(imageId + "." + imageExtension);
            var url = blob.Uri.AbsoluteUri;
            var image = await this.imageRepository.All().FirstOrDefaultAsync(x => x.Id == imageId);
            if (image == null)
            {
                throw new NullReferenceException(ExceptionMessages.Image.NotExists);
            }

            var attraction = new Attraction
            {
                Name = model.Name,
                City = city,
                Address = model.Address,
                AttractionUrl = model.AttractionUrl,
                Description = model.Description,
                Price = model.Price,
                Image = image,
                Type = activityTypeEnum,
            };

            await this.attractionRepository.AddAsync(attraction);
            await this.attractionRepository.SaveChangesAsync();
        }

        private async Task UploadImateToBlob(AttractionFormModel model, string imageId, string imageExtension)
        {
            var stream = model.ImageToBlob.OpenReadStream();
            var container = this.blobService.GetBlobContainerClient("images");
            await container.UploadBlobAsync(imageId + "." + imageExtension, stream);
        }

        public async Task UpdateAsync(string id, AttractionFormModel model)
        {
            if (!Enum.TryParse(model.Type, true, out AttractionType activityTypeEnum))
            {
                throw new ArgumentException(ExceptionMessages.Attraction.InvalidAttractionType);
            }

            var attraction = await this.attractionRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (attraction == null)
            {
                throw new NullReferenceException(ExceptionMessages.Attraction.InvalidAttraction);
            }

            var city = await this.cityRepository.All().FirstOrDefaultAsync(x => x.Id == model.CityId);
            if (city == null)
            {
                throw new NullReferenceException(ExceptionMessages.City.NotExists);
            }

            attraction.Name = model.Name;
            attraction.City = city;
            attraction.Address = model.Address;
            attraction.AttractionUrl = model.AttractionUrl;
            attraction.Description = model.Description;
            attraction.Price = model.Price;
            attraction.Type = activityTypeEnum;

            this.attractionRepository.Update(attraction);
            await this.attractionRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var attraction = this.attractionRepository.All().FirstOrDefault(x => x.Id == id);
            if (attraction == null)
            {
                throw new NullReferenceException(ExceptionMessages.Attraction.InvalidAttraction);
            }

            this.attractionRepository.Delete(attraction);
            await this.attractionRepository.SaveChangesAsync();
        }

        public int GetCountByCity(string cityName)
        {
            return this.attractionRepository.AllAsNoTracking()
                .Where(x => x.City.Name == cityName)
                .ToArray().Length;
        }

        public async Task AddReviewToUserAsync(string userId, string attractionId)
        {
            var attraction = await this.attractionRepository
                .All()
                .Include(x => x.UsersReviews)
                .Where(x => x.Id == attractionId)
                .FirstOrDefaultAsync();
            if (attraction == null)
            {
                throw new NullReferenceException(ExceptionMessages.Attraction.InvalidAttraction);
            }

            var user = await this.userRepository.All().Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new NullReferenceException(ExceptionMessages.NotExistingUser);
            }

            if (!attraction.UsersReviews.Any(x => x.Id == userId))
            {
                attraction.UsersReviews.Add(user);
                await this.userRepository.SaveChangesAsync();
                await this.attractionRepository.SaveChangesAsync();
            }
        }

        public async Task<int> GetAttractionCityIdAsync(string attractionId)
        {
            var attraction = await this.attractionRepository
                .AllAsNoTracking()
                .Include(x => x.City)
                .Where(x => x.Id == attractionId)
                .FirstOrDefaultAsync();

            return attraction.City.Id;
        }

        public async Task<string> GetAttractionCityNameAsync(string attractionId)
        {
            var attraction = await this.attractionRepository
                .AllAsNoTracking()
                .Include(x => x.City)
                .Where(x => x.Id == attractionId)
                .FirstOrDefaultAsync();

            return attraction.City.Name;
        }
    }
}
