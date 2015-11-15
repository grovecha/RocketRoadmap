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
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO [dbo].[User] ( Name, ID, Email, Password ) VALUES (@Name,@User,@mail,@pass)";
                    cmd.Parameters.AddWithValue("@Name", newuser.GetName());
                    cmd.Parameters.AddWithValue("@User", newuser.GetUserName());
                    cmd.Parameters.AddWithValue("@mail", newuser.GetEmail());
                    cmd.Parameters.AddWithValue("@pass", newuser.GetPassword());
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        /**
        * Delete a  user 
        **/
        public bool DeleteUser(string username)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "DELETE FROM [dbo].[User] WHERE ID =@User";
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        /**
        * Get all the information for a user
        **/
        public User GetUser( string username )
        {
            User user;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name, Email, Password FROM [dbo].[User] WHERE ID =@User";
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        Reader.Read();

                        user = new User(Reader.GetString(0).ToString(), username, Reader.GetString(1).ToString(), Reader.GetString(2).ToString());
                    }
                    conn.Close();
                 }
            }
            return user;
        }

        /**
        * Login a user 
        **/
        public bool Login( string username, string password )
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name, Email FROM [dbo].[User] WHERE ID =@User AND " + "Password =@pass";
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Parameters.AddWithValue("@pass", password);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        Reader.Read();

                        if (Reader.HasRows)
                        {
                            Reader.Close();
                            return true;
                        }
                        else
                        {
                            Reader.Close();
                            return false;
                        }
                    }
                    conn.Close();
                }
            }
        }

      //  private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        //private SqlDataReader mReader;
    }
}