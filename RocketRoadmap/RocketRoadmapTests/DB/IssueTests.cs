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
    public class IssueTests
    {
        [TestMethod()]
        public void IssueConstructorTest()
        {
            Issue issue = new Issue("test","test2", "Test");
           
            Assert.IsNotNull(issue);
        }
        [TestMethod()]
        public void SetDescriptionTest()
        {
            Issue issue = new Issue("Tested", "Tested", "Test");


            Assert.IsTrue(issue.SetDescription("Tested"));
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            Issue issue = new Issue("Tested", "Tested", "Test");

            Assert.IsTrue(issue.GetDescription()=="Tested");
        }

        [TestMethod()]
        public void SetProjectNameTest()
        {
            Issue issue = new Issue("Tested", "Tested", "Test");

            Assert.IsTrue(issue.SetProjectName("Tested"));
        }

        [TestMethod()]
        public void GetProjectNameTest()
        {
            Issue issue = new Issue("Tested", "Tested", "Test");

            Assert.IsTrue(issue.GetProjectName()=="Tested");
        }
    }
}