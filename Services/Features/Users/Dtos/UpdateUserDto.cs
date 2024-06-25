using Microsoft.AspNetCore.Http;

namespace Services.Features.Users.Dtos;

public class UpdateUserDto
{
    public string? Username { get; set; } 
    public string? Email { get; set; } 
    public IFormFile? AvatarImg { get; set; } 
}