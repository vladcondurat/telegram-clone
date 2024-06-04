using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;

namespace WebApi.Models.Identity;

public class LoginModel
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}