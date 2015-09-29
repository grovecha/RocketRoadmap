using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.DB
{
    public class User
    {
        /**
        * Getters and Setters for User DB object
        */
        #region Getters and Setters
        public void SetName(string name) { mName = name; }
        public string GetName() { return mName; }

        public void SetUserName(string UserName) { mUserName = UserName; }
        public string GetUserName() {

            return "TEST"; // Database.execute("SELECT Name from [dbo].[User] WHERE Email='bpchiv@gmail.com'");
        }

        public void SetEmail(string email) { mEmail = email; }
        public string GetEmail() { return mEmail; }

        public void SetPassword(string pass) { mPassword = pass; }
        public string GetPassword() { return mPassword; }
        #endregion

        string mName;
        string mUserName;
        string mEmail;
        string mPassword;
        
    }

}