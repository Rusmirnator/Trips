using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trips.Application.Interfaces;

namespace Trips.Infrastructure.Services
{
    public class ParticipantService : IParticipate
    {
        public Task<bool> ReisterAsync(string mailAddress)
        {
            throw new NotImplementedException();
        }
    }
}
