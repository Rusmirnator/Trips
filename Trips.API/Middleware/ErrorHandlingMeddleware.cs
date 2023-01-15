using System.Net;
using System.Text.Json;
using Trips.Application.Common.Interfaces;
using Trips.Application.Common.Models;

namespace Trips.API.Middleware
{
    public class ErrorHandlingMeddleware : IMiddleware
    {
        #region Fields & Properties
        private readonly ILogger<ErrorHandlingMeddleware> logger;
        #endregion

        #region CTOR
        public ErrorHandlingMeddleware(ILogger<ErrorHandlingMeddleware> logger)
        {
            this.logger = logger;
        }
        #endregion

        #region Public API
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        #endregion

        #region Private API
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            HttpResponse response = context.Response;

            IConveyOperationResult operationResult;

            switch (exception)
            {
                case ApplicationException ex:

                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    operationResult = new OperationResultModel(false, ex.Message);
                    break;

                default:

                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    operationResult = new OperationResultModel(false, HttpStatusCode.InternalServerError.ToString());
                    break;
            }

            logger.LogError(exception.Message);

            await context.Response.WriteAsync(JsonSerializer.Serialize(operationResult));
        }
        #endregion
    }
}
