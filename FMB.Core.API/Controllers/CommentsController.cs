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

namespace FMB.Core.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;   
        public CommentsController(ICommentsService commentsService)
        { 
            _commentsService = commentsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
        {
            if(request == null) return new BadRequestObjectResult("empty request body");

            try
            {
                await _commentsService.CreateCommentAsync(request.Comment);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok();
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
            await _commentsService.DeleteCommentAsync(commentId);
        }
        [HttpPost]
        public async Task UpdateCommentAsync([FromBody] UpdateCommentRequest request)
        {
            await _commentsService.UpdateCommentAsync(request.Id, request.NewBody);
        }
    }
}