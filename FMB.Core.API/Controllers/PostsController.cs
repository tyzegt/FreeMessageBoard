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
using FMB.Core.API.Data;
using Microsoft.AspNetCore.Identity;

namespace FMB.Core.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController : BaseFMBController
    {
        private readonly IPostsService _postsService;   
        public PostsController(IPostsService postsService, IConfiguration config, UserManager<AppUser> userManager) : base(userManager, config)
        { 
            _postsService = postsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
        {
            if(request == null) return new BadRequestObjectResult("empty request body");

            try
            {
                await _postsService.CreatePostAsync(request.Title, request.Body);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok();
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
            // TODO check author
            // TODO check body and title
            await _postsService.UpdatePostAsync(request.Id, request.NewBody, request.NewTitle);
        }
    }
}