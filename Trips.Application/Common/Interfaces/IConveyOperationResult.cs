namespace Trips.Application.Common.Interfaces
{
    public interface IConveyOperationResult
    {
        public bool IsSuccessful { get; }
        public string? ResultMessage { get; }
    }
}
