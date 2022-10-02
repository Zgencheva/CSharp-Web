using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisitACity.Services.Data.Contracts
{
    public interface ICitiesService
    {
        int GetCitiesCount();

        Task<IEnumerable<KeyValuePair<string, string>>> GetAllAsKeyValuePairs();
    }
}
