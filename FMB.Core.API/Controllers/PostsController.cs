using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 
using Microsoft.Extensions.Logging;
using FMB.Services.Posts;
using FMB.Core.API.Models;

namespace FMB.Core.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;   
        public PostsController(IPostsService postsService)
        { 
            _postsService = postsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
        {
            if(request == null) return new BadRequestObjectResult("empty request body");

            try
            {
                await _postsService.CreatePostAsync(request.Post);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var posts = await _postsService.GetAllPostsAsync();
            return Ok(posts);
        }
        [HttpGet] 
        public async Task<Post> GetPostByIdAsync([FromBody] GetPostRequest request)
        {
            return await _postsService.GetPostAsync(request.Id);
        }
        [HttpDelete]
        public async Task DeletePostAsync(long postId)
        {
            await _postsService.DeletePostAsync(postId);
        }
        [HttpPost]
        public async Task UpdatePostAsync([FromBody] UpdatePostRequest request)
        {
            await _postsService.UpdatePostAsync(request.Id, request.NewBody, request.NewTitle);
        }
    }
}