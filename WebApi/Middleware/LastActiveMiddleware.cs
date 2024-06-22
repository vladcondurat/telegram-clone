using Services.Features.Auth.Jwt;
using Services.Features.Users;

namespace WebApi.Middleware;
public class LastActiveMiddleware : IMiddleware
{
    private readonly IUserService _userService;
    private readonly ILogger<LastActiveMiddleware> _logger;

    public LastActiveMiddleware(IUserService userService, ILogger<LastActiveMiddleware> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == JwtClaims.Id);
            
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                _userService.UpdateUserLastActive(userId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating user's last active timestamp.");
        }

        await next(context);
    }
}