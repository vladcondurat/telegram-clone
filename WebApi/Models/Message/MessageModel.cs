using System.ComponentModel.DataAnnotations;
using WebApi.Models.Identity;

namespace WebApi.Models.Message;

public class MessageModel
{
    public int Id { get; set; }
    [Url]
    public string? AttachedImageUrl { get; set; }
    public string? TextContent { get; set; }
    
    public int UserId { get; set; }
    public UserPreviewModel User { get; set; } = null!;
}
