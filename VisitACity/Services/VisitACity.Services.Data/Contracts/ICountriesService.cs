using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisitACity.Services.Data.Contracts
{
    public interface ICountriesService
    {
        int GetCountryCount();

        Task<IEnumerable<KeyValuePair<string, string>>> GetAllAsKeyValuePairs();
    }
}
