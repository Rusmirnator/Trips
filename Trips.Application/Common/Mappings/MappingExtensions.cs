using Trips.Application.Common.Models;
using Trips.Domain.Entities;

namespace Trips.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static TripModel? ToModel(this Trip entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new TripModel
            {
                Name = entity.Name,
                Country = entity.Country,
                StartDate = entity.StartDate
            };
        }

        public static TripDetailsModel? ToDetailedModel(this Trip entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new TripDetailsModel
            {
                Name = entity.Name,
                Country = entity.Country,
                StartDate = entity.StartDate,
                Description = entity.Description,
                NumberOfSeats = entity.NumberOfSeats
            };
        }

        public static void Update(this Trip existingData, TripDetailsModel updatedData)
        {
            if (updatedData is not null)
            {
                existingData.Name = updatedData.Name;
                existingData.Country = updatedData.Country;
                existingData.StartDate = updatedData.StartDate;
                existingData.Description = updatedData.Description;
                existingData.NumberOfSeats = updatedData.NumberOfSeats;
            }
        }

        public static Trip Create(this TripDetailsModel newData)
        {
            Trip newEntry = new();

            if (newData is not null)
            {
                newEntry.Update(newData);
            }

            return newEntry;
        }

        public static Participant Create(this ParticipantModel newData)
        {
            return new Participant
            {
                MailAddress = newData.MailAddress
            };
        }
    }
}
