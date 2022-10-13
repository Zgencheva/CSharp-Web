namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICitiesService
    {
        int GetCount();

        Task<IEnumerable<KeyValuePair<string, string>>> GetAllAsKeyValuePairs();
    }
}
