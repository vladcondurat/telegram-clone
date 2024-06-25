namespace Data.Entities;

public class Room
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public bool IsGroup { get; set; } 
    public DateTime LastActive { get; set; } = DateTime.Now;
    
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();
    public IEnumerable<User> Users { get; set; } = new List<User>();
    public IEnumerable<UserRoom> UserRooms { get; set; } = new List<UserRoom>();

} 