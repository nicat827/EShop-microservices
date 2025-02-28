using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> 
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest: notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request} - Response={Response}, ResponseContent={ResponseData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            Stopwatch timer = new Stopwatch();
            timer.Start();

            var res = await next();

            timer.Stop();
            var elapsed = timer.Elapsed;
            if (elapsed.Seconds >= 3)
                logger.LogWarning("[SLOW] The request={Request} took {TimeTaken} seconds",
                    typeof(TRequest).Name, elapsed.Seconds);

            logger.LogInformation("[END] Handled request={Request}. Time: {Time}, Response={Response}",
                typeof(TRequest).Name,elapsed.Milliseconds, typeof(TResponse).Name);

            return res;
        }
    }
}
