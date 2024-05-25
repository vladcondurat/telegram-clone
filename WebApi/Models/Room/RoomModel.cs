using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Room;

public class RoomModel
{
    [Required]
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    [Required]
    public string RoomName { get; set; } = string.Empty;
    
    public IEnumerable<UserModel> Users { get; set; } = new List<UserModel>();
    public IEnumerable<MessageModel> Messages { get; set; } = new List<MessageModel>();
}