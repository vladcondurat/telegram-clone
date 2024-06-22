using System.ComponentModel.DataAnnotations;
using WebApi.Models.Message;

namespace WebApi.Models.Room;

public class RoomModel
{
    public int Id { get; set; }
    [Url] 
    public string ImageUrl { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public bool IsGroup { get; set; }
    public DateTime LastActive { get; set; }
    
    public IEnumerable<MessageModel> Messages { get; set; } = new List<MessageModel>();
}