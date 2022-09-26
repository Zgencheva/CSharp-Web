using System.Linq;
using VisitACity.Data.Common.Repositories;
using VisitACity.Data.Models;
using VisitACity.Services.Data.Contracts;

namespace VisitACity.Services.Data
{
    public class RestaurantsService : IRestaurantsService
    {
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepository;

        public RestaurantsService(IDeletableEntityRepository<Restaurant> restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        public int GetRestaurantsCount()
        {
            return this.restaurantRepository.AllAsNoTracking().ToArray().Count();
        }
    }
}
