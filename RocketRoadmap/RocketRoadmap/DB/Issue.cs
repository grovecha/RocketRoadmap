using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RocketRoadmap.DB
{
    public class Issue
    {
        private string mDescription;
        private string mProjectName;
        private string mRoadmapName;

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;

        public Issue(string description, string projname, string name)
        {
            mDescription = description;
            mProjectName = projname;
        }
        #region Getter's and Setter's
        public bool SetDescription(string description)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Issues] SET Description='" + description + "' WHERE Description='" + mDescription + "' AND RoadmapName '" + mRoadmapName + "'");
            mDatabase.close();
            return flag;
        }
        public string GetDescription()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Description FROM [dbo].[Issues] WHERE Description='" + mDescription + "' AND RoadmapName '" + mRoadmapName + "'");
            mReader.Read();
            string descrip = mReader.GetString(0).ToString();
            mDatabase.close();
            return descrip;
        }
        public bool SetProjectName(string projectname)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Issues] SET ProjectName='" + projectname + "' WHERE Description='" + mDescription + "' AND RoadmapName '" + mRoadmapName + "'");
            mDatabase.close();
            return flag;
        }
        public string GetProjectName()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT ProjectName FROM [dbo].[Issues] WHERE Description='" + mDescription+ "' AND RoadmapName '" + mRoadmapName + "'");
            mReader.Read();
            string proj = mReader.GetString(0).ToString();
            mDatabase.close();
            return proj;
        }
        #endregion

    }
}