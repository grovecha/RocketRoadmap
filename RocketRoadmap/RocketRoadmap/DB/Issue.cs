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
            mRoadmapName = name;
        }
        #region Getter's and Setter's
        public bool SetDescription(string description)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Issues] SET Description='" + description + "' WHERE Description='" + mDescription + "' AND RoadmapName='" + mRoadmapName + "'");
            mDatabase.close();
            return flag;
        }
        public string GetDescription()
        {
            return mDescription;
        }
        public bool SetProjectName(string projectname)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Issues] SET ProjectName='" + projectname + "' WHERE Description='" + mDescription + "' AND RoadmapName='" + mRoadmapName + "'");
            mDatabase.close();
            return flag;
        }
        public string GetProjectName()
        {
            return mProjectName;
        }
        #endregion

    }
}