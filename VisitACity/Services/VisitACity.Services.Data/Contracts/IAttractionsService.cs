namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VisitACity.Web.ViewModels.Administration.Attractions;

    public interface IAttractionsService
    {
        int GetCount();

        Task<IEnumerable<T>> GetBestAttractionsAsync<T>(int page, int itemsPerPage);

        Task<IEnumerable<T>> GetByCityAsync<T>(string cityName, int page, int itemsPerPage);

        Task<T> GetViewModelByIdAsync<T>(int id);

        Task CreateAsync(AttractionFormModel model);

        Task UpdateAsync(int id, AttractionFormModel model);

        Task DeleteByIdAsync(int id);

        int GetCountByCity(string cityName);

        Task AddReviewToUserAsync(string userId, int attractionId);

        Task<int> GetAttractionCityIdAsync(int attractionId);
    }
}
