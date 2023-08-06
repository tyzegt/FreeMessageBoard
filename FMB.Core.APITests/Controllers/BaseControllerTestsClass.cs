using FMB.Core.API.Controllers.Posts;
using FMB.Core.API.Controllers;
using FMB.Services.Comments.Models;
using FMB.Services.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMB.Core.API.Controllers.Marks;
using FMB.Services.Marks.Models;
using FMB.Services.Tags;
using FMB.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Configuration;
using FMB.Services.Marks;
using FMB.Services.Comments;
using Microsoft.EntityFrameworkCore;

namespace FMB.Core.APITests.Controllers
{
    public abstract class BaseControllerTestsClass
    {
        // TODO consider standalone or fake context for api tests
        protected PostsContext PostsContext; 
        protected MarksContext MarksContext; 
        protected TagsContext TagsContext;
        protected CommentsContext CommentsContext;

        protected PostsController PostsController;
        protected MarksController MarksController;
        protected CommentsController CommentsController; 
        protected TagsController TagsController;

        public BaseControllerTestsClass()
        {
            var postsOptions = new DbContextOptionsBuilder<PostsContext>();
            postsOptions.UseNpgsql("Server=127.0.0.1;Port=5432;Database=PostsDB;User Id=postgres;Password=qwerty;");
            PostsContext = new PostsContext(postsOptions.Options);
            PostsController = new PostsController(
                new PostsService(PostsContext), 
                Mock.Of<IConfiguration>(), 
                FakeUserManager.GetInstance());

            var marksOptions = new DbContextOptionsBuilder<MarksContext>();
            marksOptions.UseNpgsql("Server=127.0.0.1;Port=5432;Database=MarksDB;User Id=postgres;Password=qwerty;");
            MarksContext = new MarksContext(marksOptions.Options);
            MarksController = new MarksController(
                new PostsService(PostsContext),
                new MarksService(MarksContext),
                Mock.Of<IConfiguration>(),
                FakeUserManager.GetInstance());

            var tagsOptions = new DbContextOptionsBuilder<TagsContext>();
            tagsOptions.UseNpgsql("Server=127.0.0.1;Port=5432;Database=TagsDB;User Id=postgres;Password=qwerty;");
            TagsContext = new TagsContext(tagsOptions.Options); 
            TagsController = new TagsController(
                new TagService(TagsContext), 
                Mock.Of<IConfiguration>(), 
                FakeUserManager.GetInstance());

            var commentsOptions = new DbContextOptionsBuilder<CommentsContext>();
            commentsOptions.UseNpgsql("Server=127.0.0.1;Port=5432;Database=CommentsDB;User Id=postgres;Password=qwerty;");
            CommentsContext = new CommentsContext(commentsOptions.Options);
            CommentsController = new CommentsController(
                new CommentsService(CommentsContext), 
                new PostsService(PostsContext), 
                Mock.Of<IConfiguration>(), 
                FakeUserManager.GetInstance());
        }
    }
}
