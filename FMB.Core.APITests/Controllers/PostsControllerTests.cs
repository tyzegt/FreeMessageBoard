using FMB.Core.API.Controllers.Posts;
using FMB.Core.Data.Models.Posts;
using FMB.Services.Posts;
using FMB.Services.Posts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FMB.Core.APITests.Controllers
{
    [TestClass()]
    public class PostsControllerTests : BaseControllerTestsClass
    {
        public PostsControllerTests() : base()
        {
        }

        [TestMethod()]
        public void CreatePostTest()
        {
            var postTitle = "Testing Post";
            var postBody = "Testing Post Body";

            var createPostResult = PostsController.CreatePost(new CreatePostRequest { Title = postTitle, Body = postBody }).Result;
            var createdPost = PostsContext.Posts.FirstOrDefault(x => x.Id == createPostResult.Value);

            Assert.IsNotNull(createdPost);
            Assert.AreEqual(createdPost.Title, postTitle);
            Assert.AreEqual(createdPost.Body, postBody);
            Assert.AreEqual(createdPost.UserId, -1);

            PostsContext.Remove(createdPost);
            PostsContext.SaveChanges();
            Assert.IsFalse(PostsContext.Posts.Any(x => x.Id == createPostResult.Value));
        }


    }
}
