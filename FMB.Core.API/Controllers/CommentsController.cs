using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FMB.Services.Comments;
using FMB.Core.API.Models;
using FMB.Core.API.Data;
using Microsoft.AspNetCore.Identity;
using FMB.Services.Posts;
using FMB.Core.API.Services;
using FMB.Services.Comments.Models;

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

            if(!await _postService.IsPostExists(request.PostId))
                return BadRequest(new ErrorView("PostNotFound"));

            if(request.ParentCommentId > 0) 
            { 
                if(!await _commentsService.IsCommentExists(request.ParentCommentId, request.PostId))
                    return BadRequest(new ErrorView("ParentCommentNotFound"));
            }

            try
            {
                return await _commentsService.CreateCommentAsync(request.PostId, request.ParentCommentId, request.Body, CurrentUser.Id);
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
        public async Task UpdateCommentAsync([FromBody] UpdateCommentRequest request)
        {
            // TODO check comment author
            // TODO check if update allowed
            // TODO check body
            await _commentsService.UpdateCommentAsync(request.Id, request.NewBody);
        }
    }
}