using Microsoft.EntityFrameworkCore;
using Trips.Application.Common.Interfaces;
using Trips.Application.Common.Mappings;
using Trips.Application.Common.Models;

namespace Trips.Infrastructure.Services
{
    public class TripService : IManipulateData
    {
        private readonly ITripsDbContext context;

        public TripService(ITripsDbContext context)
        {
            this.context = context;
        }

        public Task<bool> CreateDatumAsync<T>(T newDatum)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDatumAsync<T>(T deletedDatum)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetDataAsync<T>()
        {
            var data = (await context.Trips.ToListAsync()).Select(entity => entity.ToModel());

            return data as IEnumerable<T> ?? Enumerable.Empty<T>();
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
