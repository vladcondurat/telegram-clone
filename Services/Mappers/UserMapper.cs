using Data.Entities;
using Riok.Mapperly.Abstractions;
using Services.Features.Users;
using Services.Features.Users.Dtos;

namespace Services.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial UserDto UserToUserDto(User user);
}