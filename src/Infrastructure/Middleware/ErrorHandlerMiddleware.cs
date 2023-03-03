using Domain.Helpers.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Infrastructure.Middleware
{
    internal class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// Custom Error Handler Middlware for requests. If an exception is thrown, this middleware will catch it and return a custom response
        /// based on the status code defined in the switch statement. If the exception is not handled, it will be logged and a 500 status code will be returned.
        /// This error handler will automatically log the error message provided in the exception message to the console.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                Guid id = Guid.NewGuid(); // Generate new id for this error


                switch (error)
                {
                    // Custom Exception - located in Core.Domain.Helpers.Exceptions
                    // You can add as many custom exceptions you would like to handle
                    // Remember to add them to the switch statement with a status code for the response
                    case CustomException ex:
                        // custom application error
                        _logger.LogError($"A Custom Exception Occured. ID: {id} - Message: {error.Message}");
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException ex:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        _logger.LogError(error, error.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new {
                    id = id.ToString(),
                    message = error?.Message,
                    statusCode = response.StatusCode,
                });
                await response.WriteAsync(result);
            }
        }
    }
}
