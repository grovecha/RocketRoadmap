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
    public class TickMarkTests
    {
        [TestMethod()]
        public void TickMarkTest()
        {
            TickMark tick = new TickMark("Test", 0);

            Assert.AreEqual("Test", tick.GetName());
            Assert.AreEqual(0, tick.GetXPlacement());
        }

        [TestMethod()]
        public void CreateandDeleteTickMarkTest()
        {
            TimeLine tline = new TimeLine("Test");
            TickMark tick = new TickMark("NEWTest", 0);
            List<TickMark> ticklist = tline.GetTicks();

            Assert.AreEqual(1, ticklist.Count());

            Assert.IsTrue(tline.NewTickMark(tick));

            ticklist = tline.GetTicks();
            Assert.AreEqual(2, ticklist.Count());

            Assert.IsTrue(tline.DeleteTickMark(tick));

            ticklist = tline.GetTicks();
            Assert.AreEqual(1, ticklist.Count());
        }

        [TestMethod()]
        public void EditTickMarkTest()
        {
            TimeLine tline = new TimeLine("Test");
            List<TickMark> ticklist = tline.GetTicks();
            TickMark tick =  ticklist.First();

            Assert.AreEqual(0, tick.GetXPlacement());
            tick.EditTickLocation(100, tline.GetName());
            Assert.AreEqual(100, tick.GetXPlacement());

            Assert.AreEqual("Test", tick.GetName());
            tick.EditTickName("NewName", tline.GetName());
            Assert.AreEqual("NewName", tick.GetName());

            TimeLine testline = new TimeLine("Test");
            List<TickMark> testticklist = testline.GetTicks();
            TickMark testtick = testticklist.First();

            Assert.AreEqual(100, testtick.GetXPlacement());
            Assert.AreEqual("NewName", testtick.GetName());

            tick.EditTickName("Test", tline.GetName());
            tick.EditTickLocation(0, tline.GetName());
            Assert.AreEqual(0, tick.GetXPlacement());
            Assert.AreEqual("Test", tick.GetName());
        }

        [TestMethod()]
        public void ClearTickMarkTest()
        {
            TimeLine tline = new TimeLine("Test");
            TickMark tick = new TickMark("Test", 0);

            tline.ClearTicks();

            List<TickMark> test = tline.GetTicks();
            Assert.AreEqual(0, test.Count());

            tline.NewTickMark(tick);
            Assert.AreEqual(1, test.Count());
        }
    }
}