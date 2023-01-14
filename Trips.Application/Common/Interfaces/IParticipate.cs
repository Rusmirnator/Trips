using Trips.Application.Common.Models;

namespace Trips.Application.Common.Interfaces
{
    public interface IParticipate
    {
        public Task<bool> ReisterAsync(ParticipantModel participantData);
    }
}
