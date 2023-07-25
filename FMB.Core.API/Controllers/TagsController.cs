using FMB.Core.API.Models;
using FMB.Services.Tags;
using FMB.Services.Tags.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FMB.Core.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public ActionResult CreateTag([FromBody] CreateTagRequest request)
        {
            if(request == null) return new BadRequestObjectResult("empty request body");

            try
            {
                _tagService.CreateTag(request.Name);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok();
        }

        [HttpGet]
        public ActionResult<Tag> GetTag(int id)
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
        public ActionResult DeleteTag([FromBody] int id)
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
