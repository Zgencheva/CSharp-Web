namespace VisitACity.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Restaurants;

    public class RestaurantsService : IRestaurantsService
    {
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepository;

        public RestaurantsService(IDeletableEntityRepository<Restaurant> restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        public Task CreateAsync(CreateRestaurantInputModel model)
        {
            throw new System.NotImplementedException();
        }

        public int GetCount()
        {
            return this.restaurantRepository.AllAsNoTracking().ToArray().Count();
        }
    }
}
