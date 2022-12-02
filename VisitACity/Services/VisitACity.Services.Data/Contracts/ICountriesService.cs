namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VisitACity.Web.ViewModels.Administration.Countries;

    public interface ICountriesService
    {
        Task<IEnumerable<TТ>> GetAllAsync<TТ>();

        Task CreateAsync(CountryFormModel model);

        Task<bool> DoesCountryExist(string countryName);

        Task DeleteAsync(string name);
    }
}
