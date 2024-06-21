using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Identity;

public class UpdateUserModel
{
    public string? Username { get; set; } 
    [EmailAddress]
    public string? Email { get; set; } 
    // public FileUploadModel? AvatarImg { get; set; } 
    public IFormFile? AvatarImg { get; set; }
}