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
    public class RoadMapTests
    {
        [TestMethod()]
        public void RoadMapTest()
        {
            RoadMap map = new RoadMap("Test");

            Assert.AreEqual("TEST", map.GetDecription());
            Assert.AreEqual("Test", map.GetName());
            Assert.AreEqual("10/4/2015 12:33:45 PM", map.GetTimeStamp().ToString());
            Assert.AreEqual("123", map.GetUserID());

            TimeLine timeline = new TimeLine("Test");
            TimeLine test = map.GetTimeline();

            Assert.AreEqual( timeline.GetID(), test.GetID());

            StrategyPoint sp = new StrategyPoint("test", "TEST");
            List<StrategyPoint> spTest = map.GetStrategyPoints();

            Assert.AreEqual(sp.GetName(), spTest.First().GetName());
        }
    }
}