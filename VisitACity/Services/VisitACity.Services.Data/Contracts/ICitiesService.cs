namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using VisitACity.Web.ViewModels.Administration.Cities;
    using VisitACity.Web.ViewModels.Cities;

    public interface ICitiesService
    {
        int GetCount();

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task CreateAsync(CityFormModel model);

        Task<T> GetByIdAsync<T>(int id);

        Task<int> GetCountryIdAsync(int cityId);

        Task<bool> DoesCityExist(string cityName);

        Task DeleteAsync(string name);

        List<SelectListItem> GetAllByCountryId(int id);
    }
}
