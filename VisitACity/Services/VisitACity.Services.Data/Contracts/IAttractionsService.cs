namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VisitACity.Web.ViewModels.Administration.Attractions;
    using VisitACity.Web.ViewModels.Cities;

    public interface IAttractionsService
    {
        int GetCount();

        int GetCountByCity(string cityName);

        Task<IEnumerable<T>> GetBestAttractionsAsync<T>(int page, int itemsPerPage);

        Task<IEnumerable<T>> GetByCityAsync<T>(string cityName, int page, int itemsPerPage);

        Task<T> GetViewModelByIdAsync<T>(string id);

        Task CreateAsync(AttractionFormModel model);

        Task UpdateAsync(string id, AttractionFormUpdateModel model);

        Task DeleteByIdAsync(string id);

        Task AddReviewToUserAsync(string userId, string attractionId);

        Task<CityViewModel> GetAttractionCityAsync(string attractionId);

        Task<string> GetImageIDasync(string id);
    }
}
