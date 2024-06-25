namespace Services.Features.Users.Dtos;

public class GetUsersDto
{
    public IEnumerable<UserDto> Users {get; set;} = new List<UserDto>();
}