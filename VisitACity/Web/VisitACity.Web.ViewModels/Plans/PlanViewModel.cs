namespace VisitACity.Web.ViewModels.Plans
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Attractions;
    using VisitACity.Web.ViewModels.Restaurants;

    public class PlanViewModel : IMapFrom<Plan>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string CityCountryName { get; set; }

        public string CityName { get; set; }

        public int CityId { get; set; }

        public int Days { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime FromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime ToDate { get; set; }

        public ICollection<AttractionViewModel> Attractions { get; set; }

        public ICollection<RestaurantViewModel> Restaurants { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Plan, PlanViewModel>()
               .ForMember(x => x.Days, opt =>
                   opt.MapFrom(x => (x.ToDate.Date - x.FromDate.Date).Days));
        }
    }
}
