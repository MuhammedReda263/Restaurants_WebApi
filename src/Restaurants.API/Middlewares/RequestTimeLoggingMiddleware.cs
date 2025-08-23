
using System.Diagnostics;

namespace Restaurants.API.Middlewares
{
    public class RequestTimeLoggingMiddleware : IMiddleware
    {
        private readonly ILogger <RequestTimeLoggingMiddleware> _logger;

        public RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
           var stopWatch = Stopwatch.StartNew();
           await next.Invoke(context);
           stopWatch.Stop();
            if (stopWatch.ElapsedMilliseconds/100 > 4)
            {
                _logger.LogInformation("Request[{verb}] at {path} took {time}",context.Request.Method,context.Request.Path,stopWatch.ElapsedMilliseconds);
            }
        }
    }
}
