namespace VisitACity.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
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

        public async Task<IEnumerable<KeyValuePair<string, string>>> GetAllAsKeyValuePairs()
        {
            var result = await this.cityRepository.All().Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
            })
                .ToListAsync();

            return result.Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name))
                .OrderBy(x => x.Value);
        }
    }
}
