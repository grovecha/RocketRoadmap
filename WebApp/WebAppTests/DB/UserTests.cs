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
    public class UserTests
    {
        [TestMethod()]
        public void UserTest()
        {
            User user = new User("NewName", "NewUserName", "NewEmail", "password");

            Assert.AreEqual("NewName", user.GetName());
            Assert.AreEqual("NewUserName", user.GetUserName());
            Assert.AreEqual("NewEmail", user.GetEmail());
            Assert.AreEqual("password", user.GetPassword());
        }
    }
}