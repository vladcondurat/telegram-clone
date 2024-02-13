using Data.Infrastructure.UnitOfWork;
using Services.Constants;
using Services.Exceptions;
using Services.Mappers;

namespace Services.Features.Users;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

 

    public UserDto GetDetails(int? id)
    {
        var user = _unitOfWork.Users.GetById(id.Value);

        if (user is null) throw new UserException(ErrorCodes.GenericError, "This is a generic error");
        
        var mapper = new UserMapper();
        
        return mapper.UserToUserDto(user);
    }

}