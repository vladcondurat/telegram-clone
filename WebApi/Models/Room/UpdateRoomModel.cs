using WebApi.Attributes;

namespace WebApi.Models.Room;

public class UpdateRoomModel
{
    [Image]
    public IFormFile? Image { get; set; } 
    public string? RoomName { get; set; } 
    
}
