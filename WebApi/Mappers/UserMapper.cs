using Riok.Mapperly.Abstractions;
using Services.Features.Users;
using WebApi.Models;

namespace WebApi.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial UserModel UserDtoToUserModel(UserDto userDto);
}