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


        [TestMethod()]
        public void GetCommentsTest()
        {
            var testPostId = PostsController.CreatePost(new Data.Models.Posts.CreatePostRequest { Title = "test", Body = "test" }).Result.Value;

            var comment1_0 = CommentsController.CreateComment(new CreateCommentRequest
            {
                Body = "comment1",
                Id = testPostId
            }).Result;
            var comment1_1 = CommentsController.CreateComment(new CreateCommentRequest
            {
                Body = "comment1",
                Id = testPostId,
                ParentCommentId = comment1_0.Value
            }).Result;
            var comment1_2 = CommentsController.CreateComment(new CreateCommentRequest
            {
                Body = "comment1",
                Id = testPostId,
                ParentCommentId = comment1_1.Value
            }).Result;

            var getCommentsResponse = CommentsController.GetCommentsByPostId(testPostId).Result;

            Assert.IsTrue(getCommentsResponse.Any(x => x.Id == comment1_0.Value));
            Assert.IsTrue(getCommentsResponse.Any(x => x.Id == comment1_1.Value));
            Assert.IsTrue(getCommentsResponse.Any(x => x.Id == comment1_2.Value));
            Assert.IsTrue(getCommentsResponse.Any(x => x.ParentCommentId == comment1_0.Value));
            Assert.IsTrue(getCommentsResponse.Any(x => x.ParentCommentId == comment1_1.Value));
        }
    }
}
