using FMB.Core.API.Controllers;
using FMB.Core.API.Controllers.Posts;
using FMB.Core.Data.Models.Tags;
using FMB.Services.Tags;
using FMB.Services.Tags.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FMB.Core.Data.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace FMB.Core.APITests.Controllers
{
    [TestClass()]
    public class TagsControllerTests : BaseControllerTestsClass
    {
        public TagsControllerTests() : base()
        {
        }

        [TestMethod]
        public async Task AssignGetAndDeleteTagsTests()
        {
            var postId = PostsController.CreatePost(new CreatePostRequest { Body = "test", Title = "test" }).Result.Value;

            var tag1 = ((OkObjectResult)TagsController.CreateTag(new CreateTagRequest { Name = Guid.NewGuid().ToString("N") })
                .Result).Value as Tag;
            await TagsController.AssignPostTag(tag1.Id, postId);
            Assert.IsTrue(TagsContext.PostTags.AsNoTracking().Any(x => x.PostId == postId && x.TagId == tag1.Id));

            var tag2 = ((OkObjectResult)TagsController.CreateTag(new CreateTagRequest { Name = Guid.NewGuid().ToString("N") })
                .Result).Value as Tag;
            await TagsController.AssignPostTag(tag2.Id, postId);
            Assert.IsTrue(TagsContext.PostTags.AsNoTracking().Any(x => x.PostId == postId && x.TagId == tag2.Id));

            var postTags = await TagsController.GetPostTags(postId);
            Assert.IsTrue(postTags.Any(x => x.Name == tag1.Name && x.Id == tag1.Id));
            Assert.IsTrue(postTags.Any(x => x.Name == tag2.Name && x.Id == tag2.Id));

            await TagsController.DeleteTag(tag1.Id);
            await TagsController.DeleteTag(tag2.Id);
            postTags = await TagsController.GetPostTags(postId);
            Assert.IsFalse(postTags.Any());
        }

        [TestMethod]
        public async Task TagsFullTest()
        {
            var newTagName = Guid.NewGuid().ToString("N");
            var createTagResult = await TagsController.CreateTag(new CreateTagRequest { Name = newTagName });
            
            Assert.IsNotNull(createTagResult);
            Assert.IsInstanceOfType(createTagResult, typeof(OkObjectResult));
            var tag = (createTagResult as OkObjectResult).Value as Tag;
            Assert.IsNotNull(tag);
            Assert.IsTrue(tag.Id > 0);

            var tagId = tag.Id;
            
            var getTagResult = await TagsController.GetTag(tag.Id);
            Assert.IsNotNull(getTagResult);
            Assert.IsInstanceOfType(getTagResult, typeof(OkObjectResult));
            tag = (getTagResult as OkObjectResult).Value as Tag;
            Assert.IsNotNull(tag);
            Assert.IsTrue(tag.Id == tagId);
            Assert.IsTrue(tag.Name == newTagName);

            var renamedTagTame = Guid.NewGuid().ToString("N");
            var updateTagResult = await TagsController.UpdateTag(new UpdateTagRequest { Id = tagId, NewName = renamedTagTame });
            Assert.IsNotNull(updateTagResult);
            Assert.IsInstanceOfType(updateTagResult, typeof(OkResult));
            TagsContext.Entry(tag).Reload();
            tag = TagsContext.Tags.FirstOrDefault(x => x.Id == tagId);
            Assert.IsNotNull(tag);
            Assert.AreEqual(renamedTagTame, tag.Name);
            
            var deleteTagResult = await TagsController.DeleteTag(tagId);
            Assert.IsNotNull(deleteTagResult);
            Assert.IsInstanceOfType(deleteTagResult, typeof(OkResult));
            tag = TagsContext.Tags.FirstOrDefault(x => x.Id == tagId);
            Assert.IsNull(tag);
        }
    }
}