using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Data.Constants.Jwt;

public class JwtBuilder
{
    private readonly JwtHeader _jwtHeader;
    private readonly JwtPayload _jwtPayload;

    public JwtBuilder(IConfiguration configuration)
    {
        _jwtHeader = CreateJwtHeader(configuration);
        _jwtPayload = CreateJwtPayload(configuration);
    }

    public JwtBuilder AddClaim(Claim claim)
    {
        _jwtPayload.AddClaim(claim);
        return this;
    }

    public JwtBuilder AddClaim(string claimName, string claimValue)
    {
        var claim = new Claim(claimName, claimValue);
        _jwtPayload.AddClaim(claim);
        return this;
    }
    
    public string GetToken()
    {
        var jwtToken = new JwtSecurityToken(_jwtHeader, _jwtPayload);
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    private JwtHeader CreateJwtHeader(IConfiguration configuration)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        return new JwtHeader(credentials);
    }

    private JwtPayload CreateJwtPayload(IConfiguration configuration)
    {
        var audience = configuration["JwtSettings:Audience"];
        var issuer = configuration["JwtSettings:Issuer"];
        var tokenLifetimeInSeconds = int.Parse(configuration["JwtSettings:TokenLifetimeSeconds"]!);
        return new JwtPayload(issuer, audience, new List<Claim>(), null, DateTime.Now.AddSeconds(tokenLifetimeInSeconds), DateTime.Now);
    }
}
