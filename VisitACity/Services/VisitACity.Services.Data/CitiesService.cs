namespace VisitACity.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;

    public class CitiesService : ICitiesService
    {
        private readonly IDeletableEntityRepository<City> cityRepository;

        public CitiesService(IDeletableEntityRepository<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public int GetCitiesCount()
        {
            return this.cityRepository.AllAsNoTracking().ToArray().Length;
        }
    }
}
