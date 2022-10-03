using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisitACity.Services.Data.Contracts
{
    public interface ICitiesService
    {
        int GetCount();

        Task<IEnumerable<KeyValuePair<string, string>>> GetAllAsKeyValuePairs();
    }
}
