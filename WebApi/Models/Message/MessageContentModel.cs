using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Message;

public class MessageContentModel
{
    [Url]
    public string? AttachedImageUrl { get; set; }
    public string? TextContent { get; set; }
}