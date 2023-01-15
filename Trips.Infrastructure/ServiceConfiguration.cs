using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Trips.Application.Common.Interfaces;
using Trips.Infrastructure.Persistence;
using Trips.Infrastructure.Services;

namespace Trips.Infrastructure
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, bool useInMemoryDb)
        {
            /// somewhat overkill for current case, but in commercial one i like to organise it 
            /// like that to separate services by function
            services.AddDataAccess(useInMemoryDb)
                    .AddServices();

            return services;
        }

        private static IServiceCollection AddDataAccess(this IServiceCollection services, bool useInMemoryDb)
        {
            if (useInMemoryDb)
            {
                /// registered as singleton just for this particular case 
                services.AddDbContext<TripsDbContext>(options =>
                {
                    options.UseInMemoryDatabase("DummyDb");
                }, ServiceLifetime.Singleton);

                return services;
            }

            /// registered as scoped (default)
            services.AddDbContext<TripsDbContext>(options =>
            {
                /// not available with current nuget packages
                /// this is where i would normally register DbContext
            });

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITripService, TripService>();

            return services;
        }
    }
}
