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
            Link link = new Link("Tested", "Tested", "www.test.com", "Test");
            Assert.IsNotNull(link);
        }
        [TestMethod()]
        public void SetDescriptionTest()
        {
            Link link = new Link("Tested", "Tested", "www.test.com", "Test");

            Assert.IsTrue(link.SetDescription("Tested"));
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            Link link = new Link("Tested", "Tested", "www.test.com", "Test");

            Assert.IsTrue(link.GetDescription() == "Tested");
        }

        [TestMethod()]
        public void SetProjectNameTest()
        {
            Link link = new Link("Tested", "Tested", "www.test.com", "Test");

            Assert.IsTrue(link.SetProjectName("Tested"));
        }

        [TestMethod()]
        public void GetProjectNameTest()
        {
            Link link = new Link("Tested", "Tested", "www.test.com", "Test");

            Assert.IsTrue(link.GetProjectName() == "Tested");
        }

        [TestMethod()]
        public void SetLinkTest()
        {
            Link link = new Link("Tested", "Tested", "www.test.com", "Test");

            Assert.IsTrue(link.SetLink("www.test.com"));
        }

        [TestMethod()]
        public void GetLinkTest()
        {
            Link link = new Link("Tested", "Tested", "www.test.com", "Test");

            Assert.IsTrue(link.GetLink()=="www.test.com");
        }
    }
}