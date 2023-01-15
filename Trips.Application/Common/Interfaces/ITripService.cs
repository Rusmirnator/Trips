using Trips.Application.Common.Models;

namespace Trips.Application.Common.Interfaces
{
    public interface ITripService
    {
        public Task<bool> CreateTripAsync(TripDetailsModel newData);
        public Task<IEnumerable<TripModel>> GetTripsAsync();
        public Task<IEnumerable<TripModel>> GetTripsBySearchTermAsync(string searchTerm);
        public Task<TripDetailsModel?> GetTripDetailsAsync(TripModel selectedTrip);
        public Task<bool> UpdateTripAsync(string oldDataIdentifier, TripDetailsModel updatedData);
        public Task<bool> DeleteTripAsync(TripModel deletedData);
        public Task<bool> RegisterParticipantAsync(ParticipantModel participantData, TripModel tripData);
    }
}
