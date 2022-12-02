﻿namespace VisitACity.Services.Data.Tests
{
    using System;
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using VisitACity.Data;
    using VisitACity.Data.Common;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Data.Repositories;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Services.Mapping;
    using VisitACity.Web.ViewModels.Attractions;

    public abstract class ServiceTests : IDisposable
    {
        protected ServiceTests()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddIdentityCore<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                           .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IPlansService, PlansService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<ICitiesService, CitiesService>();
            services.AddTransient<IAttractionsService, AttractionsService>();
            services.AddTransient<IRestaurantsService, RestaurantsService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IImageService, ImageService>();

            AutoMapperConfig.RegisterMappings(typeof(AttractionViewModel).GetTypeInfo().Assembly);

            return services;
        }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }
    }
}
