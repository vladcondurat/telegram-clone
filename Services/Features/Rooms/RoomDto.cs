using Services.Features.Messages;
using Services.Features.Users;

namespace Services.Features.Rooms;

public class RoomDto
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; } 
    public string RoomName { get; set; } = string.Empty;
    
    public IEnumerable<MessageDto> Messages { get; set; } = new List<MessageDto>();
}