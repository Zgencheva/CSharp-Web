namespace VisitACity.Services.Data
{
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;

    public class CityService
    {
        private readonly IDeletableEntityRepository<City> cityRepository;

        public CityService(IDeletableEntityRepository<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

    }
}
