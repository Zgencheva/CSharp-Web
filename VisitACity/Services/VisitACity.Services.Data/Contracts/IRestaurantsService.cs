using System.Threading.Tasks;
using VisitACity.Web.ViewModels.Administration.Restaurants;

namespace VisitACity.Services.Data.Contracts
{
    public interface IRestaurantsService
    {
        int GetCount();

        Task CreateAsync(CreateRestaurantInputModel model);
    }
}
