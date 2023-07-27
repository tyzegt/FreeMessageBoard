using Microsoft.VisualStudio.TestTools.UnitTesting;
using FMB.Core.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using FMB.Services.Tags;

namespace FMB.Core.API.Controllers.Tests
{
    [TestClass()]
    public class TagsControllerTests
    {
        [TestMethod()]
        public void TagsFullTest()
        {
            var context = new TagsContext(); // TODO consider standalone or fake context for api tests
            var controller = new TagsController(new TagService(context));
            var newTagName = Guid.NewGuid().ToString("N");

            var createTagResult = controller.CreateTag(new Models.CreateTagRequest { Name = newTagName }).Value;
            Assert.IsTrue(createTagResult > 0);

            var getTagResult = controller.GetTag(createTagResult).Value;
            Assert.IsTrue(getTagResult.Name == newTagName);

            var renamedTagTame = Guid.NewGuid().ToString("N");
            var updateTagResult = controller.UpdateTag(new Models.UpdateTagRequest { Id = getTagResult.Id, NewName = renamedTagTame });
            var tag = context.Tags.FirstOrDefault(x => x.Id == getTagResult.Id);
            Assert.IsNotNull(tag);
            Assert.AreEqual(tag.Name, renamedTagTame);

            var deleteTagResult = controller.DeleteTag(tag.Id);
            tag = context.Tags.FirstOrDefault(x => x.Id == getTagResult.Id);
            Assert.IsNull(tag);
        }
    }
}