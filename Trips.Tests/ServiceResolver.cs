using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Trips.API;

namespace Trips.Tests
{
    public static class ServiceResolver
    {
        private static WebApplicationBuilder? appBuilder;
        private static IServiceProvider? serviceProvider;

        public static T ResolveService<T>() where T : notnull
        {
            if (serviceProvider is null)
            {
                appBuilder = WebApplication.CreateBuilder(Array.Empty<string>());

                Program.ConfigureServices(appBuilder.Services, appBuilder.Configuration);

                serviceProvider = appBuilder.Services.BuildServiceProvider();
            }

            return serviceProvider!.GetRequiredService<T>();
        }
    }
}
