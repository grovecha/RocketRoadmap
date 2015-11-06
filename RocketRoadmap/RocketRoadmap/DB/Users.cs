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

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[User] ( Name, ID, Email, Password ) VALUES (@Name,@User,@mail,@pass)";
            cmd.Parameters.AddWithValue("@Name", newuser.GetName());
            cmd.Parameters.AddWithValue("@User", newuser.GetUserName());
            cmd.Parameters.AddWithValue("@mail", newuser.GetEmail());
            cmd.Parameters.AddWithValue("@pass", newuser.GetPassword());
            if (mDatabase.executewriteparam(cmd))
            {
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        /**
        * Delete a  user 
        **/
        public bool DeleteUser(string username)
        {
            mDatabase.connect();
            bool toReturn = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM [dbo].[User] WHERE ID =@User";
            cmd.Parameters.AddWithValue("@User", username);
            if (mDatabase.executewriteparam(cmd))
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
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name, Email, Password FROM [dbo].[User] WHERE ID =@User";
            cmd.Parameters.AddWithValue("@User", username);
            mReader = mDatabase.executereadparams(cmd);
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
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name, Email FROM [dbo].[User] WHERE ID =@User AND " + "Password =@pass";
            cmd.Parameters.AddWithValue("@User", username);
            cmd.Parameters.AddWithValue("@pass", password);
            mReader = mDatabase.executereadparams(cmd);
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