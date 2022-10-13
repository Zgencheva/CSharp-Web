namespace VisitACity.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Attractions;

    public class AttractionsService : IAttractionsService
    {
        private readonly IDeletableEntityRepository<Attraction> attractionRepository;

        public AttractionsService(IDeletableEntityRepository<Attraction> attractionRepository)
        {
            this.attractionRepository = attractionRepository;
        }

        public int GetCount()
        {
            return this.attractionRepository.AllAsNoTracking().ToArray().Length;
        }

        public IEnumerable<AttractionViewModel> GetBestAttractions(int page, int itemsPage)
        {
            return this.attractionRepository.All()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPage).Take(itemsPage)
                .To<AttractionViewModel>()
               .ToList();
        }

        public IEnumerable<AttractionViewModel> GetAttractionsByCity(string cityName, int page, int itemsPage)
        {
            return this.attractionRepository.All()
            .Where(x => x.City.Name == cityName)
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * itemsPage).Take(itemsPage)
            .To<AttractionViewModel>()
           .ToList();
        }

        public async Task<AttractionViewModel> GetAttractionById(int id)
        {
            return await this.attractionRepository.All()
                .Where(x => x.Id == id)
                .To<AttractionViewModel>()
                .FirstOrDefaultAsync();
        }
    }
}
