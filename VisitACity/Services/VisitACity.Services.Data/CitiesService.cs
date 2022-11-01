namespace VisitACity.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Administration.Cities;

    public class CitiesService : ICitiesService
    {
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<Country> countryRepository;

        public CitiesService(
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<Country> countryRepository)
        {
            this.cityRepository = cityRepository;
            this.countryRepository = countryRepository;
        }

        public int GetCount()
        {
            return this.cityRepository.AllAsNoTracking().ToArray().Length;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.cityRepository
                .All()
                .To<T>()
                .ToListAsync();
        }

        public async Task CreateAsync(CityFormModel model)
        {
            var country = await this.countryRepository.All().FirstOrDefaultAsync(x => x.Id == model.CountryId);
            if (country == null)
            {
                throw new ArgumentException("Invalid country");
            }

            var city = new City
            {
                Name = model.Name,
                CountryId = model.CountryId,
            };
            await this.cityRepository.AddAsync(city);
            await this.cityRepository.SaveChangesAsync();
        }
    }
}
