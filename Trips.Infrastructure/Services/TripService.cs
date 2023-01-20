using Microsoft.EntityFrameworkCore;
using Trips.Application.Common.Interfaces;
using Trips.Application.Common.Mappings;
using Trips.Application.Common.Models;
using Trips.Application.Trips.Interfaces;
using Trips.Application.Trips.Models;
using Trips.Domain.Entities;

namespace Trips.Infrastructure.Services
{
    public class TripService : ITripService
    {
        /// these regions below is an example of how i like to organise classes, structs etc.
        #region Fields & Properties
        private readonly ITripsDbContext context;
        #endregion

        #region CTOR
        public TripService(ITripsDbContext context)
        {
            this.context = context;
        }
        #endregion

        #region Public API

        #region READ
        public async Task<TripDetailsResponseModel?> GetTripDetailsAsync(string tripNameIdentifier)
        {
            Trip? existingTrip = await context.Trips.FirstOrDefaultAsync(e => e.Name == tripNameIdentifier);

            if (existingTrip is not null)
            {
                return existingTrip.ToDetailedModel();
            }

            return null;
        }

        public async Task<IEnumerable<TripResponseModel>> GetTripsAsync()
        {
            List<Trip> existingTrips = await context.Trips.ToListAsync();

            if (!existingTrips.Any())
            {
                return Enumerable.Empty<TripResponseModel>();
            }

            return existingTrips.Select(e => e.ToModel()!);
        }

        public async Task<IEnumerable<TripResponseModel>> GetTripsBySearchTermAsync(string searchTerm)
        {
            List<Trip> filteredTrips = await context.Trips
                                        .Where(e => e.Country == searchTerm)
                                            .ToListAsync();

            if (!filteredTrips.Any())
            {
                return Enumerable.Empty<TripResponseModel>();
            }

            return filteredTrips.Select(e => e.ToModel()!);
        }
        #endregion

        #region CREATE/UPDATE
        public async Task<IConveyOperationResult> CreateTripAsync(TripDetailsRequestModel newData)
        {
            Trip? existingTrip = await GetTripByNameAsync(newData.Name, false);

            if (existingTrip is not null)
            {
                return new OperationResultModel(false, $"Trip with the given name '{newData.Name}' already exists!");
            }

            existingTrip = newData.Create();

            _ = await context.Trips.AddAsync(existingTrip);

            return await context.SaveChangesAsync();
        }

        public async Task<IConveyOperationResult> UpdateTripAsync(string uniqueNameIdentifier, TripDetailsRequestModel updatedData)
        {
            if (string.IsNullOrEmpty(uniqueNameIdentifier))
            {
                return new OperationResultModel(false, $"Input data is not sufficient - Outdated data identifier not provided!");
            }

            Trip? existingTrip = await GetTripByNameAsync(uniqueNameIdentifier, false);

            if (existingTrip is null)
            {
                return new OperationResultModel(false, $"Trip with the given name '{uniqueNameIdentifier}' not exists!");
            }

            if (uniqueNameIdentifier.Equals(existingTrip.Name, StringComparison.Ordinal) == false
                && await GetTripByNameAsync(updatedData.Name, false) is Trip duplicateTrip)
            {
                return new OperationResultModel(false, $"Trip with the given name '{duplicateTrip.Name}' already exists!");
            }

            existingTrip.Update(updatedData);

            return await context.SaveChangesAsync();
        }

        public async Task<IConveyOperationResult> RegisterParticipantAsync(ParticipantRequestModel participantData)
        {
            Trip? existingTrip = await GetTripByNameAsync(participantData.TripName, true);

            Participant? existingParticipant = await GetParticipantByMailAsync(participantData.MailAddress, true);

            if (existingTrip is null)
            {
                return new OperationResultModel(false, $"Trip with the given name '{participantData.TripName}' does not exist!");
            }

            if (existingTrip!.TripParticipants.Any(tp => tp.ParticipantId == existingParticipant?.Id == true))
            {
                return new OperationResultModel(false, "Provided participant is already registered for the trip!");
            }

            existingParticipant ??= participantData.Create();

            TripParticipant relation = new()
            {
                TripId = existingTrip.Id,
                ParticipantId = existingParticipant.Id,
                Trip = existingTrip,
                Participant = existingParticipant
            };

            existingTrip!.TripParticipants.Add(relation);

            existingParticipant!.TripParticipants.Add(relation);

            return await context.SaveChangesAsync();
        }
        #endregion

        #region DELETE
        public async Task<IConveyOperationResult> DeleteTripAsync(string uniqueNameIdentifier)
        {
            Trip? existingTrip = await GetTripByNameAsync(uniqueNameIdentifier, false);

            if (existingTrip is null)
            {
                return new OperationResultModel(false, $"Trip with the given name '{uniqueNameIdentifier}' not exists!");
            }

            _ = context.Trips.Remove(existingTrip);

            return await context.SaveChangesAsync();
        }
        #endregion

        #endregion

        #region Private API
        private async Task<Trip?> GetTripByNameAsync(string? name, bool includeRelatedData)
        {
            if (includeRelatedData)
            {
                return await context.Trips
                        .Include(t => t.TripParticipants)
                            .FirstOrDefaultAsync(e => e.Name == name);
            }

            return await context.Trips.FirstOrDefaultAsync(e => e.Name == name);
        }

        private async Task<Participant?> GetParticipantByMailAsync(string? mailAddress, bool includeRelatedData)
        {
            if (includeRelatedData)
            {
                return await context.Participants
                                .Include(p => p.TripParticipants)
                                    .FirstOrDefaultAsync(e => e.MailAddress == mailAddress);
            }

            return await context.Participants.FirstOrDefaultAsync(e => e.MailAddress == mailAddress);
        }
        #endregion
    }
}
