using FMB.Core.API.Controllers.BaseController;
using FMB.Core.API.Models;
using FMB.Core.Data.Data;
using FMB.Core.Data.Models.Marks;
using FMB.Core.Data.Models.Posts;
using FMB.Services.Marks;
using FMB.Services.Marks.Models;
using FMB.Services.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FMB.Core.API.Controllers.Marks
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : BaseFMBController
    {
        private readonly IPostsService _postsService;
        private readonly IMarksService _marksService;
        public MarksController(IPostsService postsService, IMarksService marksService, IConfiguration config, UserManager<AppUser> userManager) : base(userManager, config)
        {
            _postsService = postsService;
            _marksService = marksService;
        }

        [HttpPost]
        public async Task<IActionResult> RatePost(RatePostRequest request)
        {
            if (!await _postsService.IsPostExists(request.Id))
            {
                return BadRequest(new ErrorView("PostNotFound"));
            }

            await _marksService.RatePost(request.Id, request.Plus ? MarkEnum.Upvote : MarkEnum.Downvote, CurrentUser.Id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<GetRatingResponse>> GetPostRating(long postId)
        {
            if (!await _postsService.IsPostExists(postId))
            {
                return BadRequest(new ErrorView("PostNotFound"));
            }

            return GetRatingResponse.MapFrom(await _marksService.GetPostRating(postId));
        }
    }
}
