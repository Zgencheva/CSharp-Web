namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VisitACity.Web.ViewModels.Plans;

    public interface IPlansService
    {
        Task CreateAsync(CreatePlanInputModel input, string userId);

        Task<ICollection<PlanViewModel>> GetUserPlansAsync(string userId);

        Task DeleteAsync(int planId);

        Task<bool> AddAttractionToPlanAsync(int attractionId, string userId);
        Task<bool> AddRestaurantToPlanAsync(int restaurantId, string userId);
        Task DeleteAttractionFromPlanAsync(int attractionId, int planId);
        Task DeleteRestaurantFromPlanAsync(int restaurantId, int planId);
    }
}
