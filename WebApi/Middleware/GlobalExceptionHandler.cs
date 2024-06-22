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
        ProblemDetails problemDetails;

        switch (exception)
        {
            case EntityNotFoundException:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Not Found",
                    Detail = exception.Message,
                    Instance = httpContext.Request.Path
                };
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                break;

            case AuthorizationException:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Title = "Forbidden",
                    Detail = exception.Message,
                    Instance = httpContext.Request.Path
                };
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                break;

            case BusinessException:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = exception.Message,
                    Instance = httpContext.Request.Path
                };
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            default:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error",
                    Detail = exception.Message,
                    Instance = httpContext.Request.Path
                };

                _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}