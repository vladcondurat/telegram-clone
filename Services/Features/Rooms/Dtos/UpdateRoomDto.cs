using Data.Infrastructure.S3;
using Microsoft.AspNetCore.Http;

namespace Services.Features.Rooms;

public class UpdateRoomDto
{
    public IFormFile? Image { get; set; } 
    public string? RoomName { get; set; }
}