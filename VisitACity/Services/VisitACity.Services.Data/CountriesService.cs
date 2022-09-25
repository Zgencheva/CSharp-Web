namespace VisitACity.Services.Data
{
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;

    public class CountriesService
    {
        private readonly IRepository<Country> countriesRepository;

        public CountriesService(IRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }
    }
}
