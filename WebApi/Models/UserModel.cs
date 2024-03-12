using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;

namespace WebApi.Models;

public class UserModel
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    [Password]
    public string Password { get; set; } = string.Empty;
    [Required] 
    public string Email { get; set; } = string.Empty;
}