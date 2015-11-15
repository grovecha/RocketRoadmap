using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    public class User
    {
        public User(string username, string password)
        {
            mUserName = username;
            mPassword = password;
        }
       public User( string name, string username, string email, string password )
        {
            mName = name;
            mUserName = username;
            mEmail = email;
            mPassword = password;
        }

       public bool Login()
       {
           if (mUserName == null) return false;

           using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
           {
               using (SqlCommand cmd = new SqlCommand())
               {
                   cmd.CommandText = "SELECT Password FROM [dbo].[User] WHERE ID=@User";
                   cmd.Parameters.AddWithValue("@User", mUserName);
                   cmd.Connection = conn;

                   conn.Open();
                   using (SqlDataReader Reader = cmd.ExecuteReader())
                   {
                       if (Reader.HasRows)
                       {
                           Reader.Read();
                           if (Reader.GetString(0).ToString() == mPassword)
                           {
                               return true;
                           }
                       }
                   }
                   conn.Close();
               }
           }
           return false;
       }
        public bool EditName(string name)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[User] SET Name =@User WHERE ID =@oldname";
                    cmd.Parameters.AddWithValue("@User", name);
                    cmd.Parameters.AddWithValue("@oldname", mUserName);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        mName = name;
                        toReturn = true;
                    }
                    conn.Close();
                }
            }

            return toReturn;
        }

        public bool EditEmail(string mail)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[User] SET Email =@mail WHERE ID =@User";
                    cmd.Parameters.AddWithValue("@mail", mail);
                    cmd.Parameters.AddWithValue("@User", mUserName);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        mEmail = mail;
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        public bool EditPassword(string pass)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[User] SET Password = @pass WHERE ID = @User";
                    cmd.Parameters.AddWithValue("@pass", pass);
                    cmd.Parameters.AddWithValue("@User", mUserName);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        mPassword = pass;
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        public string GetName(){ return mName; }
        public string GetUserName() { return mUserName; }
        public string GetEmail() { return mEmail; }
        public string GetPassword() { return mPassword; }

        private string mName;
        private string mUserName;
        private string mEmail;
        private string mPassword;

       // private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
      //  private SqlDataReader mReader;
    }

}