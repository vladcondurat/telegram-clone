using Services.Features.Users;

namespace Services.Features.Rooms;

public class CreateRoomDto
{
    public string? ImageUrl { get; set; } 
    public string RoomName { get; set; } = string.Empty;
    
    public IEnumerable<int> UserIds { get; set; } = new List<int>();
}