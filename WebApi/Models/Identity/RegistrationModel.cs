using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;

namespace WebApi.Models.Identity;

public class RegistrationModel
{
    [Required]
    [MinLength(3)]
    public string Username { get; set; } = string.Empty;
    [Required]
    [Password]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
    [Required] 
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}