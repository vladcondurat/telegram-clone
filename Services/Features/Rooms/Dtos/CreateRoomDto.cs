using Microsoft.AspNetCore.Http;

namespace Services.Features.Rooms.Dtos;

public class CreateRoomDto
{
    public IFormFile? Image { get; set; } 
    public string? RoomName { get; set; } 
    
    public IEnumerable<int> UserIds { get; set; } = new List<int>();
}