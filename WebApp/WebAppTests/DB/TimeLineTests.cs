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
    public class TimeLineTests
    {
        [TestMethod()]
        public void TimeLineTest()
        {
            TimeLine timeline = new TimeLine("test");

            TickMark tick = new TickMark("Test", 0);

            Assert.AreEqual(1, timeline.GetID());
            Assert.AreEqual(Convert.ToDateTime("1/1/1900 12:00:00 AM"), timeline.GetStartDate());
            Assert.AreEqual(Convert.ToDateTime("1/1/1900 12:00:00 AM"), timeline.GetEndDate());

            Assert.AreEqual(1, timeline.GetTicks().Count());

            TickMark first = timeline.GetTicks().First();
            Assert.AreEqual(tick.GetName(), first.GetName());
            Assert.AreEqual(tick.GetXPlacement(), first.GetXPlacement());
        }
    }
}