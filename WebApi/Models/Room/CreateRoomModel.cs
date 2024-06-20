using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Room;

public class CreateRoomModel
{
    [Url]
    public string? ImageUrl { get; set; }
    public string RoomName { get; set; } = string.Empty;
    
    public IEnumerable<int> UserIds { get; set; } = new List<int>();
}