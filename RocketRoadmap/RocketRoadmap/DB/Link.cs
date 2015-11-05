using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RocketRoadmap.DB
{
    //Class for links that projects own
    public class Link
    {
        //Project that owns it
        private string mProjectName;
        //Description of link
        private string mDescription;
        //Actual link or address
        private string mLink;
        //Roadmap this belongs to
        private string mRoadmapName;

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;

        //Constructor
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
            //Connect to DB and change in DB
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Link] SET Description=@descrip WHERE Description=@mDescrip AND RoadmapName=@Rname";
            cmd.Parameters.AddWithValue("@descrip", description);
            cmd.Parameters.AddWithValue("@mDescrip", mDescription);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag = mDatabase.executewriteparam(cmd);
            mDatabase.close();

            //Change in object
            mDescription = description;
            return flag;
        }
        public string GetDescription()
        {
            return mDescription;
        }
        public bool SetProjectName(string projectname)
        {
            //Change in DB
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Link] SET ProjectName=@Proj WHERE Description=@mDescrip AND RoadmapName=@Rname";
            cmd.Parameters.AddWithValue("@Proj", projectname);
            cmd.Parameters.AddWithValue("@mDescrip", mDescription);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag = mDatabase.executewriteparam(cmd);
            mDatabase.close();

            //Change in object
            mProjectName = projectname;
            return flag;
        }
        public string GetProjectName()
        {
            return mProjectName;
        }
        public bool SetLink(string link)
        {
            //Change in DB
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Link] SET Address=@link WHERE Description=@mDescrip AND RoadmapName=@Rname";
            cmd.Parameters.AddWithValue("@link", link);
            cmd.Parameters.AddWithValue("@mDescrip", mDescription);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag = mDatabase.executewriteparam(cmd);
            mDatabase.close();

            //Change in object
            mLink = link;
            return flag;
        }
        public string GetLink()
        {
            return mLink;
        }
        #endregion
    }
}