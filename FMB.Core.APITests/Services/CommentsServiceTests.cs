using FMB.Services.Comments;
using FMB.Services.Comments.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Core.APITests.Services
{
    [TestClass()]
    public class CommentsServiceTests
    {
        private readonly CommentsService _commentsService;
        private readonly CommentsContext _context;
        public CommentsServiceTests() {
            var commentsOptions = new DbContextOptionsBuilder<CommentsContext>();
            commentsOptions.UseNpgsql("Server=127.0.0.1;Port=5432;Database=CommentsDB;User Id=postgres;Password=qwerty;");
            _context = new CommentsContext(commentsOptions.Options);
            _commentsService = new CommentsService(_context);
        }

        [TestMethod()]
        public void TestCommentsExists()
        {
            var comment1 = new Comment { Body = "test", PostId = 1, CreatedAt = DateTime.UtcNow, UserId = 1 };
            var comment2 = new Comment { Body = "test", PostId = 1, CreatedAt = DateTime.UtcNow, UserId = 1 };
            var comment3 = new Comment { Body = "test", PostId = 1, CreatedAt = DateTime.UtcNow, UserId = 1 };
            _context.Comments.Add(comment1);
            _context.Comments.Add(comment2);
            _context.Comments.Add(comment3);
            _context.SaveChanges();
            Assert.IsTrue(_commentsService.IsCommentsExists(comment1.Id, comment2.Id, comment3.Id).Result);
        }
    }
}
