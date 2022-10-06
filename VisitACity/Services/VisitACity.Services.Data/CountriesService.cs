namespace VisitACity.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;

    public class CountriesService : ICountriesService
    {
        private readonly IRepository<Country> countriesRepository;

        public CountriesService(IRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> GetAllAsKeyValuePairs()
        {
            var result = await this.countriesRepository.All().Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
            })
              .ToListAsync();

            return result.Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name))
                .OrderBy(x => x.Value);
        }

        public int GetCount()
        {
            return this.countriesRepository.AllAsNoTracking().ToArray().Length;
        }
    }
}
