using Trips.Application.Common.Interfaces;
using Trips.Application.Trips.Models;

namespace Trips.Application.Trips.Interfaces
{
    public interface ITripService
    {
        public Task<IConveyOperationResult> CreateTripAsync(TripDetailsModel newData);
        public Task<IEnumerable<TripModel>> GetTripsAsync();
        public Task<IEnumerable<TripModel>> GetTripsBySearchTermAsync(string searchTerm);
        public Task<TripDetailsModel?> GetTripDetailsAsync(TripModel selectedTrip);
        public Task<IConveyOperationResult> UpdateTripAsync(string oldDataIdentifier, TripDetailsModel updatedData);
        public Task<IConveyOperationResult> DeleteTripAsync(TripModel deletedData);
        public Task<IConveyOperationResult> RegisterParticipantAsync(ParticipantModel participantData, TripModel tripData);
    }
}
