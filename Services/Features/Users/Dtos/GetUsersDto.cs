namespace Services.Features.Users;

public class GetUsersDto
{
    public IEnumerable<UserDto> Users {get; set;} = new List<UserDto>();
}