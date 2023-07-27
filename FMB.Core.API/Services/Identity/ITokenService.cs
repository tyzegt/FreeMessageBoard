using FMB.Core.API.Data;
using Microsoft.AspNetCore.Identity;

namespace FMB.Core.API.Services.Identity;

public interface ITokenService
{
    string CreateToken(AppUser user, List<IdentityRole<long>> role);
}