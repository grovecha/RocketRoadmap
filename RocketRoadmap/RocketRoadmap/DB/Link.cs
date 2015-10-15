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
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Link] SET Description='" + description + "' WHERE Description='" + mDescription + "' AND RoadmapName '" + mRoadmapName + "'");
            mDatabase.close();
            return flag;
        }
        public string GetDescription()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Description FROM [dbo].[Link] WHERE Description='" + mDescription + "' AND RoadmapName '" + mRoadmapName + "'");
            mReader.Read();
            string descrip = mReader.GetString(0).ToString();
            mDatabase.close();
            return descrip;
        }
        public bool SetProjectName(string projectname)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Link] SET ProjectName='" + projectname + "' WHERE Description='" + mDescription + "' AND RoadmapName '" + mRoadmapName + "'");
            mDatabase.close();
            return flag;
        }
        public string GetProjectName()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT ProjectName FROM [dbo].[Link] WHERE Description='" + mDescription + "' AND RoadmapName '" + mRoadmapName + "'");

            string name="";
            if (mReader.HasRows)
            {
                mReader.Read();
                name = mReader.GetString(0).ToString();
            }
            mDatabase.close();
            return name;
        }
        public bool SetLink(string link)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Link] SET Address='" + link + "' WHERE Description='" + mDescription + "' AND RoadmapName '" + mRoadmapName + "'");
            mDatabase.close();
            return flag;
        }
        public string GetLink()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Address FROM [dbo].[Link] WHERE Description='" + mDescription+ "' AND RoadmapName '" + mRoadmapName + "'");
            mReader.Read();
            string link = mReader.GetString(0).ToString();
            mDatabase.close();
            return link;
        }
        #endregion
    }
}