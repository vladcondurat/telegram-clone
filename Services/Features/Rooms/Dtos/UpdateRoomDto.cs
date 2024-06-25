using Microsoft.AspNetCore.Http;

namespace Services.Features.Rooms.Dtos;

public class UpdateRoomDto
{
    public IFormFile? Image { get; set; } 
    public string? RoomName { get; set; }
}