using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;

namespace WebApi.Models.Room;

public class UpdateRoomModel
{
    [Image]
    public IFormFile? ImageUrl { get; set; } 
    public string? RoomName { get; set; } 
    
}
