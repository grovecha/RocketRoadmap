using Microsoft.VisualStudio.TestTools.UnitTesting;
using RocketRoadmap.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketRoadmap.DB.Tests
{
    [TestClass()]
    public class LinkTests
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            Link link = new Link("test", "test2", "www.test.com");
            Assert.IsNotNull(link);
        }
        [TestMethod()]
        public void SetDescriptionTest()
        {
            Link link = new Link("test2", "test2", "www.test.com");

            Assert.IsTrue(link.SetDescription("test2"));
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            Link link = new Link("test2", "test2", "www.test.com");

            Assert.IsTrue(link.GetDescription() == "test2");
        }

        [TestMethod()]
        public void SetProjectNameTest()
        {
            Link link = new Link("test2", "test2", "www.test.com");

            Assert.IsTrue(link.SetProjectName("test2"));
        }

        [TestMethod()]
        public void GetProjectNameTest()
        {
            Link link = new Link("test2", "test2", "www.test.com");

            Assert.IsTrue(link.GetProjectName()=="test2");
        }

        [TestMethod()]
        public void SetLinkTest()
        {
            Link link = new Link("test2", "test2", "www.test.com");

            Assert.IsTrue(link.SetLink("www.test.org"));
        }

        [TestMethod()]
        public void GetLinkTest()
        {
            Link link = new Link("test2", "test2", "www.test.com");

            Assert.IsTrue(link.GetLink()=="www.test.org");
        }
    }
}