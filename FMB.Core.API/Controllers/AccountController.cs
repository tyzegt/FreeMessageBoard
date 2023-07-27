using FMB.Core.API.Data;
using FMB.Core.API.Extentions;
using FMB.Core.API.Models.Identity;
using FMB.Core.API.Services.Identity;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace FMB.Core.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseFMBController
    {
        private readonly IdentityContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(
            UserManager<AppUser> userManager, 
            IdentityContext context, 
            ITokenService tokenService, 
            IConfiguration configuration) 
            : base(userManager, configuration)
        {
            _context = context;
            _tokenService = tokenService;
        }


        [HttpPost]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(request);
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if(user == null)
            {
                return BadRequest("Wrong login or password");
            }

            var passValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (passValid == null)
            {
                return BadRequest("Wrong login or password");
            }

            user = _context.Users.FirstOrDefault(x => x.UserName ==  request.UserName);
            if(user == null) { return Unauthorized(); }

            var roleIds = await _context.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId).ToListAsync();
            var roles = await _context.Roles.Where(x => roleIds.Contains(x.Id)).ToListAsync();

            var accessToken = _tokenService.CreateToken(user, roles);
            user.RefreshToken = _configuration.GenerateRefreshToken();
            user.RefreshTokenExpirationTime = DateTime.Now.AddDays(_configuration.GetSection("Jwt:refreshTokenExpirationDays").Get<int>());

            await _context.SaveChangesAsync();

            return Ok(new AuthResponse
            {
                Username = user.UserName!,
                Token = accessToken,
                RefreshToken = user.RefreshToken
            });
        }

        [HttpPost]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }

            var user = new AppUser
            {
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            if(!result.Succeeded) { return BadRequest(request); }

            user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if(user == null) { throw new Exception($"user {request.UserName} not found"); }

            await _userManager.AddToRoleAsync(user, RoleConsts.Member);

            return await Login(new AuthRequest()
            {
                UserName = request.UserName,
                Password = request.Password
            });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var accessToken = tokenModel.AccessToken;
            var refreshToken = tokenModel.RefreshToken;
            var principal = _configuration.GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var username = principal.Identity!.Name;
            var user = await _userManager.FindByNameAsync(username!);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpirationTime <= DateTime.UtcNow)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = _configuration.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _configuration.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return Ok();
        }
    }
}
