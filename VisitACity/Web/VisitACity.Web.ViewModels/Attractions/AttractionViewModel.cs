namespace VisitACity.Web.ViewModels.Attractions
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Images;

    public class AttractionViewModel : IMapFrom<Attraction>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public string Address { get; set; }

        public string CityName { get; set; }

        public double Rating { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Attraction, AttractionViewModel>()
                .ForMember(x => x.Rating, opt =>
                    opt.MapFrom(x => x.Reviews.Count() == 0 ? 0 : x.Reviews.Average(x => (double)x.Rating)));
        }
    }
}
