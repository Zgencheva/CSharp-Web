using System.Collections.Generic;
using VisitACity.Data.Models;

namespace VisitACity.Services.Data.Contracts
{
    public interface IAttractionsService
    {
        int GetAttractionsCount();

        IEnumerable<Attraction> GetBestAttractions();
    }
}
