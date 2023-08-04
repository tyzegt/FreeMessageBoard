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
            Assert.IsTrue(mark.Mark == MarkEnum.Plus);

            ratePostResult = MarksController.RatePost(new RatePostRequest { Id = postId, Plus = false }).Result;
            mark = MarksContext.PostMarks.AsNoTracking().FirstOrDefault(x => x.PostId == postId && x.UserId == -1);
            Assert.IsNotNull(mark);
            Assert.IsTrue(mark.Mark == MarkEnum.Minus);

            ratePostResult = MarksController.RatePost(new RatePostRequest { Id = postId, Plus = false }).Result;
            mark = MarksContext.PostMarks.AsNoTracking().FirstOrDefault(x => x.PostId == postId && x.UserId == -1);
            Assert.IsNull(mark);
        }
    }
}
