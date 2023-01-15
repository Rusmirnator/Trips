using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trips.Domain.Entities;

namespace Trips.Infrastructure.Persistence.Configurations
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            _ = builder.HasIndex(e => e.MailAddress).IsUnique();

            _ = builder.Property(p => p.MailAddress)
                            .IsRequired();
        }
    }
}
