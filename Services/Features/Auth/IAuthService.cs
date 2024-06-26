using Services.Features.Auth.Dtos;

namespace Services.Features.Auth;

public interface IAuthService
{
    AuthTokenDto LoginUser(LoginDto dto);
    void RegisterUser(RegisterDto dto);
}