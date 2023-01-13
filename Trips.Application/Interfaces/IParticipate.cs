using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trips.Application.Interfaces
{
    public interface IParticipate
    {
        public Task<bool> ReisterAsync(string mailAddress);
    }
}
