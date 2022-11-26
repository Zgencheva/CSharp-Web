namespace VisitACity.Web.Infrastructure.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class SeedCitiesMiddlewareExtensions
    {
        public static IApplicationBuilder UseSeedCitiesMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedCitiesMiddleware>();
        }
    }
}
