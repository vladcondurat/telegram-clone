namespace Data.Entities;

public class Message
{
    public int Id { get; set; }
    public string? AttachedImageUrl { get; set; }
    public string? TextContent { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
}