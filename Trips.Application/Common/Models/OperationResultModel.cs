using Trips.Application.Common.Interfaces;

namespace Trips.Application.Common.Models
{
    public class OperationResultModel : IConveyOperationResult
    {
        public bool IsSuccessful { get; }
        public string? ResultMessage { get; }
        public OperationResultModel(bool isSuccessful, string? resultMessage)
        {
            IsSuccessful = isSuccessful;
            ResultMessage = resultMessage;
        }
    }
}
