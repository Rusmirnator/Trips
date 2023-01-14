namespace Trips.Application.Common.Interfaces
{
    public interface IManipulateData
    {
        public Task<IEnumerable<T>> GetDataAsync<T>();
        public Task<IEnumerable<T>> GetDataBySearchTermAsync<T>();
        public Task<TOut?> GetDatumAsync<TIn, TOut>(TIn selector);
        public Task<bool> CreateDatumAsync<T>(T newDatum);
        public Task<bool> UpdateDatumAsync<T>(T updatedDatum);
        public Task<bool> DeleteDatumAsync<T>(T deletedDatum);
    }
}
