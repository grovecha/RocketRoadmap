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
    public class BusinessValueTest
    {
        [TestMethod()]
        public void BusinessValueConstructorTest()
        {
            BusinessValue bv = new BusinessValue("test");

            Assert.IsTrue(bv.InsertDB());
        }

        [TestMethod()]
        public void BusinessValueGetDescriptionTest()
        {
            BusinessValue bv = new BusinessValue("test");
            Assert.IsTrue(bv.GetDescription() == "test");
        }

        [TestMethod()]
        public void BusinessValueSetDescriptionTest()
        {
            BusinessValue bv = new BusinessValue("test");
            bv.SetDescription("test2");
            Assert.IsTrue(bv.GetDescription() == "test2");
        }


        [TestMethod()]
        public void BusinessValueSetNameTest()
        {
            BusinessValue bv = new BusinessValue("test");
            bv.SetName("test2");

            BusinessValue bv2 = new BusinessValue("test2");
            Assert.IsFalse(bv2.InsertDB());
        }


    }
}
