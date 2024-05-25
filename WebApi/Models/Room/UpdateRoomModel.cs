using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Room;

public class UpdateRoomModel
{
    [Url]
    public string? ImageUrl { get; set; } 
    public string? RoomName { get; set; } 
    
}