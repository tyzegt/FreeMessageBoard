using FMB.Core.API.Controllers;
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
    public class CommentsControllerTests : BaseControllerTestsClass
    {

        public CommentsControllerTests() : base()
        {
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
