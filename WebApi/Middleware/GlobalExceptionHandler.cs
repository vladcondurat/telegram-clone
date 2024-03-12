using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;

namespace WebApi.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is EntityNotFoundException)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        } 
        if (exception is AuthorizationException)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Not Found",
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden; //403

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
        else
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };

            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError; //500

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}