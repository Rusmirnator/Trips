using Trips.API.Middleware;
using Trips.Infrastructure;

namespace Trips.API
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            CreateApp(args).Run();
        }

        public static WebApplication CreateApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<ErrorHandlingMeddleware>();

            return app;
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddRouting(x => x.LowercaseUrls = true);
            services.AddTransient<ErrorHandlingMeddleware>();
            services.AddInfrastructureServices(configuration.GetValue("DataSource:UseInMemoryDatabase", false));
        }
    }
}