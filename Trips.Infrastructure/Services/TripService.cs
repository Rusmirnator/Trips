using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trips.Application.Interfaces;
using Trips.Application.Models;

namespace Trips.Infrastructure.Services
{
    public class TripService : IManipulateData
    {
        public Task<bool> CreateDatumAsync<T>(T newDatum)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDatumAsync<T>(T deletedDatum)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetDataAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetDataBySearchTermAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<TOut?> GetDatumAsync<TIn, TOut>(TIn selector)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDatumAsync<T>(T updatedDatum)
        {
            throw new NotImplementedException();
        }
    }
}
