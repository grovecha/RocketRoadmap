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
    public class ProjectTests
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            Project proj = new Project("Tested", "test", "test", "Test");
            
            Assert.IsNotNull(proj);
        }
        [TestMethod()]
        public void SetNameTest()
        {
            Project proj = new Project("Tested", "test", "test", "Test");
            Assert.Inconclusive("Will throw primary key error if ran");
        }

        [TestMethod()]
        public void GetNameTest()
        {
            Project proj = new Project("Tested", "test", "test", "Test");
            Assert.IsTrue(proj.GetName()=="Tested");
        }

        [TestMethod()]
        public void SetDescriptionTest()
        {
            Project proj = new Project("Tested", "test", "test", "Test");
            
            Assert.IsTrue(proj.SetDescription("test"));
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            Project proj = new Project("Tested", "test", "test", "Test");
            Assert.IsTrue(proj.GetDescription()=="test");
        }

        [TestMethod()]
        public void SetStartDateTest()
        {
            Project proj = new Project("Tested", "test", "test", "Test");
            DateTime startdate = new DateTime(2012, 10, 5);
            Assert.IsTrue(proj.SetStartDate(startdate));
        }

        [TestMethod()]
        public void GetStartDateTest()
        {
            Project proj = new Project("Tested", "test", "test", "Test");
            DateTime startdate = new DateTime(2012, 10, 5);
            proj.SetStartDate(startdate);

            Assert.IsTrue(proj.GetStartDate()==startdate);
        }

        [TestMethod()]
        public void SetEndDateTest()
        {
            Project proj = new Project("Tested", "test", "test", "Test");
            DateTime enddate = new DateTime(2012, 10, 5);

            Assert.IsTrue(proj.SetEndDate(enddate));
        }

        [TestMethod()]
        public void GetEndDateTest()
        {
            Project proj = new Project("Tested", "test", "test", "Test");
            DateTime enddate = new DateTime(2012, 10, 5);
            proj.SetEndDate(enddate);
            Assert.IsTrue(proj.GetEndDate() == enddate);
        }

        [TestMethod()]
        public void SetBusinessValueTest()
        {
            Project proj = new Project("Tested", "test", "test", "Test");

            
            Assert.IsTrue(proj.SetBusinessValue("test"));
        }

        [TestMethod()]
        public void GetBusinessValueTest()
        {
            Project proj = new Project("Tested", "test", "test", "Test");

            Assert.IsTrue(proj.GetBusinessValue()=="test");
        }
        [TestMethod()]
        public void AddProjTest()
        {
            BusinessValue test = new BusinessValue("test", "Test");
            Project proj = new Project("Tested25", "test", "test", "Test");
            test.AddProject(proj);

        }
    }
}