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
    using VisitACity.Web.ViewModels.Attractions;
    using VisitACity.Web.ViewModels.Plans;
    using VisitACity.Web.ViewModels.Restaurants;

    public class PlansService : IPlansService
    {
        private readonly IDeletableEntityRepository<Plan> plansRepository;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<Country> countryReository;
        private readonly IDeletableEntityRepository<Attraction> attractionRepository;
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public PlansService(
            IDeletableEntityRepository<Plan> plansRepository,
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<Country> countryReository,
            IDeletableEntityRepository<Attraction> attractionRepository,
            IDeletableEntityRepository<Restaurant> restaurantRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.plansRepository = plansRepository;
            this.cityRepository = cityRepository;
            this.countryReository = countryReository;
            this.attractionRepository = attractionRepository;
            this.restaurantRepository = restaurantRepository;
            this.userRepository = userRepository;
        }

        public async Task<bool> AddAttractionToPlanAsync(int attractionId, int planId)
        {
            var plan = await this.plansRepository
                .All()
                .Include(x => x.Attractions)
                .FirstOrDefaultAsync(x => x.Id == planId);
            if (plan == null)
            {
                return false;
            }

            var attraction = await this.attractionRepository
                .All()
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == attractionId);
            if (attraction == null)
            {
                throw new NullReferenceException("Invalid plan");
            }

            plan.Attractions.Add(attraction);
            await this.plansRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddRestaurantToPlanAsync(int restaurantId, int planId)
        {
            var plan = await this.plansRepository.All().FirstOrDefaultAsync(x => x.Id == planId);
            if (plan == null)
            {
                throw new NullReferenceException("Invalid plan");
            }

            var restaurant = await this.attractionRepository.All().FirstOrDefaultAsync(x => x.Id == restaurantId);
            if (restaurant == null)
            {
                throw new NullReferenceException("Invalid plan");
            }

            if (!plan.Attractions.Any(x => x.Id == restaurantId))
            {
                return false;
            }
            else
            {
                plan.Attractions.Add(restaurant);
                await this.plansRepository.SaveChangesAsync();
                return true;
            }
        }

        public async Task CreateAsync(CreatePlanInputModel input, string userId)
        {
            var country = await this.countryReository.All().FirstOrDefaultAsync(x => x.Id == input.CountryId);
            if (country == null)
            {
                throw new NullReferenceException("Invalid country");
            }

            var city = await this.cityRepository.All().FirstOrDefaultAsync(x => x.Id == input.CityId);

            // TODO: not adding city that does not exist; This is for seeding the cities
            if (city == null)
            {
                throw new NullReferenceException("Invalid city");
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
            if (plan == null)
            {
                throw new NullReferenceException("Invalid plan");
            }

            this.plansRepository.Delete(plan);
            await this.plansRepository.SaveChangesAsync();
        }

        public async Task DeleteAttractionFromPlanAsync(int attractionId, int planId)
        {
            var plan = await this.plansRepository
                .All()
                .Where(x => x.Id == planId)
                .Include(x => x.Attractions)
                .FirstOrDefaultAsync();
            if (plan == null)
            {
                throw new NullReferenceException("Invalid plan");
            }

            var attraction = plan.Attractions.FirstOrDefault(x => x.Id == attractionId);

            if (attraction == null)
            {
                throw new NullReferenceException("No such attraction in your plan");
            }
            plan.Attractions.Remove(attraction);
            await this.plansRepository.SaveChangesAsync();
        }

        public async Task DeleteRestaurantFromPlanAsync(int restaurantId, int planId)
        {
            var plan = await this.plansRepository
               .All()
               .Where(x => x.Id == planId)
               .Include(x => x.Restaurants)
               .FirstOrDefaultAsync();
            if (plan == null)
            {
                throw new NullReferenceException("Invalid plan");
            }

            var restaurant = plan.Attractions.FirstOrDefault(x => x.Id == restaurantId);
            if (restaurant == null)
            {
                throw new NullReferenceException("No such restaurant in your plan");
            }

            plan.Attractions.Remove(restaurant);
            await this.plansRepository.SaveChangesAsync();
        }

        public async Task<bool> DoesAttractionExist(int attractionId, int planId)
        {
            var plan = await this.plansRepository.AllAsNoTracking()
                .Where(x => x.Id == planId)
                .Include(x=> x.Attractions)
                .FirstOrDefaultAsync();
            return plan.Attractions.Any(x => x.Id == attractionId);
        }

        public async Task<ICollection<PlanViewModel>> GetUpcomingUserPlansAsync(string userId)
        {
            var plans = await this.plansRepository.All().Where(x => x.UserId == userId && x.ToDate >= DateTime.UtcNow)
               .Select(x => new PlanViewModel
               {
                   Id = x.Id,
                   Country = x.City.Country.Name,
                   City = x.City.Name,
                   CityId = x.City.Id,
                   Days = (x.ToDate.Date - x.FromDate.Date).Days,
                   FromDate = x.FromDate,
                   ToDate = x.ToDate,
                   MyAttractions = x.Attractions.Select(a => new AttractionViewModel
                   {
                       Id = a.Id,
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

        public async Task<ICollection<PlanViewModel>> GetUserPlansAsync(string userId)
        {
            //TODO: Make it with automapper
            var plans = await this.plansRepository.All().Where(x => x.UserId == userId)
                .Select(x => new PlanViewModel
                {
                    Id = x.Id,
                    Country = x.City.Country.Name,
                    City = x.City.Name,
                    CityId = x.City.Id,
                    Days = (x.ToDate.Date - x.FromDate.Date).Days,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    MyAttractions = x.Attractions.Select(a => new AttractionViewModel
                        {
                            Id = a.Id,
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

        public async Task<PlanQueryModel> GetUserUpcomingPlansByCityAsync(string cityName, string userId)
        {
            return await this.plansRepository.All()
                .Where(x => x.UserId == userId && x.City.Name == cityName)
                .To<PlanQueryModel>()
                .FirstOrDefaultAsync();
        }
    }
}
