using System.IdentityModel.Tokens.Jwt;
using FMB.Core.Data.Data;
using FMB.Core.Data.Extentions;
using Microsoft.AspNetCore.Identity;

namespace FMB.Core.API.Infrastructure.Services.Identity;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(AppUser user, List<IdentityRole<long>> roles)
    {
        var token = user
            .CreateClaims(roles)
            .CreateJwtToken(_configuration);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}