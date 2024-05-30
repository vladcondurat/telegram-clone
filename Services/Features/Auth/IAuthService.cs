namespace Services.Features.Auth;

public interface IAuthService
{
    string LoginUser(LoginDto dto);
    void RegisterUser(RegisterDto dto);
}