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

        Task<T> GetViewModelByIdAsync<T>(string id);

        Task CreateAsync(AttractionFormModel model, string imageId, string imageExtension);

        Task UpdateAsync(string id, AttractionFormUpdateModel model);

        Task DeleteByIdAsync(string id);

        int GetCountByCity(string cityName);

        Task AddReviewToUserAsync(string userId, string attractionId);

        Task<int> GetAttractionCityIdAsync(string attractionId);

        Task<string> GetAttractionCityNameAsync(string attractionId);
        Task UploadImageAsync(string id, AttractionFormUpdateModel model, string imageId, string imageExtension);
    }
}
