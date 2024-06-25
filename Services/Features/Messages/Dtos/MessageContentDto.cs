using Microsoft.AspNetCore.Http;

namespace Services.Features.Messages.Dtos;

public class MessageContentDto
{
    public IFormFile? AttachedImage { get; set; }
    public string? TextContent { get; set; }
}