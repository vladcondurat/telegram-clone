namespace WebApi.Models.Room;

public class RoomCardModel
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public string RoomName { get; set; } = string.Empty;
    public DateTime? LastMessageTime { get; set; }
    
    public int? LastMessageId { get; set; }
    public MessageModel? LastMessage { get; set; }
}