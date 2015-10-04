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
           // TimeLine timeline = new TimeLine();

            Assert.AreEqual("TEST", map.GetDecription());
            Assert.AreEqual("test", map.GetName());
            Assert.AreEqual(Convert.ToDateTime("10/4/2015 12:33:45 PM"), map.GetTimeStamp());
            Assert.AreEqual("123", map.GetUserID());

           // Assert.AreEqual( timeline, map.GetTimeline());
        }

        [TestMethod()]
        public void GetNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTimeStampTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDecriptionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetUserIDTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTimelineTest()
        {
            Assert.Fail();
        }
    }
}