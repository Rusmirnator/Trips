using Trips.Application.Common.Interfaces;

namespace Trips.Application.Common.Models
{
    public class OperationResultModel : IConveyOperationResult
    {
        public bool IsSuccessful { get; }
        public string? Message { get; }
        public OperationResultModel(bool isSuccessful, string? resultMessage)
        {
            IsSuccessful = isSuccessful;
            Message = resultMessage;
        }
    }
}
