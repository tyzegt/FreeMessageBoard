using FMB.Core.API.Data;
using FMB.Core.API.Services.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FMB.Core.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class BaseFMBController : ControllerBase
    {
        protected readonly UserManager<AppUser> _userManager;
        protected readonly IConfiguration _configuration;

        public BaseFMBController(
            UserManager<AppUser> userManager,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public AppUser CurrentUser => _userManager.FindByNameAsync(User?.Identity?.Name ?? "").Result;
    }
}
