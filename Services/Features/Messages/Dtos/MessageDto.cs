using Services.Features.Users;

namespace Services.Features.Messages;

public class MessageDto
{
    public int Id { get; set; }
    public string? AttachedImageUrl { get; set; }
    public string? TextContent { get; set; } 
    public DateTime CreatedAt { get; set; }
    
    public int UserId { get; set; }
    public UserDto User { get; set; } = null!;
}