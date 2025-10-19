using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace CourseRobot.WebApi.ExceptionHandler;

public class NoImplementedExceptionHandler(ILogger<CustomExceptionHandler> logger): IExceptionHandler
{
     public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is NotImplementedException)
            {
                logger.LogError(exception, exception.Message, exception.StackTrace);
                var response = new
                {
                    //StatusCode = (int)HttpStatusCode.NotImplemented,
                    Success = false,
                    Title = "Something went wrong.",
                    ErrorMessage = exception.Message// You may want to log this instead of returning it                    
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;

                await context.Response.WriteAsJsonAsync(response, cancellationToken);
                return true; // Indicate that the exception was handled
            }
            return false;
        }
}
