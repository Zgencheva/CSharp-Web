using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisitACity.Services.Data.Contracts
{
    public interface ICountriesService
    {
        int GetCount();

        Task<IEnumerable<KeyValuePair<string, string>>> GetAllAsKeyValuePairs();
    }
}
