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
    public class UsersTests
    {

        [TestMethod()]
        public void CreateUserTest()
        {
            User newuser = new User("NewName", "NewUserName", "NewEmail", "password");
            Users users = new Users();

            Assert.IsTrue(users.CreateUser(newuser));
        }

   /*     [TestMethod()]
        public void GetUserTest()
        {
            Users users = new Users();
            User test = users.GetUser("bchivers");

            Assert.AreSame("Brian", test.GetName());
        }

        [TestMethod()]
        public void LoginTest()
        {
            Users test = new Users();

            User loggedin = test.Login("bchivers", "password");
            Assert.IsNull(loggedin);

            loggedin = test.Login("bchivers", "password");
            Assert.AreSame("Brian", loggedin.GetName());
        }
        */
    }
}
