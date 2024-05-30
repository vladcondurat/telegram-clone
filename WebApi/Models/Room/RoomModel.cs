using System.ComponentModel.DataAnnotations;
using WebApi.Models.Messages;

namespace WebApi.Models.Room;

public class RoomModel
{
    [Required]
    public int Id { get; set; }
    [Url]
    public string? ImageUrl { get; set; }
    [Required]
    public string RoomName { get; set; } = string.Empty;
    
    public IEnumerable<MessageModel> Messages { get; set; } = new List<MessageModel>();
}