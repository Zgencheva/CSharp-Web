using System.Linq;
using System.Threading.Tasks;
using VisitACity.Data.Common.Repositories;
using VisitACity.Data.Models;
using VisitACity.Services.Data.Contracts;
using VisitACity.Web.ViewModels.Plans;

namespace VisitACity.Services.Data
{
    public class PlansService : IPlansService
    {
        private readonly IDeletableEntityRepository<Plan> plansRepository;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<Country> countryReository;

        public PlansService(IDeletableEntityRepository<Plan> plansRepository,
                IDeletableEntityRepository<City> cityRepository,
                IDeletableEntityRepository<Country> countryReository)
        {
            this.plansRepository = plansRepository;
            this.cityRepository = cityRepository;
            this.countryReository = countryReository;
        }

        public async Task CreateAsync(CreatePlanInputModel input, string userId)
        {
            var country = this.countryReository.All().FirstOrDefault(x => x.Name == input.Country);
            if (country == null)
            {
                country = new Country { Name = input.Country };
            };
            var city = this.cityRepository.All().FirstOrDefault(x => x.Name == input.City);
            //TODO: not adding city that does not exist; This is for seeding the cities
            if (city == null)
            {
                //throw new Exception($"Invalid city");
                city = new City { Name = input.City, Country = country};
            }

            var plan = new Plan
            {
                UserId = userId,
                City = city,
                FromDate = input.FromDate,
                ToDate = input.ToDate,
            };
            await this.plansRepository.AddAsync(plan);
            await this.cityRepository.SaveChangesAsync();
        }
    }
}
