namespace Services.Features.Users;

public interface IUserService
{
    UserDto GetUser(int userId);
    GetUsersDto GetUsers(int userId);
    GetUsersDto GetUsersInRoom(int roomId, int userId);
    GetUsersDto GetUsersOutsideRoom(int roomId, int userId);
    UserDto UpdateUser(UpdateUserDto updateUserDto, int userId);
}