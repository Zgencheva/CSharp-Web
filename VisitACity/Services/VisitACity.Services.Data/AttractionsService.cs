namespace VisitACity.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Azure.Storage.Blobs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using VisitACity.Common;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Data.Models.Enums;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Administration.Attractions;
    using VisitACity.Web.ViewModels.Attractions;
    using VisitACity.Web.ViewModels.Cities;

    public class AttractionsService : IAttractionsService
    {
        private readonly IDeletableEntityRepository<Attraction> attractionRepository;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly IImagesService imagesService;

        public AttractionsService(
            IDeletableEntityRepository<Attraction> attractionRepository,
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Image> imageRepository,
            IImagesService imagesService
            )
        {
            this.attractionRepository = attractionRepository;
            this.cityRepository = cityRepository;
            this.userRepository = userRepository;
            this.imageRepository = imageRepository;
            this.imagesService = imagesService;
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
            if (cityName == null)
            {
                throw new NullReferenceException(ExceptionMessages.City.NotExists);
            }

            var city = await this.cityRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Name == cityName);

            if (city == null)
            {
                throw new NullReferenceException(ExceptionMessages.City.NotExists);
            }

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

        public async Task CreateAsync(AttractionFormModel model)
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

            var imageId = await this.imagesService.CreateAsync(model.ImageToBlob);

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

        public async Task UpdateAsync(string id, AttractionFormUpdateModel model)
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

            if (model.ImageToBlob is not null)
            {
                string imageId = await this.GetImageIDasync(id);
                await this.imagesService.UpdateAsync(model.ImageToBlob, imageId);
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

        public async Task<CityViewModel> GetAttractionCityAsync(string attractionId)
        {
            var attraction = await this.attractionRepository
                .AllAsNoTracking()
                .Include(x => x.City)
                .Where(x => x.Id == attractionId)
                .To<AttractionViewModel>()
                .FirstOrDefaultAsync();
            if (attraction == null)
            {
                throw new NullReferenceException(ExceptionMessages.Attraction.InvalidAttraction);
            }

            var city = new CityViewModel
            {
                Name = attraction.CityName,
                Id = attraction.CityId,
            };
            return city;
        }

        public async Task<string> GetImageIDasync(string id)
        {
            var attraction = await this.attractionRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (attraction == null)
            {
                throw new NullReferenceException(ExceptionMessages.Attraction.InvalidAttraction);
            }

            return attraction.ImageId;
        }
    }
}
