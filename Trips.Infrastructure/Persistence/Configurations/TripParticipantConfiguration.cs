using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trips.Domain.Entities;

namespace Trips.Infrastructure.Persistence.Configurations
{
    public class TripParticipantConfiguration : IEntityTypeConfiguration<TripParticipant>
    {
        public void Configure(EntityTypeBuilder<TripParticipant> builder)
        {
            builder.HasKey(tp => new { tp.TripId, tp.ParticipantId });

            builder.HasOne(tp => tp.Trip)
                .WithMany(t => t.TripParticipants)
                .HasForeignKey(tp => tp.TripId);

            builder.HasOne(tp => tp.Participant)
                .WithMany(p => p.TripParticipants)
                .HasForeignKey(tp => tp.ParticipantId);
        }
    }
}
