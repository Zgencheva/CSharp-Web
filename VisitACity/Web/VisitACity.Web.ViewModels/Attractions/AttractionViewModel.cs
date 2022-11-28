namespace VisitACity.Web.ViewModels.Attractions
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Images;
    using VisitACity.Web.ViewModels.Plans;

    public class AttractionViewModel : IMapFrom<Attraction>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        //public string ImageId { get; set; }

        //public string ImageExtension { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public string Address { get; set; }

        public string CityName { get; set; }

        public int CityId { get; set; }

        public int Reviews { get; set; }

        public ImageViewModel Image { get; set; }

        public PlanQueryModel UserPlan { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Attraction, AttractionViewModel>()
                .ForMember(x => x.Reviews, opt =>
                    opt.MapFrom(x => x.UsersReviews.Count() == 0 ? 0 : x.UsersReviews.Count()));
        }
    }
}
