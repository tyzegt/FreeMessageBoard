using FMB.Core.API.Controllers.Marks;
using FMB.Core.API.Controllers.Posts;
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
            var ratePostResult = MarksController.RatePost(new RatePostRequest { Id = postId, Plus = true }).Result;
            var mark = MarksContext.PostMarks.AsNoTracking().FirstOrDefault(x => x.PostId == postId && x.UserId == -1);
            Assert.IsNotNull(mark);
            Assert.IsTrue(mark.Mark == MarkEnum.Upvote);

            ratePostResult = MarksController.RatePost(new RatePostRequest { Id = postId, Plus = false }).Result;
            mark = MarksContext.PostMarks.AsNoTracking().FirstOrDefault(x => x.PostId == postId && x.UserId == -1);
            Assert.IsNotNull(mark);
            Assert.IsTrue(mark.Mark == MarkEnum.Downvote);

            ratePostResult = MarksController.RatePost(new RatePostRequest { Id = postId, Plus = false }).Result;
            mark = MarksContext.PostMarks.AsNoTracking().FirstOrDefault(x => x.PostId == postId && x.UserId == -1);
            Assert.IsNull(mark);
        }

        [TestMethod]
        public void GetPostRatingTest()
        {
            var postId = PostsController.CreatePost(new CreatePostRequest { Title = "test", Body = "test" }).Result.Value;
            MarksContext.AddRange(
                new PostMark { Mark = MarkEnum.Upvote, UserId = -1, PostId = postId },
                new PostMark { Mark = MarkEnum.Upvote, UserId = -2, PostId = postId },
                new PostMark { Mark = MarkEnum.Upvote, UserId = -3, PostId = postId });
            MarksContext.SaveChanges();
            var rating = MarksController.GetPostRating(postId).Result.Value;
            Assert.IsTrue(rating.Upvotes == 3 && rating.Downvotes == 0 && rating.Rating == 3);

            MarksContext.AddRange(
                new PostMark { Mark = MarkEnum.Downvote, UserId = -4, PostId = postId },
                new PostMark { Mark = MarkEnum.Downvote, UserId = -5, PostId = postId });
            MarksContext.SaveChanges();
            rating = MarksController.GetPostRating(postId).Result.Value;
            Assert.IsTrue(rating.Upvotes == 3 && rating.Downvotes == 2 && rating.Rating == 1);

            MarksContext.AddRange(
                new PostMark { Mark = MarkEnum.Downvote, UserId = -6, PostId = postId },
                new PostMark { Mark = MarkEnum.Downvote, UserId = -7, PostId = postId },
                new PostMark { Mark = MarkEnum.Downvote, UserId = -8, PostId = postId },
                new PostMark { Mark = MarkEnum.Downvote, UserId = -9, PostId = postId });
            MarksContext.SaveChanges();
            rating = MarksController.GetPostRating(postId).Result.Value;
            Assert.IsTrue(rating.Upvotes == 3 && rating.Downvotes == 6 && rating.Rating == -3);
        }
    }
}
