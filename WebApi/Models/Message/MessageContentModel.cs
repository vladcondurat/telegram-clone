using WebApi.Attributes;

namespace WebApi.Models.Message;

public class MessageContentModel
{
    [Image]
    public IFormFile? AttachedImage { get; set; }
    public string? TextContent { get; set; }
}