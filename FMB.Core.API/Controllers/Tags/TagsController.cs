using FMB.Core.API.Controllers.BaseController;
using FMB.Core.Data.Data;
using FMB.Core.Data.Models.Tags;
using FMB.Services.Tags;
using FMB.Services.Tags.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FMB.Core.API.Controllers.Tags
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TagsController : BaseFMBController
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService, IConfiguration config, UserManager<AppUser> userManager) : base(userManager, config)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public ActionResult<long> CreateTag([FromBody] CreateTagRequest request)
        {
            if (request == null) return new BadRequestObjectResult("empty request body");

            try
            {
                return _tagService.CreateTag(request.Name);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok();
        }

        [HttpGet]
        public ActionResult<Tag> GetTag(long id)
        {
            try
            {
                return _tagService.GetTag(id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult UpdateTag([FromBody] UpdateTagRequest request)
        {
            if (request == null) return new BadRequestObjectResult("empty request body");

            try
            {
                _tagService.UpdateTag(request.Id, request.NewName);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteTag([FromBody] long id)
        {
            try
            {
                _tagService.DeleteTag(id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok();
        }
    }
}
