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
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Administration.Restaurants;
    using VisitACity.Web.ViewModels.Cities;

    public class RestaurantsService : IRestaurantsService
    {
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepository;
        private readonly IDeletableEntityRepository<City> cityRepository;

        public RestaurantsService(
            IDeletableEntityRepository<Restaurant> restaurantRepository,
            IDeletableEntityRepository<City> cityRepository)
        {
            this.restaurantRepository = restaurantRepository;
            this.cityRepository = cityRepository;
        }

        public async Task CreateAsync(RestaurantFromModel model)
        {
            var city = await this.cityRepository.All().FirstOrDefaultAsync(x => x.Id == model.CityId);
            if (city == null)
            {
                throw new NullReferenceException(ExceptionMessages.City.NotExists);
            }

            var restaurant = new Restaurant
            {
                Name = model.Name,
                City = city,
                Address = model.Address,
                ImageUrl = model.ImageUrl,
                Url = model.Url,
                PhoneNumber = model.PhoneNumber,
            };

            await this.restaurantRepository.AddAsync(restaurant);
            await this.restaurantRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var restaurant = await this.restaurantRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (restaurant == null)
            {
                throw new NullReferenceException(ExceptionMessages.Restaurant.InvalidRestaurant);
            }

            this.restaurantRepository.Delete(restaurant);
            await this.restaurantRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetByCityAsync<T>(string cityName, int page, int itemsPage)
        {
            return await this.restaurantRepository.AllAsNoTracking()
            .Where(x => x.City.Name == cityName)
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPage).Take(itemsPage)
            .To<T>()
           .ToListAsync();
        }

        public int GetCount()
        {
            return this.restaurantRepository.AllAsNoTracking().ToArray().Length;
        }

        public int GetCountByCity(string cityName)
        {
            return this.restaurantRepository.AllAsNoTracking()
                .Where(x => x.City.Name == cityName)
                .ToArray().Length;
        }

        public async Task<CityViewModel> GetRestaurantCityAsync(int restaurantId)
        {
            var restaurant = await this.restaurantRepository
                .AllAsNoTracking()
                .Include(x => x.City)
                .Where(x => x.Id == restaurantId)
                .FirstOrDefaultAsync();
            if (restaurant == null)
            {
                throw new NullReferenceException(ExceptionMessages.Restaurant.InvalidRestaurant);
            }

            var result = new CityViewModel
            {
                Id = restaurant.City.Id,
                Name = restaurant.City.Name,
            };
            return result;
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var restaurant = await this.restaurantRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
            if (restaurant == null)
            {
                throw new NullReferenceException(ExceptionMessages.Restaurant.InvalidRestaurant);
            }

            return restaurant;
        }

        public async Task UpdateAsync(int id, RestaurantFromModel model)
        {
            var restaurant = await this.restaurantRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (restaurant == null)
            {
                throw new NullReferenceException(ExceptionMessages.Restaurant.InvalidRestaurant);
            }

            var city = await this.cityRepository.All().FirstOrDefaultAsync(x => x.Id == model.CityId);
            if (city == null)
            {
                throw new NullReferenceException(ExceptionMessages.City.NotExists);
            }

            restaurant.Name = model.Name;
            restaurant.City = city;
            restaurant.Address = model.Address;
            restaurant.Url = model.Url;
            restaurant.ImageUrl = model.ImageUrl;
            restaurant.PhoneNumber = model.PhoneNumber;

            this.restaurantRepository.Update(restaurant);
            await this.restaurantRepository.SaveChangesAsync();
        }
    }
}
