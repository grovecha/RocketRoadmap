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
    public class ProjectTests
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            Project proj = new Project("test", "test proj", "test2");
            
            Assert.IsNotNull(proj);
        }
        [TestMethod()]
        public void SetNameTest()
        {
            Project proj = new Project("test", "test proj", "test2");
            Assert.Inconclusive("Will throw primary key error if ran");
        }

        [TestMethod()]
        public void GetNameTest()
        {
            Project proj = new Project("test2", "test proj", "test2");
            Assert.IsTrue(proj.GetName()=="test2");
        }

        [TestMethod()]
        public void SetDescriptionTest()
        {
            Project proj = new Project("test2", "test proj", "test2");
            
            Assert.IsTrue(proj.SetDescription("weee"));
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            Project proj = new Project("test3", "test proj3", "test2");
            proj.InsertDB();
            Assert.IsTrue(proj.GetDescription()=="test proj3");
        }

        [TestMethod()]
        public void SetStartDateTest()
        {
            Project proj = new Project("test3", "test proj3", "test2");
            DateTime startdate = new DateTime(2012, 10, 5);
            Assert.IsTrue(proj.SetStartDate(startdate));
        }

        [TestMethod()]
        public void GetStartDateTest()
        {
            Project proj = new Project("test3", "test proj3", "test2");
            DateTime startdate = new DateTime(2012, 10, 5);

            Assert.IsTrue(proj.GetStartDate()==startdate);
        }

        [TestMethod()]
        public void SetEndDateTest()
        {
            Project proj = new Project("test3", "test proj3", "test2");
            DateTime enddate = new DateTime(2012, 10, 5);

            Assert.IsTrue(proj.SetEndDate(enddate));
        }

        [TestMethod()]
        public void GetEndDateTest()
        {
            Project proj = new Project("test3", "test proj3", "test2");
            DateTime enddate = new DateTime(2012, 10, 5);
            Assert.IsTrue(proj.GetEndDate() == enddate);
        }

        [TestMethod()]
        public void SetBusinessValueTest()
        {
            Project proj = new Project("test3", "test proj3", "test2");

            
            Assert.IsTrue(proj.SetBusinessValue("test2"));
        }

        [TestMethod()]
        public void GetBusinessValueTest()
        {
            Project proj = new Project("test3", "test proj3", "test2");

            Assert.IsTrue(proj.GetBusinessValue()=="test2");
        }
    }
}