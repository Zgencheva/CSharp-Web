namespace VisitACity.Web.ViewModels.Attractions
{
    using System.Collections.Generic;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Images;

    public class AttractionViewModel : IMapFrom<Attraction>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }
    }
}
