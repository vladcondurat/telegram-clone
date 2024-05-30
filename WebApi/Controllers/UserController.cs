using Microsoft.AspNetCore.Mvc;
using Services.Features.Users;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Mappers;
using WebApi.Models;

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
    [SwaggerOperation(Description = "Retrieves the details of a user by their ID.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserPreviewModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult GetUserDetails()
    {
        ValidateUserId();
        var mapper = new UserMapper();
        var userDto = _userService.GetUser(UserId!.Value);
        return Ok(mapper.UserPreviewDtoToUserPreviewModel(userDto));
    }

}