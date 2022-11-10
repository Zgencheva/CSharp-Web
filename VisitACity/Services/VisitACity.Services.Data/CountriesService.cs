namespace VisitACity.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Administration.Countries;
    using VisitACity.Web.ViewModels.Countries;

    public class CountriesService : ICountriesService
    {
        private readonly IRepository<Country> countriesRepository;

        public CountriesService(IRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public async Task CreateAsync(CountryFormModel model)
        {
            var country = new Country
            {
                Name = model.Name,
            };
            await this.countriesRepository.AddAsync(country);
            await this.countriesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.countriesRepository
                .All()
                .To<T>()
                .ToListAsync();
        }

        public int GetCount()
        {
            return this.countriesRepository.AllAsNoTracking().ToArray().Length;
        }

    }
}
