namespace VisitACity.Web.ViewModels.Attractions
{
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class AttractionViewModel : IMapFrom<Attraction>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }
    }
}
