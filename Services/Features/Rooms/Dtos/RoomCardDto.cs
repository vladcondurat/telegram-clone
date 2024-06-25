using Services.Features.Messages.Dtos;

namespace Services.Features.Rooms.Dtos;

public class RoomCardDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    
    public MessageDto? LastMessage { get; set; } 
}