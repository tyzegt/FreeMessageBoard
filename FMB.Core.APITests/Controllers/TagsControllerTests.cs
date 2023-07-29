using FMB.Core.API.Controllers;
using FMB.Core.API.Controllers.Marks;
using FMB.Core.API.Controllers.Posts;
using FMB.Core.Data.Models.Tags;
using FMB.Services.Marks.Models;
using FMB.Services.Marks;
using FMB.Services.Posts;
using FMB.Services.Posts.Models;
using FMB.Services.Tags;
using FMB.Services.Tags.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FMB.Core.APITests.Controllers
{
    [TestClass()]
    public class TagsControllerTests : BaseControllerTestsClass
    {
        public TagsControllerTests() : base()
        {
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