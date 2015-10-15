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
    public class StrategyPointTests
    {
        [TestMethod()]
        public void StrategyPointTest()
        {
            StrategyPoint sp = new StrategyPoint("Test", "TEST", "Test");

            Assert.AreEqual("Test", sp.GetName());
            Assert.AreEqual("TEST", sp.GetDescription());

            List<BusinessValue> values = sp.GetBusinessValues();
            Assert.AreEqual(1, values.Count());
        }

        [TestMethod()]
        public void EditStrategyPointTest()
        {
            StrategyPoint sp = new StrategyPoint("Test", "TEST", "Test");

            Assert.IsTrue(sp.EditDescription("NEW"));
            Assert.AreEqual("NEW", sp.GetDescription());

            Assert.IsTrue(sp.EditName("NEW"));
            Assert.AreEqual("NEW", sp.GetName());

            StrategyPoint testsp = new StrategyPoint("NEW", "NEW", "NEW");
            Assert.AreEqual("NEW", sp.GetName());
            Assert.AreEqual("NEW", sp.GetDescription());

            Assert.IsTrue(sp.EditName("Test"));
            Assert.IsTrue(sp.EditDescription("TEST"));
        }


        }
}