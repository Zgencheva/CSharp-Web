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
    using VisitACity.Web.ViewModels.Administration.Countries;

    public class CountriesService : ICountriesService
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public CountriesService(IDeletableEntityRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public async Task CreateAsync(CountryFormModel model)
        {
            var country = await this.countriesRepository
                .AllWithDeleted()
                .FirstOrDefaultAsync(x => x.Name == model.Name);
            if (country == null)
            {
                var newCountry = new Country
                {
                    Name = model.Name,
                };
                await this.countriesRepository.AddAsync(newCountry);
            }
            else
            {
                if (country.IsDeleted == true)
                {
                    this.countriesRepository.Undelete(country);
                    this.countriesRepository.Update(country);
                }
                else
                {
                    throw new ArgumentException(
                        string.Format(ModelConstants.Country.CountryExists, country.Name));
                }
            }

            await this.countriesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.countriesRepository
                .AllAsNoTracking()
                .To<T>()
                .ToListAsync();
        }

        public async Task<bool> DoesCountryExist(string countryName)
        {
            return await this.countriesRepository.AllAsNoTracking().AnyAsync(x => x.Name == countryName);
        }

        public async Task DeleteAsync(string name)
        {
            var country = await this.countriesRepository.All().Where(x => x.Name == name).FirstOrDefaultAsync();

            if (country == null)
            {
                throw new NullReferenceException(ExceptionMessages.Country.NotExists);
            }

            this.countriesRepository.Delete(country);
            this.countriesRepository.Update(country);
            await this.countriesRepository.SaveChangesAsync();
        }

        public async Task<List<SelectListItem>> GetAllToSelectList()
        {
            return await this.countriesRepository
                .All()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                })
                .ToListAsync();
        }
    }
}
