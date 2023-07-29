using FMB.Core.API.Controllers.BaseController;
using FMB.Core.API.Models;
using FMB.Core.Data.Data;
using FMB.Core.Data.Models.Tags;
using FMB.Services.Tags;
using FMB.Services.Tags.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FMB.Core.API.Controllers
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
        public async Task<IActionResult> CreateTag([FromBody] CreateTagRequest request)
        {
            if(request == null) return new BadRequestObjectResult("empty request body");

            var tag = await _tagService.CreateTag(request.Name);
            if(tag == null)
                return BadRequest(new ErrorView("TagAlreadyExists"));

            return Ok(tag);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetTag(long id)
        {
            var tag = await _tagService.GetTag(id);
            if(tag == null)
                return BadRequest(new ErrorView("TagNotFound"));
            
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTag([FromBody] UpdateTagRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.NewName))
                return BadRequest(new ErrorView("EmptyNewName"));
            
            if (await _tagService.UpdateTag(request.Id, request.NewName))
                return Ok();
            
            return BadRequest(new ErrorView("TagNotFound"));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTag([FromBody] long id)
        {
            if(await _tagService.DeleteTag(id))
                return Ok();
            
            return BadRequest(new ErrorView("TagNotFound"));
        }

        [HttpPost]
        public async Task<IActionResult> AssignPostTag([FromBody] long tagId, [FromBody] long postId)
        {
            await _tagService.AssignPostTag(tagId, postId);
            return Ok();
        }

        [HttpPost]
        public async Task<IEnumerable<Tag>> GetPostTags([FromBody] long postId)
        {
            return await _tagService.GetPostTags(postId);
        }
    }
}
