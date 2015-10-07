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
    public class UsersTests
    {

        [TestMethod()]
        public void CreateandDeleteUsersTest()
        {
            User newuser = new User("NewName", "NewUserName", "NewEmail", "password");
            Users users = new Users();

            Assert.IsTrue(users.CreateUser(newuser));

            Assert.IsTrue(users.DeleteUser(newuser.GetUserName()));
        }

        [TestMethod()]
        public void GetUserTest()
        {
            Users users = new Users();
            User test = users.GetUser("123");

            Assert.AreEqual("Brian", test.GetName());
            Assert.AreEqual("bpchiv@gmail.com", test.GetEmail());
            Assert.AreEqual("password", test.GetPassword());
            Assert.AreEqual("123", test.GetUserName());
        }

        [TestMethod()]
        public void LoginTest()
        {
            Users test = new Users();

            bool loggedin = test.Login("wrongusername", "password");
            Assert.IsFalse(loggedin);

            loggedin = test.Login("123", "wrongpassword");
            Assert.IsFalse(loggedin);

            loggedin = test.Login("123", "password");
            Assert.IsTrue(loggedin);
        }
        
    }
}
