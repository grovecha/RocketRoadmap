using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
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
        * Create a new user 
        **/
        public bool DeleteUser(string username)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("DELETE FROM [dbo].[User] WHERE ID = '" + username + "'" ))
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
            mReader = mDatabase.executeread("SELECT Name, Email, Password FROM [dbo].[User] WHERE ID = '" + username + "'" );
            mReader.Read();

            User user = new User(mReader.GetString(0).ToString(), username, mReader.GetString(1).ToString(), mReader.GetString(2).ToString());
            mDatabase.close();
            return user;
        }

        /**
        * Login a user 
        **/
        public bool Login( string username, string password )
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, Email FROM [dbo].[User] WHERE ID = '" + username + "' AND " + "Password = '" + password + "'" );
            mReader.Read();

            if( mReader.HasRows ) {
                mReader.Close();
                return true;
            }
            else
            {
                mReader.Close();
                return false;
            }
        }

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;
    }
}