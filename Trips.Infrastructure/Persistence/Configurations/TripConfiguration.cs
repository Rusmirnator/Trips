using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trips.Domain.Entities;

namespace Trips.Infrastructure.Persistence.Configurations
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            _ = builder.HasIndex(e => e.Name).IsUnique();

            _ = builder.Property(p => p.Name)
                            .HasMaxLength(50)
                            .IsRequired();

            _ = builder.Property(p => p.Description)
                            .IsUnicode();
        }
    }
}
