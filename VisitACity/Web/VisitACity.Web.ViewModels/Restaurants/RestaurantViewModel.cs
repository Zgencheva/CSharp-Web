namespace VisitACity.Web.ViewModels.Restaurants
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Reviews;

    public class RestaurantViewModel : IMapFrom<Restaurant>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Address { get; set; }

        public string CityName { get; set; }

        public string Url { get; set; }

        public string PhoneNumber { get; set; }

        public double Rating { get; set; }

        public IEnumerable<ReviewRestaurantViewModel> Reviews { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Restaurant, RestaurantViewModel>()
                .ForMember(x => x.Rating, opt =>
                    opt.MapFrom(x => x.Reviews.Count() == 0 ? 0 : x.Reviews.Average(x => (double)x.Rating)));
        }
    }
}
