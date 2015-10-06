using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.DB.Tests
{
    [TestClass()]
    public class IssueTests
    {
        [TestMethod()]
        public void IssueConstructorTest()
        {
            Issue issue = new Issue("test","test2");
           
            Assert.IsNotNull(issue);
        }
        [TestMethod()]
        public void SetDescriptionTest()
        {
            Issue issue = new Issue("test", "test2");


            Assert.IsTrue(issue.SetDescription("test"));
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            Issue issue = new Issue("test", "test2");

            Assert.IsTrue(issue.GetDescription()=="test");
        }

        [TestMethod()]
        public void SetProjectNameTest()
        {
            Issue issue = new Issue("test", "test2");

            Assert.IsTrue(issue.SetProjectName("test2"));
        }

        [TestMethod()]
        public void GetProjectNameTest()
        {
            Issue issue = new Issue("test", "test2");

            Assert.IsTrue(issue.GetProjectName()=="test2");
        }
    }
}