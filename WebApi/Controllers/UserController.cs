using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Features.Users;
using WebApi.Mappers;

namespace WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ApplicationController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetUserDetails(int? userId)
    {
        var mapper = new UserMapper();
        var userDto = _userService.GetDetails(userId);
        return Ok(mapper.UserDtoToUserModel(userDto));
    }

}