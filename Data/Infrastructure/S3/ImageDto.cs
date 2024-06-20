namespace Data.Infrastructure.S3;

public sealed class ImageDto
{
    public string Name { get; set; }
    public string ContentType { get; set; }
    public byte[] Image { get; set; }
}