using System.ComponentModel.DataAnnotations;
using WebApi.Models.Message;

namespace WebApi.Models.Room;

public class RoomCardModel
{
    [Required]
    public int Id { get; set; }
    [Url]
    public string? ImageUrl { get; set; }
    [Required]
    public string RoomName { get; set; } = string.Empty;
    public DateTime? LastMessageTime { get; set; }
    
    // public int? LastMessageId { get; set; }
    public MessageModel? LastMessage { get; set; }
}