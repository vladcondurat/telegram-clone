using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CreateMessageModel
{
    [Url]
    public string? AttachedImageUrl { get; set; }
    public string? TextContent { get; set; }
}