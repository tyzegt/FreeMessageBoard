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
            PostsContext = new PostsContext();
            PostsController = new PostsController(
                new PostsService(PostsContext), 
                Mock.Of<IConfiguration>(), 
                FakeUserManager.GetInstance());

            MarksContext = new MarksContext();
            MarksController = new MarksController(
                new PostsService(PostsContext),
                new MarksService(MarksContext),
                Mock.Of<IConfiguration>(),
                FakeUserManager.GetInstance());

            TagsContext = new TagsContext(); 
            TagsController = new TagsController(
                new TagService(TagsContext), 
                Mock.Of<IConfiguration>(), 
                FakeUserManager.GetInstance());

            CommentsContext = new CommentsContext();
            CommentsController = new CommentsController(
                new CommentsService(CommentsContext), 
                new PostsService(PostsContext), 
                Mock.Of<IConfiguration>(), 
                FakeUserManager.GetInstance());
        }
    }
}
