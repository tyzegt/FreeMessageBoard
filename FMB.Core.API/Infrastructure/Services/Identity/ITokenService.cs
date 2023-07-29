using FMB.Core.Data.Data;
using Microsoft.AspNetCore.Identity;

namespace FMB.Core.API.Infrastructure.Services.Identity;

public interface ITokenService
{
    string CreateToken(AppUser user, List<IdentityRole<long>> role);
}