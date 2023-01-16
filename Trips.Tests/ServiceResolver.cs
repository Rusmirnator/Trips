using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Trips.Tests
{
    public static class ServiceResolver
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        public static T ResolveService<T>() where T : notnull
        {
            if (ServiceProvider is null)
            {
                var builder = WebApplication.CreateBuilder(Array.Empty<string>());

                API.Program.ConfigureServices(builder.Services, builder.Configuration);

                ServiceProvider = builder.Services.BuildServiceProvider();
            }

            return ServiceProvider!.GetRequiredService<T>();
        }
    }
}
