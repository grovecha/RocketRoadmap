﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebApp.DB
{
    public class Link
    {
        private string mProjectName;
        private string mDescription;
        private string mLink;

        private WebApp.DB.Database mDatabase = new WebApp.DB.Database();
        private SqlDataReader mReader;

        public Link(string descrip, string projname, string link)
        {
            mProjectName = projname;
            mLink = link;
            mDescription = descrip;
        }

        #region Getter's and Setter's
        public bool SetDescription(string description)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Link] SET Description='" + description + "' WHERE Description='" + mDescription + "'");
            mDatabase.close();
            return flag;
        }
        public string GetDescription()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Description FROM [dbo].[Link] WHERE Description='" + mDescription + "'");
            mReader.Read();
            string descrip = mReader.GetString(0).ToString();
            mDatabase.close();
            return descrip;
        }
        public bool SetProjectName(string projectname)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Link] SET ProjectName='" + projectname + "' WHERE Description='" + mDescription + "'");
            mDatabase.close();
            return flag;
        }
        public string GetProjectName()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT ProjectName FROM [dbo].[Link] WHERE Description='" + mDescription + "'");

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
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Link] SET Address='" + link + "' WHERE Description='" + mDescription + "'");
            mDatabase.close();
            return flag;
        }
        public string GetLink()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Address FROM [dbo].[Link] WHERE Description='" + mDescription+"'");
            mReader.Read();
            string link = mReader.GetString(0).ToString();
            mDatabase.close();
            return link;
        }
        #endregion

        public bool InsertDB()
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("INSERT INTO [dbo].[Link] (Description, ProjectName, Address) VALUES ('" + mDescription + "','" + mProjectName + "','" + mLink + "')");
            mDatabase.close();
            return flag;
        }
    }
}