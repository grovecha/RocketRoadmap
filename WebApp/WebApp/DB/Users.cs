using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.DB
{
    public class Users
    {

        public Users() {  }

        /**
        * Create a new user 
        **/
        public bool CreateUser( User newuser )
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("INSERT INTO [dbo].[User] ( Name, ID, Email, Password ) VALUES (" + "'" + newuser.GetName() + "'" + ',' + "'" + newuser.GetUserName() + "'" + ',' + "'" + newuser.GetEmail() + "'" + ',' + "'" + newuser.GetPassword() + "')"))
            {
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        /**
        * Get all the information for a user
        **/
        public User GetUser( string username )
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, Email, Password FROM [dbo].[User] WHERE UserName = " + username );
            mReader.Read();

            User user = new User(mReader.GetString(0).ToString(), username, mReader.GetString(1).ToString(), mReader.GetString(2).ToString());
            return user;
        }

        /**
        * Login a user 
        **/
        public User Login( string username, string password )
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, Email FROM [dbo].[User] WHERE UserName = " + username + "Password = " + password );
            mReader.Read();

            User user = new User(mReader.GetString(0).ToString(), username, mReader.GetString(1).ToString(), password);
            return user;
        }

        private WebApp.DB.Database mDatabase = new WebApp.DB.Database();
        private SqlDataReader mReader;

    }
}