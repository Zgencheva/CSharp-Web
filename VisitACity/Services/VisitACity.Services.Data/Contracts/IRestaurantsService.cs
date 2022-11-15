namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VisitACity.Web.ViewModels.Administration.Restaurants;
    using VisitACity.Web.ViewModels.Restaurants;

    public interface IRestaurantsService
    {
        int GetCount();

        Task CreateAsync(RestaurantFromModel model);

        Task<IEnumerable<T>> GetByCityAsync<T>(string restaurantName, int id, int itemsPerPage);

        int GetCountByCity(string cityName);

        Task<T> GetViewModelByIdAsync<T>(int id);

        Task UpdateAsync(int id, RestaurantFromModel model);

        Task DeleteByIdAsync(int id);

        Task<int> GetRestaurantCityIdAsync(int restaurantId);

        Task<string> GetRestaurantCityNameAsync(int restaurantId);
    }
}
