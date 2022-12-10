namespace VisitACity.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using VisitACity.Common;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Administration.Cities;
    using VisitACity.Web.ViewModels.Cities;

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
                .AllAsNoTracking()
                .To<T>()
                .ToListAsync();
        }

        public async Task CreateAsync(CityFormModel model)
        {
            var country = await this.countryRepository.All().FirstOrDefaultAsync(x => x.Id == model.CountryId);
            if (country == null)
            {
                throw new NullReferenceException(ExceptionMessages.Country.NotExists);
            }

            var city = await this.cityRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Name == model.Name);
            if (city == null)
            {
                var newCity = new City
                {
                    Name = model.Name,
                    CountryId = model.CountryId,
                };
                await this.cityRepository.AddAsync(newCity);
            }
            else
            {
                if (city.IsDeleted == true)
                {
                    this.cityRepository.Undelete(city);
                    this.cityRepository.Update(city);
                }
                else
                {
                    throw new ArgumentException(
                        string.Format(ModelConstants.City.CityExists, city.Name));
                }
            }

            await this.cityRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var city = await this.cityRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
            if (city == null)
            {
                throw new NullReferenceException(ExceptionMessages.City.NotExists);
            }

            return city;
        }

        public async Task<int> GetCountryIdAsync(int cityId)
        {
            var city = await this.cityRepository
                .AllAsNoTracking()
                .Where(x => x.Id == cityId)
                .Include(x => x.Country)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                throw new NullReferenceException(ExceptionMessages.City.NotExists);
            }

            return city.CountryId;
        }

        public async Task<bool> DoesCityExist(string cityName)
        {
            return await this.cityRepository.AllAsNoTracking().AnyAsync(x => x.Name == cityName);
        }

        public async Task DeleteAsync(string name)
        {
            var city = await this.cityRepository.All().Where(x => x.Name == name).FirstOrDefaultAsync();

            if (city == null)
            {
                throw new NullReferenceException(ExceptionMessages.City.NotExists);
            }

            this.cityRepository.Delete(city);
            this.cityRepository.Update(city);
            await this.cityRepository.SaveChangesAsync();
        }

        public List<SelectListItem> GetAllByCountryId(int id)
        {
            return this.cityRepository.AllAsNoTracking().
                 Where(x => x.CountryId == id)
                 .Select(x => new SelectListItem
                 {
                     Value = x.Id.ToString(),
                     Text = x.Name,
                 })
                 .ToList();
        }
    }
}
