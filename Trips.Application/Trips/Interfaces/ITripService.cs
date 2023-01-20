using Trips.Application.Common.Interfaces;
using Trips.Application.Trips.Models;

namespace Trips.Application.Trips.Interfaces
{
    public interface ITripService
    {
        public Task<IConveyOperationResult> CreateTripAsync(TripDetailsRequestModel newData);
        public Task<IEnumerable<TripResponseModel>> GetTripsAsync();
        public Task<IEnumerable<TripResponseModel>> GetTripsBySearchTermAsync(string searchTerm);
        public Task<TripDetailsResponseModel?> GetTripDetailsAsync(string tripNameIdentifier);
        public Task<IConveyOperationResult> UpdateTripAsync(string uniqueNameIdentifier, TripDetailsRequestModel updatedData);
        public Task<IConveyOperationResult> DeleteTripAsync(string uniqueNameIdentifier);
        public Task<IConveyOperationResult> RegisterParticipantAsync(ParticipantRequestModel participantData);
    }
}
