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

            SeedData(builder);
        }

        private static void SeedData(EntityTypeBuilder<Trip> builder)
        {
            builder.HasData(
                new Trip
                {
                    Id = 1,
                    Name = "Vintage Experience",
                    Country = "Switzerland",
                    StartDate = DateTime.Now.AddMonths(1),
                    Description = "Prepare for great adventure!\nParticipate in vintage train trail and explore " +
                    "nooks of an late industrial era."
                },
                new Trip
                {
                    Id = 2,
                    Name = "Mountain Dew",
                    Country = "Spain",
                    StartDate = DateTime.Now.AddDays(14),
                    Description = "Let's hike!\nTake your backpack and feel the clear air of southern mountains."
                });
        }
    }
}
