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
    public class TimeLineTests
    {
        [TestMethod()]
        public void TimeLineTest()
        {
            TimeLine timeline = new TimeLine("Test");
            TickMark tick = new TickMark("Test", 0);

            Assert.AreEqual("Test", timeline.GetName());
            Assert.AreEqual(Convert.ToDateTime("1/1/1900 12:00:00 AM"), timeline.GetStartDate());
            Assert.AreEqual(Convert.ToDateTime("1/1/1900 12:00:00 AM"), timeline.GetEndDate());

            Assert.AreEqual(1, timeline.GetTicks().Count());

            TickMark first = timeline.GetTicks().First();
            Assert.AreEqual(tick.GetName(), first.GetName());
            Assert.AreEqual(tick.GetXPlacement(), first.GetXPlacement());
        }

        [TestMethod()]
        public void CreateandDeleteTimeLineTest()
        {
            RoadMap map = new RoadMap("Test");
            Assert.IsTrue(map.CreateTimeLine("NEW"));
            //Assert.IsTrue(map.DeleteTimeLine());
        }

        [TestMethod()]
        public void EditTimeLineTest()
        {
            TimeLine timeline = new TimeLine("Test");

            DateTime test = DateTime.Now;
            
            timeline.EditStartDate(test, "test");
            Assert.AreEqual(test.ToString(), timeline.GetStartDate().ToString());
            timeline.EditEndDate(test, "test");
            Assert.AreEqual(test.ToString(), timeline.GetEndDate().ToString());

            TimeLine testtimeline = new TimeLine("test");

            Assert.AreEqual(test.ToString(), testtimeline.GetStartDate().ToString());
            Assert.AreEqual(test.ToString(), testtimeline.GetEndDate().ToString());

            timeline.EditStartDate(Convert.ToDateTime("1/1/1900 12:00:00 AM"), "test");
            Assert.AreEqual("1/1/1900 12:00:00 AM", timeline.GetStartDate().ToString());
            timeline.EditEndDate(Convert.ToDateTime("1/1/1900 12:00:00 AM"), "test");
            Assert.AreEqual("1/1/1900 12:00:00 AM", timeline.GetEndDate().ToString()); 
        }

    }
}