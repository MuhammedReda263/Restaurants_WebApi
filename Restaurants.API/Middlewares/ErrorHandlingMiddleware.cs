
using Restaurants.Domin.Exceptions;

namespace Restaurants.API.Middlewares
{
    public class ErrorHandlingMiddleware  : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                  await next.Invoke(context);
            }
            catch (NotFoundException notFound)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(notFound.Message);
                _logger.LogWarning(notFound.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                context.Response.StatusCode= 500;
                await context.Response.WriteAsJsonAsync("Something went wrong");
            }
        }
    }
}
