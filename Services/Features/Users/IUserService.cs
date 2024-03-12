namespace Services.Features.Users;

public interface IUserService
{
    UserDto GetDetails(int? id);
    UserDto GetUserByUsername(string? username);
}