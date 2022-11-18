namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VisitACity.Web.ViewModels.Administration.Countries;

    public interface ICountriesService
    {
        int GetCount();

        Task<IEnumerable<Т>> GetAllAsync<Т>();

        Task CreateAsync(CountryFormModel model);

        Task<bool> DoesCountryExist(string countryName);
    }
}
