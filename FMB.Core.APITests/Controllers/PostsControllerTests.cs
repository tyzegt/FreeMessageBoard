using FMB.Core.API.Controllers.Posts;
using FMB.Core.Data.Models.Posts;
using FMB.Services.Posts;
using FMB.Services.Posts.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FMB.Core.APITests.Controllers
{
    [TestClass()]
    public class PostsControllerTests
    {
        PostsContext Context; // TODO consider standalone or fake context for api tests
        PostsController Controller;

        public PostsControllerTests()
        {
            Context = new PostsContext();
            Controller = new PostsController(new PostsService(Context), Mock.Of<IConfiguration>(), FakeUserManager.GetInstance());
        }

        [TestMethod()]
        public void CreatePostTest()
        {
            var postTitle = "Testing Post";
            var postBody = "Testing Post Body";

            var createPostResult = Controller.CreatePost(new CreatePostRequest { Title = postTitle, Body = postBody }).Result;
            var createdPost = Context.Posts.FirstOrDefault(x => x.Id == createPostResult.Value);

            Assert.IsNotNull(createdPost);
            Assert.AreEqual(createdPost.Title, postTitle);
            Assert.AreEqual(createdPost.Body, postBody);
            Assert.AreEqual(createdPost.UserId, -1);

            Context.Remove(createdPost);
            Context.SaveChanges();
            Assert.IsFalse(Context.Posts.Any(x => x.Id == createPostResult.Value));
        }
    }
}
