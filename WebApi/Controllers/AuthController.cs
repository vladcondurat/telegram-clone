using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Features.Auth;
using WebApi.Mappers;
using WebApi.Models;

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
    public ActionResult<string> Login(LoginModel loginModel)
    {
        var mapper = new IdentityMapper();
        
        var token =_authService.LoginUser(mapper.LoginModelToLoginDto(loginModel));
        return Ok(token);
    }
    
    [HttpPost("register")]
    public ActionResult Register(RegistrationModel registrationModel)
    {
        var mapper = new IdentityMapper();
        _authService.RegisterUser(mapper.RegistrationModelToRegistrationDto(registrationModel));
        return StatusCode(201);
    }

}