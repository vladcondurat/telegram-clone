using Services.Features.Users.Dtos;

namespace Services.Features.Messages.Dtos;

public class MessageDto
{
    public int Id { get; set; }
    public string? AttachedImageUrl { get; set; }
    public string? TextContent { get; set; } 
    public DateTime CreatedAt { get; set; }
    
    public int UserId { get; set; }
    public UserDto User { get; set; } = null!;
}