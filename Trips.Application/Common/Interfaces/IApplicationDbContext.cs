using Microsoft.EntityFrameworkCore;
using Trips.Domain.Entities;

namespace Trips.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Trip> Trips { get; }
        public DbSet<Participant> Participants { get; }
        public DbSet<TripParticipant> TripParticipants { get; }
        public Task<IConveyOperationResult> SaveChangesAsync();
    }
}
