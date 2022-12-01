namespace VisitACity.Services.Data.Tests
{
    using System;
    using System.Reflection;

    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Attractions;

    public class ServiceTests : IDisposable
    {
        public ServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(AttractionViewModel).GetTypeInfo().Assembly);

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
