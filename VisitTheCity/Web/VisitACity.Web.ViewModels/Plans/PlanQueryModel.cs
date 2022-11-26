namespace VisitACity.Web.ViewModels.Plans
{
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class PlanQueryModel : IMapFrom<Plan>
    {
        public int Id { get; set; }
    }
}
