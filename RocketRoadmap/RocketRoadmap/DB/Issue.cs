using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    //Issue/ Risks for Projects
    public class Issue
    {
        //Description of issue or risk
        private string mDescription;
        //Project it belongs to
        private string mProjectName;
        //Roadmap is belongs to
        private string mRoadmapName;

       // private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
      //  private SqlDataReader mReader;

        //CONSTRUCTOR
        public Issue(string description, string projname, string name)
        {
            mDescription = description;
            mProjectName = projname;
            mRoadmapName = name;
        }
        #region Getter's and Setter's
        public bool SetDescription(string description)
        {
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[Issues] SET Description=@descrip WHERE Description=@mDescrip AND RoadmapName=@Rname";
                    cmd.Parameters.AddWithValue("@descrip", description);
                    cmd.Parameters.AddWithValue("@mDescrip", mDescription);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }

            //Add in object
            mDescription = description;
            return flag;
        }
        public string GetDescription()
        {
            return mDescription;
        }
        public bool SetProjectName(string projectname)
        {
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[Issues] SET ProjectName=@proj WHERE Description=@descrip AND RoadmapName=@Rname";
                    cmd.Parameters.AddWithValue("@proj", projectname);
                    cmd.Parameters.AddWithValue("@descrip", mDescription);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }
            //Set in object
            mProjectName = projectname;
            return flag;
        }
        public string GetProjectName()
        {
            return mProjectName;
        }
        #endregion

    }
}