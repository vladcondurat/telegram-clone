using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;

namespace WebApi.Models.Identity;

public class UpdateUserModel
{
    public string? Username { get; set; } 
    [EmailAddress]
    public string? Email { get; set; } 
    [Image]
    public IFormFile? AvatarImg { get; set; }
}