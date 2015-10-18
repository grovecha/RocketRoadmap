using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RocketRoadmap.DB
{
    public class Link
    {
        private string mProjectName;
        private string mDescription;
        private string mLink;
        private string mRoadmapName;

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;

        public Link(string descrip, string projname, string link, string RoadmapName)
        {
            mProjectName = projname;
            mLink = link;
            mDescription = descrip;
            mRoadmapName = RoadmapName;
        }

        #region Getter's and Setter's
        public bool SetDescription(string description)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Link] SET Description='" + description + "' WHERE Description='" + mDescription + "' AND RoadmapName='" + mRoadmapName + "'");
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
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Link] SET ProjectName='" + projectname + "' WHERE Description='" + mDescription + "' AND RoadmapName='" + mRoadmapName + "'");
            mDatabase.close();
            return flag;
        }
        public string GetProjectName()
        {
            return mProjectName;
        }
        public bool SetLink(string link)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Link] SET Address='" + link + "' WHERE Description='" + mDescription + "' AND RoadmapName='" + mRoadmapName + "'");
            mDatabase.close();
            return flag;
        }
        public string GetLink()
        {
            return mLink;
        }
        #endregion
    }
}