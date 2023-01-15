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
        private readonly IApplicationDbContext context;
        #endregion

        #region CTOR
        public TripService(IApplicationDbContext context)
        {
            this.context = context;
        }
        #endregion

        #region Public API

        #region READ
        public async Task<TripDetailsModel?> GetTripDetailsAsync(TripModel selectedTrip)
        {
            Trip? existingTrip = await context.Trips.FirstOrDefaultAsync(e => e.Name == selectedTrip.Name);

            if (existingTrip is not null)
            {
                return existingTrip.ToDetailedModel();
            }

            return null;
        }

        public async Task<IEnumerable<TripModel>> GetTripsAsync()
        {
            List<Trip> existingTrips = await context.Trips.ToListAsync();

            if (!existingTrips.Any())
            {
                return Enumerable.Empty<TripModel>();
            }

            return existingTrips.Select(e => e.ToModel()!);
        }

        public async Task<IEnumerable<TripModel>> GetTripsBySearchTermAsync(string searchTerm)
        {
            List<Trip> filteredTrips = await context.Trips
                                        .Where(e => e.Country == searchTerm)
                                            .ToListAsync();

            if (!filteredTrips.Any())
            {
                return Enumerable.Empty<TripModel>();
            }

            return filteredTrips.Select(e => e.ToModel()!);
        }
        #endregion

        #region CREATE/UPDATE
        public async Task<IConveyOperationResult> CreateTripAsync(TripDetailsModel newData)
        {
            Trip? existingTrip = await GetTripByNameAsync(newData.Name, false);

            if (existingTrip is null)
            {
                existingTrip = newData.Create();

                _ = await context.Trips.AddAsync(existingTrip);
            }

            return await context.SaveChangesAsync();
        }

        public async Task<IConveyOperationResult> UpdateTripAsync(string oldDataIdentifier, TripDetailsModel updatedData)
        {
            Trip? existingTrip = await GetTripByNameAsync(oldDataIdentifier, false);

            existingTrip?.Update(updatedData);

            return await context.SaveChangesAsync();
        }

        public async Task<IConveyOperationResult> RegisterParticipantAsync(ParticipantModel participantData, TripModel tripData)
        {
            Trip? existingTrip = await GetTripByNameAsync(tripData.Name, true);

            Participant? existingParticipant = await GetParticipantByMailAsync(participantData.MailAddress, true);

            if (existingTrip?.TripParticipants.Any(tp => tp.Participant == existingParticipant) == true)
            {
                return new OperationResultModel(false, "Provided participant is already registered for the trip!");
            }

            existingParticipant ??= participantData.Create();

            _ = await context.Participants.AddAsync(existingParticipant);

            IConveyOperationResult operationResult = await context.SaveChangesAsync();

            if (!operationResult.IsSuccessful)
            {
                return operationResult;
            }

            existingTrip!.TripParticipants.Add(
                new TripParticipant
                {
                    TripId = existingTrip.Id,
                    ParticipantId = existingParticipant.Id,
                    Trip = existingTrip,
                    Participant = existingParticipant
                });

            return await context.SaveChangesAsync();
        }
        #endregion

        #region DELETE
        public async Task<IConveyOperationResult> DeleteTripAsync(TripModel deletedData)
        {
            Trip? existingTrip = await GetTripByNameAsync(deletedData.Name, false);

            if (existingTrip is not null)
            {
                context.Trips.Remove(existingTrip);
            }

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
