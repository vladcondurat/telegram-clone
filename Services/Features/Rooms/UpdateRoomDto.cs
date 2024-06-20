using Data.Infrastructure.S3;
using Microsoft.AspNetCore.Http;

namespace Services.Features.Rooms;

public class UpdateRoomDto
{
    public ImageDto? ImageUrl { get; set; } 
    public string? RoomName { get; set; }
}