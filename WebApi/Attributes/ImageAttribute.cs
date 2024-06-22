using System.ComponentModel.DataAnnotations;

namespace WebApi.Attributes;

public class ImageAttribute : ValidationAttribute
{
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
    private readonly string[] _allowedContentTypes = { "image/jpeg", "image/png", "image/gif", "image/bmp" };

    public override bool IsValid(object? value)
    {
        var file = value as IFormFile;

        if (file == null)
        {
            // If no file is provided, it's valid (optional field)
            return true;
        }

        if (!_allowedContentTypes.Contains(file.ContentType))
        {
            return false;
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        
        if (string.IsNullOrEmpty(extension) || !_allowedExtensions.Contains(extension))
        {
            return false;
        }

        return true;
    }
}