using Microsoft.VisualStudio.TestTools.UnitTesting;
using FMB.Core.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using FMB.Services.Tags;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using FMB.Core.API.Data;
using FMB.Core.APITests;

namespace FMB.Core.API.Controllers.Tests
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
            var createTagResult = Controller.CreateTag(new Models.CreateTagRequest { Name = newTagName }).Value;
            Assert.IsTrue(createTagResult > 0);

            var getTagResult = Controller.GetTag(createTagResult).Value;
            Assert.IsTrue(getTagResult.Name == newTagName);

            var renamedTagTame = Guid.NewGuid().ToString("N");
            var updateTagResult = Controller.UpdateTag(new Models.UpdateTagRequest { Id = getTagResult.Id, NewName = renamedTagTame });
            var tag = Context.Tags.FirstOrDefault(x => x.Id == getTagResult.Id);
            Assert.IsNotNull(tag);
            Assert.AreEqual(tag.Name, renamedTagTame);

            var deleteTagResult = Controller.DeleteTag(tag.Id);
            tag = Context.Tags.FirstOrDefault(x => x.Id == getTagResult.Id);
            Assert.IsNull(tag);
        }
    }
}