using Data.Entities;
using Riok.Mapperly.Abstractions;
using Services.Features.Auth;

namespace Services.Mappers;

[Mapper]
public partial class RegisterMapper
{
    public partial User RegisterToUserEntity(RegisterDto registerDto);
}