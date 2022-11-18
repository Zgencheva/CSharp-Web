namespace VisitACity.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Administration.Restaurants;
    using VisitACity.Web.ViewModels.Restaurants;

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
                throw new NullReferenceException("No such city");
            }

            var restaurant = new Restaurant
            {
                Name = model.Name,
                City = city,
                Address = model.Address,  
                ImageUrl = model.ImageUrl,
            };

            await this.restaurantRepository.AddAsync(restaurant);
            await this.restaurantRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var restaurant = await this.restaurantRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (restaurant == null)
            {
                throw new NullReferenceException("No such restaurant");
            }

            this.restaurantRepository.Delete(restaurant);
            await this.restaurantRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetByCityAsync<T>(string restaurantName, int page, int itemsPage)
        {
            return await this.restaurantRepository.All()
            .Where(x => x.City.Name == restaurantName)
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPage).Take(itemsPage)
            .To<T>()
           .ToListAsync();
        }

        public int GetCount()
        {
            return this.restaurantRepository.AllAsNoTracking().ToArray().Count();
        }

        public int GetCountByCity(string cityName)
        {
            return this.restaurantRepository.AllAsNoTracking()
                .Where(x => x.City.Name == cityName)
                .ToArray().Length;
        }

        public async Task<int> GetRestaurantCityIdAsync(int restaurantId)
        {
            var restaurant = await this.restaurantRepository
                .All()
                .Include(x => x.City)
                .Where(x => x.Id == restaurantId)
                .FirstOrDefaultAsync();

            return restaurant.City.Id;
        }

        public async Task<string> GetRestaurantCityNameAsync(int restaurantId)
        {
            var restaurant = await this.restaurantRepository
                .All()
                .Include(x => x.City)
                .Where(x => x.Id == restaurantId)
                .FirstOrDefaultAsync();

            return restaurant.City.Name;
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var restaurant = await this.restaurantRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
            if (restaurant == null)
            {
                throw new NullReferenceException("Invalid restaurant");
            }

            return restaurant;
        }

        public async Task UpdateAsync(int id, RestaurantFromModel model)
        {
            var restaurant = await this.restaurantRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (restaurant == null)
            {
                throw new NullReferenceException("No such attraction");
            }

            var city = await this.cityRepository.All().FirstOrDefaultAsync(x => x.Id == model.CityId);
            if (city == null)
            {
                throw new NullReferenceException("No such city");
            }

            restaurant.Name = model.Name;
            restaurant.City = city;
            restaurant.Address = model.Address;
            restaurant.Url = model.Url;
            restaurant.ImageUrl = model.ImageUrl;

            this.restaurantRepository.Update(restaurant);
            await this.restaurantRepository.SaveChangesAsync();
        }
    }
}
