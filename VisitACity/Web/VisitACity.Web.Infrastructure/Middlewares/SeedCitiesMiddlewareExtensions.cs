using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitACity.Web.Infrastructure.Middlewares
{
    public static class SeedCitiesMiddlewareExtensions
    {
        public static IApplicationBuilder UseSeedCitiesMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedCitiesMiddleware>();
        }
    }
}
