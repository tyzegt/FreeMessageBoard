using FMB.Core.API.Controllers.Tags;
using FMB.Core.Data.Models.Tags;
using FMB.Services.Tags;
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

        [TestMethod()]
        public void TagsFullTest()
        {

            var newTagName = Guid.NewGuid().ToString("N");
            var createTagResult = Controller.CreateTag(new CreateTagRequest { Name = newTagName }).Value;
            Assert.IsTrue(createTagResult > 0);

            var getTagResult = Controller.GetTag(createTagResult).Value;
            Assert.IsTrue(getTagResult.Name == newTagName);

            var renamedTagTame = Guid.NewGuid().ToString("N");
            var updateTagResult = Controller.UpdateTag(new UpdateTagRequest { Id = getTagResult.Id, NewName = renamedTagTame });
            var tag = Context.Tags.FirstOrDefault(x => x.Id == getTagResult.Id);
            Assert.IsNotNull(tag);
            Assert.AreEqual(tag.Name, renamedTagTame);

            var deleteTagResult = Controller.DeleteTag(tag.Id);
            tag = Context.Tags.FirstOrDefault(x => x.Id == getTagResult.Id);
            Assert.IsNull(tag);
        }
    }
}