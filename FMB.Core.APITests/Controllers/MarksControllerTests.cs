using FMB.Core.API.Controllers.Marks;
using FMB.Core.API.Controllers.Posts;
using FMB.Core.Data.Models.Comments;
using FMB.Core.Data.Models.Posts;
using FMB.Services.Marks;
using FMB.Services.Marks.Models;
using FMB.Services.Posts;
using FMB.Services.Posts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Core.APITests.Controllers
{
    [TestClass]
    public class MarksControllerTests : BaseControllerTestsClass
    {
        public MarksControllerTests() : base()
        {
        }

        [TestMethod]
        public void RatePostTest()
        {
            var postId = PostsController.CreatePost(new CreatePostRequest { Title = "test", Body = "test" }).Result.Value;
            var ratePostResult = MarksController.RatePost(new RateRequest { Id = postId, Upvote = true }).Result;
            var mark = MarksContext.PostMarks.AsNoTracking().FirstOrDefault(x => x.PostId == postId && x.UserId == -1);
            Assert.IsNotNull(mark);
            Assert.IsTrue(mark.Mark == MarkEnum.UpVote);

            ratePostResult = MarksController.RatePost(new RateRequest { Id = postId, Upvote = false }).Result;
            mark = MarksContext.PostMarks.AsNoTracking().FirstOrDefault(x => x.PostId == postId && x.UserId == -1);
            Assert.IsNotNull(mark);
            Assert.IsTrue(mark.Mark == MarkEnum.DownVote);

            ratePostResult = MarksController.RatePost(new RateRequest { Id = postId, Upvote = false }).Result;
            mark = MarksContext.PostMarks.AsNoTracking().FirstOrDefault(x => x.PostId == postId && x.UserId == -1);
            Assert.IsNull(mark);
        }

        [TestMethod]
        public void GetPostRatingTest()
        {
            var postId = PostsController.CreatePost(new CreatePostRequest { Title = "test", Body = "test" }).Result.Value;
            MarksContext.AddRange(
                new PostMark { Mark = MarkEnum.UpVote, UserId = -1, PostId = postId },
                new PostMark { Mark = MarkEnum.UpVote, UserId = -2, PostId = postId },
                new PostMark { Mark = MarkEnum.UpVote, UserId = -3, PostId = postId });
            MarksContext.SaveChanges();
            var rating = MarksController.GetPostRating(postId).Result.Value;
            Assert.IsTrue(rating.UpVotes == 3 && rating.DownVotes == 0 && rating.Rating == 3);

            MarksContext.AddRange(
                new PostMark { Mark = MarkEnum.DownVote, UserId = -4, PostId = postId },
                new PostMark { Mark = MarkEnum.DownVote, UserId = -5, PostId = postId });
            MarksContext.SaveChanges();
            rating = MarksController.GetPostRating(postId).Result.Value;
            Assert.IsTrue(rating.UpVotes == 3 && rating.DownVotes == 2 && rating.Rating == 1);

            MarksContext.AddRange(
                new PostMark { Mark = MarkEnum.DownVote, UserId = -6, PostId = postId },
                new PostMark { Mark = MarkEnum.DownVote, UserId = -7, PostId = postId },
                new PostMark { Mark = MarkEnum.DownVote, UserId = -8, PostId = postId },
                new PostMark { Mark = MarkEnum.DownVote, UserId = -9, PostId = postId });
            MarksContext.SaveChanges();
            rating = MarksController.GetPostRating(postId).Result.Value;
            Assert.IsTrue(rating.UpVotes == 3 && rating.DownVotes == 6 && rating.Rating == -3);
        }

        [TestMethod]
        public void RateCommentTest()
        {
            var postId = PostsController.CreatePost(new CreatePostRequest { Title = "test", Body = "test" }).Result.Value;
            var commentId = CommentsController.CreateComment(new CreateCommentRequest { Id = postId, Body = "test" }).Result.Value;
            var rateCommentResult = MarksController.RateComment(new RateRequest { Id = commentId, Upvote = true }).Result;
            var mark = MarksContext.CommentMarks.AsNoTracking().FirstOrDefault(x => x.CommentId == commentId && x.UserId == -1);
            Assert.IsNotNull(mark);
            Assert.IsTrue(mark.CommentId == commentId && mark.Mark == MarkEnum.UpVote);

            rateCommentResult = MarksController.RateComment(new RateRequest { Id = commentId, Upvote = false }).Result;
            mark = MarksContext.CommentMarks.AsNoTracking().FirstOrDefault(x => x.CommentId == commentId && x.UserId == -1);
            Assert.IsNotNull(mark);
            Assert.IsTrue(mark.CommentId == commentId && mark.Mark == MarkEnum.DownVote);

            rateCommentResult = MarksController.RateComment(new RateRequest { Id = commentId, Upvote = false }).Result;
            mark = MarksContext.CommentMarks.AsNoTracking().FirstOrDefault(x => x.CommentId == commentId && x.UserId == -1);
            Assert.IsNull(mark);
        }

        [TestMethod]
        public void GetCommentRatingTest()
        {
            var postId = PostsController.CreatePost(new CreatePostRequest { Title = "test", Body = "test" }).Result.Value;
            var commentId = CommentsController.CreateComment(new CreateCommentRequest { Id = postId, Body = "test" }).Result.Value;
            MarksContext.AddRange(
                new CommentMark { Mark = MarkEnum.UpVote, UserId = -1, CommentId = commentId },
                new CommentMark { Mark = MarkEnum.UpVote, UserId = -2, CommentId = commentId },
                new CommentMark { Mark = MarkEnum.UpVote, UserId = -3, CommentId = commentId });
            MarksContext.SaveChanges();
            var rating = MarksController.GetCommentRating(commentId).Result.Value;
            Assert.IsTrue(rating.UpVotes == 3 && rating.DownVotes == 0 && rating.Rating == 3);

            MarksContext.AddRange(
                new CommentMark { Mark = MarkEnum.DownVote, UserId = -4, CommentId = commentId },
                new CommentMark { Mark = MarkEnum.DownVote, UserId = -5, CommentId = commentId });
            MarksContext.SaveChanges();
            rating = MarksController.GetCommentRating(commentId).Result.Value;
            Assert.IsTrue(rating.UpVotes == 3 && rating.DownVotes == 2 && rating.Rating == 1);

            MarksContext.AddRange(
                new CommentMark { Mark = MarkEnum.DownVote, UserId = -6, CommentId = commentId },
                new CommentMark { Mark = MarkEnum.DownVote, UserId = -7, CommentId = commentId },
                new CommentMark { Mark = MarkEnum.DownVote, UserId = -8, CommentId = commentId },
                new CommentMark { Mark = MarkEnum.DownVote, UserId = -9, CommentId = commentId });
            MarksContext.SaveChanges();
            rating = MarksController.GetCommentRating(commentId).Result.Value;
            Assert.IsTrue(rating.UpVotes == 3 && rating.DownVotes == 6 && rating.Rating == -3);
        }

        [TestMethod]
        public void GetCommentsRatingTest()
        {
            var postId = PostsController.CreatePost(new CreatePostRequest { Title = "test", Body = "test" }).Result.Value;
            var commentId = CommentsController.CreateComment(new CreateCommentRequest { Id = postId, Body = "test" }).Result.Value;
            MarksContext.AddRange(
                new CommentMark { Mark = MarkEnum.UpVote, UserId = -1, CommentId = commentId },
                new CommentMark { Mark = MarkEnum.UpVote, UserId = -2, CommentId = commentId },
                new CommentMark { Mark = MarkEnum.UpVote, UserId = -3, CommentId = commentId });

            var commentId2 = CommentsController.CreateComment(new CreateCommentRequest { Id = postId, Body = "test" }).Result.Value;
            MarksContext.AddRange(
                new CommentMark { Mark = MarkEnum.DownVote, UserId = -1, CommentId = commentId2 },
                new CommentMark { Mark = MarkEnum.DownVote, UserId = -2, CommentId = commentId2 },
                new CommentMark { Mark = MarkEnum.DownVote, UserId = -3, CommentId = commentId2 });
            MarksContext.SaveChanges();

            var getCommentsRatingResponse = MarksController.GetCommentsRating(new List<long> { commentId, commentId2 }).Result;
            Assert.IsTrue(getCommentsRatingResponse.Value.FirstOrDefault(x => x.EntityId == commentId).Rating == 3);
            Assert.IsTrue(getCommentsRatingResponse.Value.FirstOrDefault(x => x.EntityId == commentId2).Rating == -3);
        }
    }
}
