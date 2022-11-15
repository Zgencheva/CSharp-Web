namespace VisitACity.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VisitACity.Web.ViewModels.Plans;

    public interface IPlansService
    {
        Task CreateAsync(CreatePlanInputModel input, string userId);

        Task<ICollection<PlanViewModel>> GetUserPlansAsync(string userId);

        Task<ICollection<PlanViewModel>> GetUpcomingUserPlansAsync(string userId);

        Task<PlanQueryModel> GetUserUpcomingPlansByCityAsync(string cityName, string userId);

        Task DeleteAsync(int planId);

        Task<bool> AddAttractionToPlanAsync(int attractionId, int planId);

        Task DeleteAttractionFromPlanAsync(int attractionId, int planId);

        Task DeleteRestaurantFromPlanAsync(int restaurantId, int planId);

        Task<bool> DoesAttractionExist(int attractionId, int planId);

        Task<int> GerUserPlanIdAsync(string cityName, string userId);

        Task<bool> AddRestaurantToPlanAsync(int restaurantId, int planId);

        Task<bool> DoesUserHavePlanInTheCity(string userId, string cityName);

        Task<bool> DoesRestaurantExistInThePlan(int restaurantId, int planId);
    }
}
