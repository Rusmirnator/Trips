using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Trips.Application.Common.Interfaces;
using Trips.Application.Trips.Interfaces;
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
                    .AddServices(useInMemoryDb);

            return services;
        }

        private static IServiceCollection AddDataAccess(this IServiceCollection services, bool useInMemoryDb)
        {
            if (useInMemoryDb)
            {
                services.AddDbContext<TripsDbContext>(options =>
                {
                    options.UseInMemoryDatabase("DummyDb");
                }, ServiceLifetime.Scoped);

                return services;
            }

            services.AddDbContext<TripsDbContext>(options =>
            {
                /// not available with current nuget packages
                /// this is where i would normally register DbContext
            });

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services, bool useInMemoryDb)
        {
            services.AddScoped<ITripService, TripService>();

            if (useInMemoryDb)
            {
                services.AddScoped<ITripsDbContext>(provider =>
                {
                    var context = provider.GetRequiredService<TripsDbContext>();

                    /// needed for seeding data
                    context.Database.EnsureCreated();

                    return context;
                });

                return services;
            }

            services.AddScoped<ITripsDbContext>(provider => provider.GetRequiredService<TripsDbContext>());

            return services;
        }
    }
}
