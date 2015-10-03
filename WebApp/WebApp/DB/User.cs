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
        #region Getters

        public string GetName()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT User FROM Project WHERE Name=" + mUserName);
            mReader.Read();
            return mReader.GetString(0).ToString();
        }

        public string GetUserName() {

            return "TEST"; // Database.execute("SELECT Name from [dbo].[User] WHERE Email='bpchiv@gmail.com'");
        }

        public string GetEmail() { return mEmail; }

        public string GetPassword() { return mPassword; }
        #endregion

        public bool NewUser(string name, string username, string email, string password )
        {
            mDatabase.connect();
            mDatabase.executewrite("INSERT INTO [dbo].[User] ( Name, UserName, Email, Password) VALUES (" + name + "," + username + "," + email + "," + password + ")");
            mDatabase.close();

            return false;
        }

        string mName;
        string mUserName;
        string mEmail;
        string mPassword;

        private WebApp.DB.Database mDatabase;
        private SqlDataReader mReader;
    }

}