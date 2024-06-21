namespace Services.Features.Users;

public class UserIdsDto
{
    public IEnumerable<int> UserIds { get; set; } = new List<int>();
}