using FMB.Core.API.Controllers.BaseController;
using FMB.Core.API.Infrastructure.Services;
using FMB.Core.API.Models;
using FMB.Core.Data.Data;
using FMB.Core.Data.Models.Comments;
using FMB.Services.Comments;
using FMB.Services.Posts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FMB.Core.API.Controllers
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
            if (request == null)
                return BadRequest(new ErrorView("EmptyRequestBody"));

            if (!TextValidator.IsCommentValid(request.Body))
                return BadRequest(new ErrorView("InvalidCommentBody"));

            if(!await _postService.IsPostExists(request.Id))
                return BadRequest(new ErrorView("PostNotFound"));

            if(request.ParentCommentId > 0) 
            { 
                if(!await _commentsService.IsCommentExists(request.ParentCommentId, request.Id))
                    return BadRequest(new ErrorView("ParentCommentNotFound"));
            }

            try
            {
                return await _commentsService.CreateCommentAsync(request.Id, request.ParentCommentId, request.Body, CurrentUser.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorView(ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommentsByPostIdAsync(long postId)
        {
            var comments = await _commentsService.GetAllCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        [HttpGet] 
        public async Task<IActionResult> GetCommentByIdAsync([FromBody] GetCommentRequest request)
        {
            var comment = await _commentsService.GetCommentAsync(request.Id);
            if (comment == null)
                return BadRequest(new ErrorView("CommentNotFound"));

            return Ok(comment);
        }

        [HttpDelete]
        public async Task DeleteCommentAsync(long commentId)
        {
            // TODO only for moderator/admin
            await _commentsService.DeleteCommentAsync(commentId);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCommentAsync([FromBody] UpdateCommentRequest request)
        {
            if(string.IsNullOrEmpty(request.NewBody)) return BadRequest(new ErrorView("Empty body"));
            var targetComment = await _commentsService.GetCommentAsync(request.Id);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if(targetComment != null 
                && targetComment.UserId == currentUser.Id 
                && (DateTime.UtcNow - targetComment.CreatedAt).Minutes < 30 )
            { 
                await _commentsService.UpdateCommentAsync(request.Id, request.NewBody);
                return Ok(targetComment);
            } 
            return BadRequest(new ErrorView("You're not allowed to edit this"));
        }
    }
}