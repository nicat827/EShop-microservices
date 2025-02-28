using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Exceptions.Base;

namespace Shared.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error message: {message}, time: {time}", exception.Message, DateTime.UtcNow);

            int status = 500;
            if (exception is BaseException baseException)
                status = (int)baseException.StatusCode;

            httpContext.Response.StatusCode = status;
            await httpContext.Response.WriteAsJsonAsync(new {Status = status,Message = exception.Message},cancellationToken);
            
            return true;
        }
    }
}
