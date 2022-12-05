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

        Task<Т> GetUserUpcomingPlansByCityAsync<Т>(string cityName, string userId);

        Task DeleteAsync(int planId);

        Task<bool> AddAttractionToPlanAsync(string attractionId, int planId);

        Task DeleteAttractionFromPlanAsync(string attractionId, int planId);

        Task DeleteRestaurantFromPlanAsync(int restaurantId, int planId);

        Task<bool> DoesAttractionExist(string attractionId, int planId);

        Task<int> GetUserPlanIdAsync(string cityName, string userId);

        Task<bool> AddRestaurantToPlanAsync(int restaurantId, int planId);

        Task<bool> DoesUserHavePlanInTheCity(string userId, string cityName);

        Task<bool> DoesRestaurantExist(int restaurantId, int planId);
    }
}
