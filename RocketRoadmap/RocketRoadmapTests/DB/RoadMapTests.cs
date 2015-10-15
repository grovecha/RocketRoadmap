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
    public class RoadMapTests
    {
        [TestMethod()]
        public void RoadMapTest()
        {
            RoadMap map = new RoadMap("Test");

            Assert.AreEqual("TEST", map.GetDecription());
            Assert.AreEqual("Test", map.GetName());
            Assert.AreEqual("10/6/2015 1:53:46 PM", map.GetTimeStamp().ToString());
            Assert.AreEqual("123", map.GetUser().GetUserName());

            TimeLine timeline = new TimeLine("Test");
            TimeLine test = map.GetTimeline();

            Assert.AreEqual( timeline.GetName(), test.GetName());

            StrategyPoint sp = new StrategyPoint("Test", "TEST", "Test");
            List<StrategyPoint> spTest = map.GetStrategyPoints();

            Assert.AreEqual(sp.GetName(), spTest.First().GetName());

            BusinessValue bv = new BusinessValue("test", "Test");
            List<BusinessValue> bvList = sp.GetBusinessValues();

            Assert.AreEqual(bv.GetName(), bvList.First().GetName());

            Project proj = new Project("Tested", "test", "test", "Test");
            List<Project> projList = bvList.First().GetProjects();

            Assert.AreEqual(proj.GetName(), projList.First().GetName());

            Link link = new Link("tested", "Tested", "www.test.com", "Test");
            List<Link> linkList = projList.First().GetLinks();

            Assert.AreEqual(link.GetLink(), linkList.First().GetLink());

            Issue issue = new Issue("Tested", "Tested", "Test");
            List<Issue> issueList = projList.First().GetIssues();

            Assert.AreEqual(issue.GetDescription(), issueList.First().GetDescription());

            Project dep = new Project("Tested2", "TEST", "test", "Test");
            proj.CreateDependant(dep);
            List<Project> depList = projList.First().GetDependencies();

            Assert.AreEqual(dep.GetName(), depList.First().GetName());
        }

        [TestMethod()]
        public void EditRoadMapTest()
        {
            RoadMap map = new RoadMap("Test");

            Assert.IsTrue(map.EditName("NEWNAME"));
            Assert.AreEqual("NEWNAME", map.GetName());

            Assert.IsTrue(map.EditDescription("CHANGED"));
            Assert.AreEqual("CHANGED", map.GetDecription());

            RoadMap testmap = new RoadMap("NEWNAME");
            Assert.AreEqual("NEWNAME", map.GetName());
            Assert.AreEqual("CHANGED", map.GetDecription());

            Assert.IsTrue(map.EditName("Test"));
            Assert.AreEqual("Test", map.GetName());

            Assert.IsTrue(map.EditDescription("TEST"));
            Assert.AreEqual("TEST", map.GetDecription());   
        }

        [TestMethod()]
        public void CHASERUNTHIS()
        {
            string name = "ROADMAPNAMEHERE"; //CHASE PUT THE ROADMAP NAME IN HERE
            RocketRoadmap.DB.Database db = new Database();

            db.connect();
            bool flag=db.executewrite("DELETE FROM [dbo].[Roadmap] WHERE Name='" + name + ";");
            db.close();
            
        }

        }
}