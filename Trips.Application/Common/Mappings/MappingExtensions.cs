using Trips.Application.Trips.Models;
using Trips.Domain.Entities;

namespace Trips.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static TripResponseModel? ToModel(this Trip entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new TripResponseModel
            {
                Name = entity.Name,
                Country = entity.Country,
                StartDate = entity.StartDate
            };
        }

        public static TripDetailsResponseModel? ToDetailedModel(this Trip entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new TripDetailsResponseModel
            {
                Name = entity.Name,
                Country = entity.Country,
                StartDate = entity.StartDate,
                Description = entity.Description,
                NumberOfSeats = entity.NumberOfSeats
            };
        }

        public static void Update(this Trip existingData, TripDetailsRequestModel updatedData)
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

        public static Trip Create(this TripDetailsRequestModel newData)
        {
            Trip newEntry = new();

            if (newData is not null)
            {
                newEntry.Update(newData);
            }

            return newEntry;
        }

        public static Participant Create(this ParticipantRequestModel newData)
        {
            return new Participant
            {
                MailAddress = newData.MailAddress
            };
        }
    }
}
