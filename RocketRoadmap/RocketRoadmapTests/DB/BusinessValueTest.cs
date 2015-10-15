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
    public class BusinessValueTest
    {
        [TestMethod()]
        public void BusinessValueConstructorTest()
        {
            BusinessValue bv = new BusinessValue("test", "Test");

            Assert.IsNotNull(bv);
        }

        [TestMethod()]
        public void BusinessValueGetDescriptionTest()
        {
            BusinessValue bv = new BusinessValue("test", "Test");
            Assert.IsTrue(bv.GetDescription() == "test");
        }

        [TestMethod()]
        public void BusinessValueSetDescriptionTest()
        {
            BusinessValue bv = new BusinessValue("test", "Test");
            bv.SetDescription("test1");
            Assert.IsTrue(bv.GetDescription() == "test1");
            bv.SetDescription("test");
        }


        [TestMethod()]
        public void BusinessValueSetNameTest()
        {
            BusinessValue bv = new BusinessValue("test", "Test");
            Assert.IsTrue(bv.SetName("test3"));
            bv.SetName("test");
        }


    }
}
