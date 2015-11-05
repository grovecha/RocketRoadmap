using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

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

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;

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
            //Connect to DB and change description.
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Issues] SET Description=@descrip WHERE Description=@mDescrip AND RoadmapName=@Rname";
            cmd.Parameters.AddWithValue("@descrip", description);
            cmd.Parameters.AddWithValue("@mDescrip", mDescription);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag = mDatabase.executewriteparam(cmd);
            mDatabase.close();

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
            //Change project name in DB
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Issues] SET ProjectName=@proj WHERE Description=@descrip AND RoadmapName=@Rname";
            cmd.Parameters.AddWithValue("@proj", projectname);
            cmd.Parameters.AddWithValue("@descrip", mDescription);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag = mDatabase.executewriteparam(cmd);
            mDatabase.close();
            
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