using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trips.Application.Interfaces;
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
