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

            StrategyPoint sp = new StrategyPoint("Test", "TEST");
            List<StrategyPoint> spTest = map.GetStrategyPoints();

            Assert.AreEqual(sp.GetName(), spTest.First().GetName()); 
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

        }
}