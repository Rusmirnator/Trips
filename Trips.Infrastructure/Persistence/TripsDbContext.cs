using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trips.Application.Common.Interfaces;
using Trips.Application.Common.Models;
using Trips.Domain.Entities;
using Trips.Infrastructure.Persistence.Configurations;

namespace Trips.Infrastructure.Persistence
{
    public class TripsDbContext : DbContext, ITripsDbContext
    {
        #region Fields & Properties
        private readonly ILogger<TripsDbContext> logger;

        public DbSet<Trip> Trips { get; private set; }
        public DbSet<Participant> Participants { get; private set; }
        public DbSet<TripParticipant> TripParticipants { get; private set; }
        #endregion

        #region CTOR
        public TripsDbContext(DbContextOptions<TripsDbContext> options, ILogger<TripsDbContext> logger)
            : base(options)
        {
            this.logger = logger;
        }
        #endregion

        #region Public API
        public async Task<IConveyOperationResult> SaveChangesAsync()
        {
            string errorMessage = string.Empty;
            try
            {
                _ = await base.SaveChangesAsync();

                return new OperationResultModel(true, errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

                logger.LogError(errorMessage);
            }

            return new OperationResultModel(false, errorMessage);
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TripConfiguration())
                .ApplyConfiguration(new TripParticipantConfiguration());
        }
    }
}
