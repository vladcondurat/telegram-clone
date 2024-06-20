using Microsoft.AspNetCore.Mvc;
using Services.Features.Users;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Mappers;
using WebApi.Models.Identity;

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
    
    [HttpPut]
    [SwaggerOperation(Description = "Updates the details of a user.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult UpdateUser(UpdateUserModel updateUserModel)
    {
        ValidateUserId();
        var mapper = new UserMapper();
        var updateUserDto = mapper.UpdateUserModelToUpdateUserDto(updateUserModel);
        var userDto = _userService.UpdateUser(updateUserDto,UserId!.Value);
        var userModel = mapper.UserDtoToUserModel(userDto);
        return Ok(userModel);
    }
    
    [HttpGet]
    [SwaggerOperation(Description = "Retrieves the details of a user by their ID.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult GetUserDetails()
    {
        ValidateUserId();
        var userDto = _userService.GetUser(UserId!.Value);
        var mapper = new UserMapper();
        var userModel = mapper.UserDtoToUserModel(userDto);
        return Ok(userModel);
    }

    [HttpGet("all")]
    [SwaggerOperation(Description = "Retrieves the details of all users.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult GetUsers()
    {
        ValidateUserId();
        var usersDto = _userService.GetUsers(UserId!.Value);
        var mapper = new UserMapper();
        var userModels = mapper.GetUsersDtoToGetUsersModel(usersDto);
        return Ok(userModels);
    }
    
    [HttpGet("in-room/{roomId}")]
    [SwaggerOperation(Description = "Retrieves all users inside a room.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult GetUsersInRoom(int roomId)
    {
        ValidateUserId();
        var usersDto = _userService.GetUsersInRoom(roomId, UserId!.Value);
        var mapper = new UserMapper();
        var userModels = mapper.GetUsersDtoToGetUsersModel(usersDto);
        return Ok(userModels);
    }
    
    [HttpGet("outside-room/{roomId}")]
    [SwaggerOperation(Description = "Retrieves all users outside a room.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult GetUsersOutsideRoom(int roomId)
    {
        ValidateUserId();
        var usersDto = _userService.GetUsersOutsideRoom(roomId, UserId!.Value);
        var mapper = new UserMapper();
        var userModels = mapper.GetUsersDtoToGetUsersModel(usersDto);
        return Ok(userModels);
    }
}