//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using RocketRoadmap.DB;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RocketRoadmap.DB.Tests
//{
//    [TestClass()]
//    public class UserTests
//    {
//        [TestMethod()]
//        public void UserTest()
//        {
//            User user = new User("NewName", "NewUserName", "NewEmail", "password");

//            Assert.AreEqual("NewName", user.GetName());
//            Assert.AreEqual("NewUserName", user.GetUserName());
//            Assert.AreEqual("NewEmail", user.GetEmail());
//            Assert.AreEqual("password", user.GetPassword());
//        }

//        [TestMethod()]
//        public void EditUserTest()
//        {
//            Users users = new Users();
//            User user = users.GetUser("123");

//            Assert.IsTrue(user.EditName("TEST"));
//            Assert.IsTrue(user.EditEmail("TEST"));
//            Assert.IsTrue(user.EditPassword ("TEST"));

//            Assert.AreEqual("TEST", user.GetName());
//            Assert.AreEqual("TEST", user.GetEmail());
//            Assert.AreEqual("TEST", user.GetPassword());

//            Assert.IsTrue(user.EditName("Brian"));
//            Assert.IsTrue(user.EditEmail("bpchiv@gmail.com"));
//            Assert.IsTrue(user.EditPassword("password"));
//        }
//    }
//}