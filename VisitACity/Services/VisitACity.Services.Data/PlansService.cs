using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitACity.Data.Common.Repositories;
using VisitACity.Data.Models;
using VisitACity.Services.Data.Contracts;
using VisitACity.Web.ViewModels.Attractions;
using VisitACity.Web.ViewModels.Plans;
using VisitACity.Web.ViewModels.Restaurants;

namespace VisitACity.Services.Data
{
    public class PlansService : IPlansService
    {
        private readonly IDeletableEntityRepository<Plan> plansRepository;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<Country> countryReository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public PlansService(
            IDeletableEntityRepository<Plan> plansRepository,
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<Country> countryReository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.plansRepository = plansRepository;
            this.cityRepository = cityRepository;
            this.countryReository = countryReository;
            this.userRepository = userRepository;
        }

        public async Task CreateAsync(CreatePlanInputModel input, string userId)
        {
            var country = await this.countryReository.All().FirstOrDefaultAsync(x => x.Name == input.Country);
            if (country == null)
            {
                country = new Country { Name = input.Country };
            }

            var city = await this.cityRepository.All().FirstOrDefaultAsync(x => x.Name == input.City);

            // TODO: not adding city that does not exist; This is for seeding the cities
            if (city == null)
            {
                // throw new Exception($"Invalid city");
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

        public async Task<ICollection<PlanViewModel>> GetUserPlansAsync(string userId)
        {
            var plans = await this.plansRepository.All().Where(x => x.UserId == userId)
                .Select(x => new PlanViewModel
                {
                    Country = x.City.Country.Name,
                    City = x.City.Name,
                    Days = (x.ToDate.Date - x.FromDate.Date).Days,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    MyAttractions = new UserAttractionsViewModel
                    {
                        MyAttractions = x.Attractions.Select(a => new AttractionViewModel
                        {
                            Name = a.Name,
                        }).ToList(),
                    },
                    MyRestaurants = new UserRestaurantsViewModel
                    {
                        MyRestaurants = x.Restaurants.Select(r => new RestaurantViewModel 
                        {
                        Name = r.Name,
                        }).ToList(),
                    },
                }).ToListAsync();


            return plans;
        }
    }
}
