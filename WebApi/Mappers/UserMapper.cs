using Riok.Mapperly.Abstractions;
using Services.Features.Users;
using WebApi.Models.Identity;
using WebApi.Models.Room;

namespace WebApi.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial UserModel UserDtoToUserModel (UserDto userDto);
    public partial GetUsersModel GetUsersDtoToGetUsersModel(GetUsersDto getUsersDto);
    public partial UpdateUserDto UpdateUserModelToUpdateUserDto(UpdateUserModel updateUserModel);
    public partial UserIdsDto UserIdsModelToUserIdsDto(UserIdsModel userIdsModel);
}