using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Features.Auth;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Mappers;
using WebApi.Models;
using WebApi.Models.Identity;

namespace WebApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/auth")]
public class AuthController : ApplicationController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [SwaggerOperation(Description = "Authenticates a user and returns a JWT token")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult Login(LoginModel loginModel)
    {
        var mapper = new IdentityMapper();
        var loginDto = mapper.LoginModelToLoginDto(loginModel);
        var authTokenDto =_authService.LoginUser(loginDto);
        var authTokenModel = mapper.AuthTokenDtoToAuthTokenModel(authTokenDto);
        return Ok(authTokenModel);
    }
    
    [HttpPost("register")]
    [SwaggerOperation(Description = "Registers a new user")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult Register(RegistrationModel registrationModel)
    {
        var mapper = new IdentityMapper();
        var registrationDto = mapper.RegistrationModelToRegistrationDto(registrationModel);
        _authService.RegisterUser(registrationDto);
        return NoContent();
    }

}