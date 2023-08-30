using FMB.Core.API.Controllers.BaseController;
using FMB.Core.API.Models;
using FMB.Core.Data.Data;
using FMB.Core.Data.Models.Marks;
using FMB.Core.Data.Models.Posts;
using FMB.Services.Comments;
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
        private readonly ICommentsService _commentsService;
        private readonly IMarksService _marksService;
        public MarksController(
            IPostsService postsService, 
            IMarksService marksService,
            ICommentsService commentsService,
            IConfiguration config, 
            UserManager<AppUser> userManager) : base(userManager, config)
        {
            _postsService = postsService;
            _marksService = marksService;
            _commentsService = commentsService;
        }

        [HttpPost]
        public async Task<IActionResult> RatePost(RateRequest request)
        {
            if (!await _postsService.IsPostExists(request.Id))
            {
                return BadRequest(new ErrorView("PostNotFound"));
            }

            await _marksService.RatePost(request.Id, request.Upvote ? MarkEnum.UpVote : MarkEnum.DownVote, CurrentUser.Id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<RatingDto>> GetPostRating(long postId)
        {
            if (!await _postsService.IsPostExists(postId))
            {
                return BadRequest(new ErrorView("PostNotFound"));
            }

            return await _marksService.GetPostRating(postId);
        }

        [HttpPost]
        public async Task<IActionResult> RateComment(RateRequest request)
        {
            if (!await _commentsService.IsCommentExists(request.Id))
            {
                return BadRequest(new ErrorView("CommentNotFound"));
            }

            await _marksService.RateComment(request.Id, request.Upvote ? MarkEnum.UpVote : MarkEnum.DownVote, CurrentUser.Id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<RatingDto>> GetCommentRating(long commentId)
        {
            if (!await _commentsService.IsCommentExists(commentId))
            {
                return BadRequest(new ErrorView("CommentNotFound"));
            }

            return await _marksService.GetCommentRating(commentId);
        }

        [HttpPost]
        public async Task<ActionResult<List<RatingDto>>> GetCommentsRating(List<long> commentIds)
        {
            if (!await _commentsService.IsCommentsExists(commentIds.ToArray()))
            {
                return BadRequest(new ErrorView("CommentNotFound"));
            }

            return await _marksService.GetCommentsRating(commentIds);
        }
    }
}
