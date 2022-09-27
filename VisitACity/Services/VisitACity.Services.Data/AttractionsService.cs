using System.Collections.Generic;
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
            return this.attractionRepository.AllAsNoTracking().ToArray().Length;
        }

        public IEnumerable<Attraction> GetBestAttractions()
        {
            return this.attractionRepository.All().Take(6).ToArray();
        }

        public IEnumerable<Attraction> GetAttractionsByCity(string cityName)
        {
            return this.attractionRepository.All().Take(6).ToArray();
        }
    }
}
