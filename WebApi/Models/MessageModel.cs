using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class MessageModel
{
    public int Id { get; set; }
    [Url]
    public string? AttachedImageUrl { get; set; }
    public string? TextContent { get; set; }
    
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;
}
