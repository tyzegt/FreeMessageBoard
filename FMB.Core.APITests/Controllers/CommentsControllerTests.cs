using FMB.Core.API.Controllers.Comments;
using FMB.Core.API.Controllers.Posts;
using FMB.Core.Data.Models.Comments;
using FMB.Services.Comments;
using FMB.Services.Comments.Models;
using FMB.Services.Posts;
using FMB.Services.Posts.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FMB.Core.APITests.Controllers
{
    [TestClass()]
    public class CommentsControllerTests
    {
        PostsContext PostsContext; // TODO consider standalone or fake context for api tests
        PostsController PostsController;
        CommentsContext CommentsContext;
        CommentsController CommentsController;

        public CommentsControllerTests()
        {
            PostsContext = new PostsContext();
            CommentsContext = new CommentsContext();

            var postsService = new PostsService(PostsContext);
            var fakeUserManager = FakeUserManager.GetInstance();

            PostsController = new PostsController(postsService, Mock.Of<IConfiguration>(), fakeUserManager);
            CommentsController = new CommentsController(new CommentsService(CommentsContext), postsService, Mock.Of<IConfiguration>(), fakeUserManager);
        }

        [TestMethod()]
        public void CreateCommentTest()
        {
            var commentText = "commentText";

            var testPostId = PostsController.CreatePost(new Data.Models.Posts.CreatePostRequest { Title = "test", Body = "test" }).Result.Value;

            var createCommentResponse = CommentsController.CreateComment(new CreateCommentRequest
            {
                Body = commentText,
                Id = testPostId
            }).Result;
            Assert.IsNotNull(createCommentResponse);

            var createdComment = CommentsContext.Comments.FirstOrDefault(x => x.Id == createCommentResponse.Value);
            Assert.IsNotNull(createdComment);
            Assert.AreEqual(createdComment.Body, commentText);
            Assert.AreEqual(createdComment.PostId, testPostId);

            CommentsContext.Remove(createdComment);
            CommentsContext.SaveChanges();
        }
    }
}
