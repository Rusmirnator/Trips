using Trips.Application.Common.Interfaces;
using Trips.Application.Common.Models;

namespace Trips.Infrastructure.Services
{
    public class ParticipantService : IParticipate
    {
        public Task<bool> ReisterAsync(ParticipantModel participantData)
        {
            throw new NotImplementedException();
        }
    }
}
