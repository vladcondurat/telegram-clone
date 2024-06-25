using Data.Constants;
using Data.Constants.Jwt;
using Data.Entities;
using Data.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Services.Constants;
using Services.Exceptions;
using Services.Features.Auth.Dtos;
using Services.Features.Auth.Jwt;
using Services.Mappers;

namespace Services.Features.Auth;

public sealed class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;
    
    public AuthService(IUnitOfWork unitOfWork, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _config = config;
    }

    public AuthTokenDto LoginUser(LoginDto loginDto)
    {
        var user = _unitOfWork.Users.GetUserByUsername(loginDto.Username);

        if (user == null)
        {
            throw new BusinessException(ErrorCodes.InvalidUsernameOrPassword,"Invalid username or password");
        }

        if (user.Password != loginDto.Password)
        {
            throw new BusinessException(ErrorCodes.InvalidUsernameOrPassword,"Invalid username or password");
        }

        return GenerateToken(user);
    }

    public void RegisterUser(RegisterDto registerDto)
    {
        var existingUser = _unitOfWork.Users.GetUserByUsername(registerDto.Username);

        if(existingUser is not null)
        {
            throw new BusinessException(ErrorCodes.UsernameAlreadyExists,
                "Username already in use");
        }

        var mapper = new RegisterMapper();
        var user = mapper.RegisterToUserEntity(registerDto);

        user.AvatarUrl = Defaults.DefaultUserImage;
        
        _unitOfWork.Users.Add(user);
        _unitOfWork.SaveChanges();
    }

    private AuthTokenDto GenerateToken(User user)
    {
        var jwt = new JwtBuilder(_config);
        jwt.AddClaim(JwtClaims.Id, user.Id.ToString());
        return new AuthTokenDto
        {
            Token = jwt.GetToken(),
        };
    }
}