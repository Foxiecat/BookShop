using Microsoft.AspNetCore.Diagnostics;

namespace BookShop.Middleware;

public class GlobalExceptionHandling(ILogger<GlobalExceptionHandling> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandling> _logger;
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
                                                Exception exception,
                                                CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception,
            "Could not process a request on Machine {MachineName}. TraceID:{TraceId}",
            Environment.MachineName, httpContext.TraceIdentifier);
        
        // Mapping
        // StatusCode and Title
        (int statusCode, string title) = MapException(exception);

        await Results.Problem(
            title: title,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>
            {
                { "traceId", httpContext.TraceIdentifier }
            }).ExecuteAsync(httpContext);

        // Stopping the pipeline
        return true;
    }

    private static (int statusCode, string title) MapException(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => (StatusCodes.Status400BadRequest, "You made a mistake!"),
            BadHttpRequestException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error"),
        };
    }
}