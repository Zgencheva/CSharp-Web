using System.Linq;
using System.Threading.Tasks;
using VisitACity.Data.Common.Repositories;
using VisitACity.Data.Models;
using VisitACity.Services.Data.Contracts;

namespace VisitACity.Services.Data
{
    public class AttractionsService : IAttractionsService
    {
        private readonly IDeletableEntityRepository<Attraction> attractionRepository;

        public AttractionsService(IDeletableEntityRepository<Attraction> attractionRepository)
        {
            this.attractionRepository = attractionRepository;
        }

        public int GetAttractionsCount()
        {
            return attractionRepository.AllAsNoTracking().ToArray().Length;
        }
    }
}
