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
    public class StrategyPointTests
    {
        [TestMethod()]
        public void StrategyPointTest()
        {
            StrategyPoint sp = new StrategyPoint("test", "TEST");

            Assert.AreEqual("test", sp.GetName());
            Assert.AreEqual("TEST", sp.GetDescription());
        }
    }
}