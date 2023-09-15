using FMB.Core.API.Controllers.BaseController;
using FMB.Core.API.Dto;
using FMB.Core.API.Infrastructure.Services;
using FMB.Core.Data.Data;
using FMB.Core.Data.Models.Posts;
using FMB.Services.Comments;
using FMB.Services.Marks;
using FMB.Services.Posts;
using FMB.Services.Posts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FMB.Core.API.Controllers.Posts
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PostsController : BaseFMBController
    {
        private readonly IPostsService _postsService; 
        private readonly ICommentsService _commentsService;
        private readonly IMarksService _marksService;
        public PostsController(
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
        public async Task<ActionResult<long>> CreatePost([FromBody] CreatePostRequest request)
        {
            if (request == null) { return new BadRequestObjectResult("empty request body"); }
            if (!TextValidator.IsPostTitleTextValid(request.Title)) { return new BadRequestObjectResult("invalid post title"); }
            if (!TextValidator.IsPostBodyTextValid(request.Body)) { return new BadRequestObjectResult("invalid post body"); }

            try
            {
                return await _postsService.CreatePost(request.Title, request.Body, CurrentUser.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }


        [HttpGet]
        public async Task<PostDto> GetPost([FromBody] GetPostRequest request) // TODO return DTO with rating, comments, tags
        {
            var postRequest = _postsService.GetPost(request.Id);
            var commentsRequest = _commentsService.GetCommentsByPostId(request.Id);
            var postRatingRequest = _marksService.GetPostRating(request.Id);
            var comments = await commentsRequest;
            var commentsRatingRequest = _marksService.GetCommentsRating(comments.Select(x => x.Id).ToList());
            Task.WaitAll(postRequest, postRatingRequest, commentsRatingRequest);

            var commentsTree = CommentDto.GetCommentsTree(comments.ToList(), commentsRatingRequest.Result.ToList());
            return 
                new PostDto
                {
                    Author = CurrentUser.UserName, // TODO
                    AuthorId = CurrentUser.Id,
                    CreatedAt = postRequest.Result.CreatedAt,
                    Comments = commentsTree,
                    Title = postRequest.Result.Title,
                    Body = postRequest.Result.Body,
                    PostId = postRequest.Result.Id,
                    UpVotes = postRatingRequest.Result.UpVotes,
                    DownVotes = postRatingRequest.Result.DownVotes
                };
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(long postId) // TODO only for moderators, consider archivation, add logging
        {
            await _postsService.DeletePost(postId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostRequest request)
        {
            // TODO check author
            // TODO check body and title
            await _postsService.UpdatePost(request.Id, request.NewBody, request.NewTitle);
            return Ok();
        }
    }
}