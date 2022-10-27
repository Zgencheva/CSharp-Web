namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VisitACity.Web.ViewModels.Administration.Countries;
    using VisitACity.Web.ViewModels.Countries;

    public interface ICountriesService
    {
        int GetCount();

        Task<IEnumerable<CountryViewModel>> GetAllAsync();

        Task CreateAsync(CountryFormModel model);
    }
}
