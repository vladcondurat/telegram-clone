using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Room;

public class UpdateRoomModel
{
    public IFormFile? ImageUrl { get; set; } 
    public string? RoomName { get; set; } 
    
}
