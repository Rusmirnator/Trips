namespace Trips.Application.Common.Interfaces
{
    /// <summary>
    /// Facilitates expected operation result presentation and indirectly handling.
    /// </summary>
    public interface IConveyOperationResult
    {
        public bool IsSuccessful { get; }
        public string? Message { get; }
    }
}
