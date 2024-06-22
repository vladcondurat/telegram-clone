using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;

namespace WebApi.Models.Room;

public class CreateRoomModel
{
    [Image]
    public IFormFile? ImageUrl { get; set; } 
    public string? RoomName { get; set; }
    public IEnumerable<int> UserIds { get; set; } = new List<int>();
}