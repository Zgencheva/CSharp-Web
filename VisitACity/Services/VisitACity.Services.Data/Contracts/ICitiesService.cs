namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VisitACity.Data.Models;
    using VisitACity.Web.ViewModels.Administration.Cities;
    using VisitACity.Web.ViewModels.Cities;

    public interface ICitiesService
    {
        int GetCount();

        Task<IEnumerable<CityViewModel>> GetAllAsync();

        Task CreateAsync(CreateCityInputModel model);
    }
}
