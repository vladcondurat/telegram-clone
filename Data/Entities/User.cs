namespace Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }

    public IEnumerable<Message> Messages { get; set; } = new List<Message>();
    public IEnumerable<Room> Rooms { get; set; } = new List<Room>();
    public IEnumerable<UserRoom> UserRooms { get; set; } = new List<UserRoom>();

}