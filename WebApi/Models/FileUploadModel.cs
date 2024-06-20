using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public sealed class FileUploadModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string ContentType { get; set; }
    [Required]
    public byte[] Image { get; set; }
}
