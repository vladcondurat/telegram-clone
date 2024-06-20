using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Room;

public class UpdateRoomModel
{
    public FileUploadModel? ImageUrl { get; set; } 
    public string? RoomName { get; set; } 
    
}
