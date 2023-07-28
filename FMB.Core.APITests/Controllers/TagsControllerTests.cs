using FMB.Core.API.Controllers;
using FMB.Core.Data.Models.Tags;
using FMB.Services.Tags;
using FMB.Services.Tags.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FMB.Core.APITests.Controllers
{
    [TestClass()]
    public class TagsControllerTests
    {
        TagsContext Context;
        TagsController Controller;

        public TagsControllerTests()
        {
            Context = new TagsContext(); // TODO consider standalone or fake context for api tests
            Controller = new TagsController(new TagService(Context), Mock.Of<IConfiguration>(), FakeUserManager.GetInstance());
        }

        [TestMethod]
        public async Task TagsFullTest()
        {
            var newTagName = Guid.NewGuid().ToString("N");
            var createTagResult = await Controller.CreateTag(new CreateTagRequest { Name = newTagName });
            
            Assert.IsNotNull(createTagResult);
            Assert.IsInstanceOfType(createTagResult, typeof(OkObjectResult));
            var tag = (createTagResult as OkObjectResult).Value as Tag;
            Assert.IsNotNull(tag);
            Assert.IsTrue(tag.Id > 0);

            var tagId = tag.Id;
            
            var getTagResult = await Controller.GetTag(tag.Id);
            Assert.IsNotNull(getTagResult);
            Assert.IsInstanceOfType(getTagResult, typeof(OkObjectResult));
            tag = (getTagResult as OkObjectResult).Value as Tag;
            Assert.IsNotNull(tag);
            Assert.IsTrue(tag.Id == tagId);
            Assert.IsTrue(tag.Name == newTagName);

            var renamedTagTame = Guid.NewGuid().ToString("N");
            var updateTagResult = await Controller.UpdateTag(new UpdateTagRequest { Id = tagId, NewName = renamedTagTame });
            Assert.IsNotNull(updateTagResult);
            Assert.IsInstanceOfType(updateTagResult, typeof(OkResult));
            Context.Entry(tag).Reload();
            tag = Context.Tags.FirstOrDefault(x => x.Id == tagId);
            Assert.IsNotNull(tag);
            Assert.AreEqual(renamedTagTame, tag.Name);
            
            var deleteTagResult = await Controller.DeleteTag(tagId);
            Assert.IsNotNull(deleteTagResult);
            Assert.IsInstanceOfType(deleteTagResult, typeof(OkResult));
            tag = Context.Tags.FirstOrDefault(x => x.Id == tagId);
            Assert.IsNull(tag);
        }
    }
}