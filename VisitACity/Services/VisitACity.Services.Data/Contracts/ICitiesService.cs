namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VisitACity.Web.ViewModels.Administration.Cities;

    public interface ICitiesService
    {
        int GetCount();

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task CreateAsync(CityFormModel model);
    }
}
