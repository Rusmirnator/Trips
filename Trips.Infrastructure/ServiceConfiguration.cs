using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Trips.Infrastructure.Persistence;

namespace Trips.Infrastructure
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, bool useInMemoryDb)
        {
            if (useInMemoryDb)
            {
                /// created as singleton just for this particular case 
                services.AddDbContext<TripsDbContext>(options =>
                {
                    options.UseInMemoryDatabase("DummyDb");
                }, ServiceLifetime.Singleton);
            }

            return services;
        }
    }
}
