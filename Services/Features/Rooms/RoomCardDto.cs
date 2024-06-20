using Services.Features.Messages;

namespace Services.Features.Rooms;

public class RoomCardDto
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; } 
    public string RoomName { get; set; } = string.Empty;
    
    public MessageDto? LastMessage { get; set; } 
}