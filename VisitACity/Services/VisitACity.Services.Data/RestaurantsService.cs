﻿namespace VisitACity.Services.Data
{
    using System.Linq;

    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;

    public class RestaurantsService : IRestaurantsService
    {
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepository;

        public RestaurantsService(IDeletableEntityRepository<Restaurant> restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        public int GetCount()
        {
            return this.restaurantRepository.AllAsNoTracking().ToArray().Count();
        }
    }
}
