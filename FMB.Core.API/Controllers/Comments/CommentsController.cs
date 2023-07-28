using FMB.Core.API.Controllers.BaseController;
using FMB.Core.API.Infrastructure.Services;
using FMB.Core.Data.Data;
using FMB.Core.Data.Models.Comments;
using FMB.Services.Comments;
using FMB.Services.Comments.Models;
using FMB.Services.Posts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FMB.Core.API.Controllers.Comments
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentsController : BaseFMBController
    {
        private readonly ICommentsService _commentsService;
        private readonly IPostsService _postService;
        public CommentsController(ICommentsService commentsService, IPostsService postService, IConfiguration config, UserManager<AppUser> userManager) : base(userManager, config)
        {
            _commentsService = commentsService;
            _postService = postService;
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateComment([FromBody] CreateCommentRequest request)
        {
            if (request == null) { return new BadRequestObjectResult("empty request body"); }
            if (!TextValidator.IsCommentValid(request.Body)) { return new BadRequestObjectResult("invelid comment text"); }

            var post = await _postService.GetPostAsync(request.Id);
            if (post == null) { return new BadRequestObjectResult("post not found"); }

            if (request.ParentCommentId > 0)
            {
                var parentComment = await _commentsService.GetCommentAsync(request.ParentCommentId);
                if (parentComment == null) { return new BadRequestObjectResult("parent comment not found"); }
            }

            try
            {
                return await _commentsService.CreateCommentAsync(request.Id, request.ParentCommentId, request.Body, CurrentUser.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommentsByPostIdAsync(long postId)
        {
            var comments = await _commentsService.GetAllCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        [HttpGet]
        public async Task<Comment> GetCommentByIdAsync([FromBody] GetCommentRequest request)
        {
            return await _commentsService.GetCommentAsync(request.Id);
        }

        [HttpDelete]
        public async Task DeleteCommentAsync(long commentId)
        {
            // TODO only for moderator/admin
            await _commentsService.DeleteCommentAsync(commentId);
        }

        [HttpPost]
        public async Task UpdateCommentAsync([FromBody] UpdateCommentRequest request)
        {
            // TODO check comment author
            // TODO check if update allowed
            // TODO check body
            await _commentsService.UpdateCommentAsync(request.Id, request.NewBody);
        }
    }
}