using Data.Infrastructure.S3;

namespace Services.Features.Users;

public class UpdateUserDto
{
    public string? Username { get; set; } 
    public string? Email { get; set; } 
    public ImageDto? AvatarImg { get; set; } 
}