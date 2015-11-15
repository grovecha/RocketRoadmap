using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    public class TickMark
    {
        public TickMark(string name, int x)
        {
            mName = name;
            mXPlacement = x;
        }

        public bool EditTickName(string name, string tname)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[TickMark] SET Name =@name WHERE TimelineName = @Tname";
                    cmd.Parameters.AddWithValue("@Tname", tname);
                    cmd.Parameters.AddWithValue("@name", name);
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

        public bool EditTickLocation(int X, string name)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE[dbo].[TickMark] SET XPlacement =@X WHERE TimelineName = @name";
                    cmd.Parameters.AddWithValue("@X", X);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        mXPlacement = X;
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        public string GetName() { return mName; } //label for the tickmark (ex. December)
        public int GetXPlacement() { return mXPlacement; } //Used for placement on the timeline

        private string mName;
        private int mXPlacement;

        //private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        //private SqlDataReader mReader;
    }
}