namespace VisitACity.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VisitACity.Common;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Data.Models.Enums;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Administration.Attractions;
    using VisitACity.Web.ViewModels.Attractions;

    public class AttractionsService : IAttractionsService
    {
        private readonly IDeletableEntityRepository<Attraction> attractionRepository;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public AttractionsService(
            IDeletableEntityRepository<Attraction> attractionRepository,
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.attractionRepository = attractionRepository;
            this.cityRepository = cityRepository;
            this.userRepository = userRepository;
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

        public async Task<T> GetViewModelByIdAsync<T>(int id)
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

            var attraction = new Attraction
            {
                Name = model.Name,
                City = city,
                Address = model.Address,
                AttractionUrl = model.AttractionUrl,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                Type = activityTypeEnum,
            };

            await this.attractionRepository.AddAsync(attraction);
            await this.attractionRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, AttractionFormModel model)
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
            attraction.ImageUrl = model.ImageUrl;
            attraction.Type = activityTypeEnum;

            this.attractionRepository.Update(attraction);
            await this.attractionRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
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

        public async Task AddReviewToUserAsync(string userId, int attractionId)
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

        public async Task<int> GetAttractionCityIdAsync(int attractionId)
        {
            var attraction = await this.attractionRepository
                .AllAsNoTracking()
                .Include(x => x.City)
                .Where(x => x.Id == attractionId)
                .FirstOrDefaultAsync();

            return attraction.City.Id;
        }

        public async Task<string> GetAttractionCityNameAsync(int attractionId)
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
