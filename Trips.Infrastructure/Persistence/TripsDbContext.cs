using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using Trips.Application.Common.Interfaces;
using Trips.Domain.Entities;

namespace Trips.Infrastructure.Persistence
{
    public class TripsDbContext : DbContext, ITripsDbContext
    {
        private readonly ILogger<TripsDbContext> logger;

        public DbSet<Trip> Trips { get; private set; }
        public DbSet<Participant> Participants { get; private set; }

        public TripsDbContext(ILogger<TripsDbContext> logger)
        {
            this.logger = logger;
        }

        public TripsDbContext(DbContextOptions<TripsDbContext> options, ILogger<TripsDbContext> logger)
            : base(options)
        {
            this.logger = logger;
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (Exception ex) when (ex is DbUpdateException or DbUpdateConcurrencyException)
            {
                logger.LogError(ex.Message);
            }
            return 0;
        }
    }
}
