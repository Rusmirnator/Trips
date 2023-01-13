using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trips.Application.Models;

namespace Trips.Application.Interfaces
{
    public interface ITripService
    {
        public Task<TripModel> GetTripsAsync();
    }
}
