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
    using VisitACity.Web.ViewModels.Attractions;
    using VisitACity.Web.ViewModels.Home;
    using VisitACity.Web.ViewModels.Plans;
    using VisitACity.Web.ViewModels.Restaurants;

    public class PlansService : IPlansService
    {
        private readonly IDeletableEntityRepository<Plan> plansRepository;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<Country> countryReository;
        private readonly IDeletableEntityRepository<Attraction> attractionRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public PlansService(
            IDeletableEntityRepository<Plan> plansRepository,
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<Country> countryReository,
            IDeletableEntityRepository<Attraction> attractionRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.plansRepository = plansRepository;
            this.cityRepository = cityRepository;
            this.countryReository = countryReository;
            this.attractionRepository = attractionRepository;
            this.userRepository = userRepository;
        }

        public async Task<bool> AddAttractionToPlanAsync(int attractionId, string userId)
        {
            var attraction = await this.attractionRepository.All().FirstOrDefaultAsync(x => x.Id == attractionId);
            if (attraction == null)
            {
                throw new NullReferenceException("Invalid attraction");
            }

            var cityId = attraction.CityId;
            var user = await this.userRepository
                .All()
                .Where(x => x.Id == userId)
                .Include(x => x.Plans)
                .ThenInclude(x => x.Attractions)
                .FirstOrDefaultAsync();
            if (!user.Plans.Any(x => x.CityId == cityId))
            {
                return false;
            }
            else
            {
                foreach (var plan in user.Plans.Where(x => x.CityId == cityId))
                {
                    plan.Attractions.Add(attraction);
                }
                await this.plansRepository.SaveChangesAsync();
                return true;
            }
        }

        public async Task CreateAsync(CreatePlanInputModel input, string userId)
        {
            var country = await this.countryReository.All().FirstOrDefaultAsync(x => x.Id == input.CountryId);
            if (country == null)
            {
                throw new Exception("Invalid country");
            }

            var city = await this.cityRepository.All().FirstOrDefaultAsync(x => x.Id == input.CityId);

            // TODO: not adding city that does not exist; This is for seeding the cities
            if (city == null)
            {
                throw new Exception("Invalid city");
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

        public async Task DeleteAsync(int planId)
        {
            var plan = await this.plansRepository.All().Where(x => x.Id == planId).FirstOrDefaultAsync();
            this.plansRepository.Delete(plan);
            await this.plansRepository.SaveChangesAsync();
        }

        public async Task<ICollection<PlanViewModel>> GetUserPlansAsync(string userId)
        {
            //TODO: Make it with automapper
            var plans = await this.plansRepository.All().Where(x => x.UserId == userId)
                .Select(x => new PlanViewModel
                {
                    Id = x.Id,
                    Country = x.City.Country.Name,
                    City = x.City.Name,
                    Days = (x.ToDate.Date - x.FromDate.Date).Days,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    MyAttractions = x.Attractions.Select(a => new AttractionViewModel
                        {
                            Id= a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            ImageUrl = a.ImageUrl,
                            Type = a.Type.ToString(),
                        }).ToList(),
                    MyRestaurants = x.Restaurants.Select(r => new RestaurantViewModel
                        {
                            Id = r.Id,
                            CityName = r.City.Name,
                            Name = r.Name,
                        }).ToList(),
                })
                .OrderByDescending(x => x.FromDate)
                .ToListAsync();
            return plans;
        }
    }
}
