using Riok.Mapperly.Abstractions;
using Services.Features.Auth;
using Services.Features.Auth.Dtos;
using WebApi.Models.Identity;

namespace WebApi.Mappers;

[Mapper]
public partial class IdentityMapper
{
    public partial RegisterDto RegistrationModelToRegistrationDto(RegistrationModel registrationModel);
    public partial LoginDto LoginModelToLoginDto(LoginModel loginModel);
    public partial AuthTokenModel AuthTokenDtoToAuthTokenModel(AuthTokenDto authTokenDto);
}
