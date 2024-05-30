using Data.Entities;
using Riok.Mapperly.Abstractions;
using Services.Features.Auth;

namespace Services.Mappers;

[Mapper]
public partial class RegisterMapper
{
    public User RegisterToUserEntity(RegisterDto registerDto)
    {
        var user = new User
        {
            Username = registerDto.Username,
            Password = registerDto.Password,
            Email = registerDto.Email
        };
        return user;
    }
}