using Data.Infrastructure.S3;
using Microsoft.AspNetCore.Http;

namespace Services.Features.Users;

public class UpdateUserDto
{
    public string? Username { get; set; } 
    public string? Email { get; set; } 
    public IFormFile? AvatarImg { get; set; } 
}