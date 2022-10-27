namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VisitACity.Web.ViewModels.Administration.Attractions;
    using VisitACity.Web.ViewModels.Attractions;

    public interface IAttractionsService
    {
        int GetCount();

        Task<IEnumerable<AttractionViewModel>> GetBestAttractionsAsync(int page, int itemsPerPage);

        Task<IEnumerable<AttractionViewModel>> GetByCityAsync(string cityName, int page, int itemsPerPage);

        Task<AttractionViewModel> GetByIdAsync(int id);

        Task CreateAsync(AttractionFormModel model);
    }
}
