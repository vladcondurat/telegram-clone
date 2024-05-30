namespace Data.Entities;

public class Room
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; } 
    public string RoomName { get; set; } = string.Empty;
    public DateTime LastMessageTime { get; set; } = DateTime.UtcNow;
    
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();
    public IEnumerable<User> Users { get; set; } = new List<User>();
    public IEnumerable<UserRoom> UserRooms { get; set; } = new List<UserRoom>();

} 