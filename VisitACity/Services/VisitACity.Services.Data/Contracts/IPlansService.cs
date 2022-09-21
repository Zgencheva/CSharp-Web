namespace VisitACity.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using VisitACity.Web.ViewModels.Plans;

    public interface IPlansService
    {
        Task CreateAsync(CreatePlanInputModel input, string userId);
    }
}
