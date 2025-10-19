using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json; // Add this line
using System.Threading; // Add this line
using System.Threading.Tasks; // Add this line

namespace CourseRobot.WebApi
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not NotImplementedException)
            {
                logger.LogError(exception, exception.Message, exception.StackTrace);
                var response = new
                {
                    //StatusCode = (int)HttpStatusCode.InternalServerError,
                    Success = false,
                    Title = "Something went wrong.",
                    ErrorMessage = exception.Message  // You may want to log this instead of returning it                   
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(response, cancellationToken);
               
                return true; // Indicate that the exception was handled
            }
            return false;          
        }
    }
}
