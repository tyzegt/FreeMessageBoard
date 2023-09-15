using FMB.Core.API.Controllers.Posts;
using FMB.Core.Data.Models.Comments;
using FMB.Core.Data.Models.Posts;
using FMB.Services.Marks;
using FMB.Services.Marks.Models;
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

        [TestMethod()]
        public void GetPostTest()
        {
            var title = "title";
            var body = "body";

            var createPostResult = PostsController.CreatePost(new CreatePostRequest { Title = title, Body = body }).Result;
            var comment1 = CommentsController
                .CreateComment(new CreateCommentRequest { Id = createPostResult.Value, Body = "comment1" }).Result;
            var comment2 = CommentsController
                .CreateComment(new CreateCommentRequest { Id = createPostResult.Value, Body = "comment2", ParentCommentId = comment1.Value })
                .Result;
            var comment3 = CommentsController
                .CreateComment(new CreateCommentRequest { Id = createPostResult.Value, Body = "comment3", ParentCommentId = comment1.Value })
                .Result;
            var comment4 = CommentsController
                .CreateComment(new CreateCommentRequest { Id = createPostResult.Value, Body = "comment4", ParentCommentId = comment3.Value })
                .Result;
            MarksContext.Add(new CommentMark { CommentId = comment1.Value, UserId = -1, Mark = MarkEnum.UpVote });
            MarksContext.Add(new CommentMark { CommentId = comment1.Value, UserId = -2, Mark = MarkEnum.UpVote });
            MarksContext.Add(new CommentMark { CommentId = comment1.Value, UserId = -3, Mark = MarkEnum.UpVote });
            MarksContext.Add(new CommentMark { CommentId = comment2.Value, UserId = -1, Mark = MarkEnum.DownVote });
            MarksContext.Add(new CommentMark { CommentId = comment2.Value, UserId = -2, Mark = MarkEnum.DownVote });
            MarksContext.Add(new CommentMark { CommentId = comment2.Value, UserId = -3, Mark = MarkEnum.DownVote });
            MarksContext.Add(new PostMark { PostId = createPostResult.Value, UserId = -1, Mark = MarkEnum.UpVote });
            MarksContext.Add(new PostMark { PostId = createPostResult.Value, UserId = -2, Mark = MarkEnum.UpVote });
            MarksContext.Add(new PostMark { PostId = createPostResult.Value, UserId = -3, Mark = MarkEnum.DownVote });
            MarksContext.SaveChanges();

            var post = PostsController.GetPost(new GetPostRequest { Id = createPostResult.Value }).Result;

            Assert.IsNotNull(post);
            Assert.IsTrue(post.Title == title);
            Assert.IsTrue(post.Body == body);
            Assert.IsTrue(post.UpVotes == 2);
            Assert.IsTrue(post.DownVotes == 1);
            Assert.IsTrue(post.Comments.Count == 1);
            Assert.IsTrue(post.Comments[0].Body == "comment1");
            Assert.IsTrue(post.Comments[0].UpVotes == 3);
            Assert.IsTrue(post.Comments[0].DownVotes == 0);
            Assert.IsTrue(post.Comments[0].ChildComments.ElementAt(0).Body == "comment2");
            Assert.IsTrue(post.Comments[0].ChildComments.ElementAt(1).Body == "comment3");
            Assert.IsTrue(post.Comments[0].ChildComments.ElementAt(1).ChildComments.ElementAt(0).Body == "comment4");
        }
    }
}
