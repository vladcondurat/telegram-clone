using Data.Entities;
using Data.Infrastructure.UnitOfWork;
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
        //handle null id
        var user = _unitOfWork.Users.GetById(id.Value);

        if (user is null) throw new EntityNotFoundException(id.Value, typeof(User));
        
        var mapper = new UserMapper();
        
        return mapper.UserToUserDto(user);
    }

    public UserDto GetUserByUsername(string? username)
    {
        //handle null username
        var user = _unitOfWork.Users.GetByUsername(username);
        
        if (user is null) throw new EntityNotFoundException(user.Id, typeof(User));
        
        var mapper = new UserMapper();
        
        return mapper.UserToUserDto(user);
    }

}