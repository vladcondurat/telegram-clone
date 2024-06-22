using Microsoft.AspNetCore.Http;

namespace Services.Features.Messages;

public class MessageContentDto
{
    public IFormFile? AttachedImage { get; set; }
    public string? TextContent { get; set; }
}