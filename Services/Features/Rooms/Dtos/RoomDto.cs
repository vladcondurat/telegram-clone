using Services.Features.Messages;
using Services.Features.Messages.Dtos;

namespace Services.Features.Rooms.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public bool IsGroup { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public DateTime LastActive { get; set; }
    
    public IEnumerable<MessageDto> Messages { get; set; } = new List<MessageDto>();
}