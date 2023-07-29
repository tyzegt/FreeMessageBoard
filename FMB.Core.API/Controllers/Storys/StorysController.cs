using FMB.Core.API.Controllers.BaseController;
using FMB.Core.API.Infrastructure.Services.StorysServices;
using FMB.Core.API.Infrastructure.ViewModels;
using FMB.Core.Data.Data;
using FMB.Core.Data.Models.Storys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FMB.Core.API.Controllers.Storys
{

    //Шаблон пример

    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]/[action]")]
    //[ApiController, Authorize]
    public class StorysController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly IStoryService _storysService;
        private readonly IServiceEditor<Story> _serviceEditor;


        public StorysController(IdentityContext context, IStoryService storysService, IServiceEditor<Story> serviceEditor)
        {
            _context = context;
            _storysService = storysService;
            _serviceEditor = serviceEditor;
        }

        /// <summary>
        /// Список
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult> Get()
        {
            await _serviceEditor.Get();
            return Ok();
        }

        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Post([FromBody] StorysInfoViewModel model)
        {
            var result = new Story
            {
                Archive = false,
                Name = model.Name,
            };
            await _serviceEditor.Post(result);
            return Ok();
        }




    }
}
