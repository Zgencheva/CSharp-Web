namespace VisitACity.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Cities;
    using VisitACity.Services.Mapping;

    public class CitiesService : ICitiesService
    {
        private readonly IDeletableEntityRepository<City> cityRepository;

        public CitiesService(IDeletableEntityRepository<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public int GetCount()
        {
            return this.cityRepository.AllAsNoTracking().ToArray().Length;
        }

        public async Task<IEnumerable<CityViewModel>> GetAllAsync()
        {
            return await this.cityRepository
                .All()
                .To<CityViewModel>()
                .ToListAsync();
        }
    }
}
