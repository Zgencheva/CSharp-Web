namespace VisitACity.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using VisitACity.Web.ViewModels.Administration.Restaurants;

    public interface IRestaurantsService
    {
        int GetCount();

        Task CreateAsync(RestaurantFromModel model);
    }
}
