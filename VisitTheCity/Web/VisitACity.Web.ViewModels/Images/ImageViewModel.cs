namespace VisitACity.Web.ViewModels.Images
{
    using VisitACity.Data.Models;
    using VisitACity.Services.Mapping;

    public class ImageViewModel : IMapFrom<Image>
    {
        public string Id { get; set; }

        public string Extension { get; set; }
    }
}
