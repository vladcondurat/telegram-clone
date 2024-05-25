using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;

namespace WebApi.Models;

public class LoginModel
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    [Password]
    public string Password { get; set; } = string.Empty;
}