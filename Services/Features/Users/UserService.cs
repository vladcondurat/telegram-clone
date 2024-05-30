using Data.Entities;
using Data.Infrastructure.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
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
    
    public UserPreviewDto GetUser(int id)
    {
        var user = _unitOfWork.Users.GetUserByUserId(id);

        if (user is null)
        {
            throw new EntityNotFoundException(ErrorCodes.UserNotFound,id, typeof(User));
        }
        
        var mapper = new UserMapper();
        return mapper.UserToUserPreviewDto(user);
    }
    
}