using System;
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

        private WebApp.DB.Database mDatabase;
        private SqlDataReader mReader;

        #region Getter's and Setter's
        public void SetDescription(string description)
        {
            mDatabase.connect();
            mDatabase.executewrite("UPDATE Link SET Description=" + description + " WHERE Description=" + mDescription);
            mDatabase.close();
        }
        public string GetDescription()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Description FROM Link WHERE Description=" + mDescription);
            mReader.Read();
            mDatabase.close();
            return mReader.GetString(0).ToString();
        }
        public void SetProjectName(string projectname)
        {
            mDatabase.connect();
            mDatabase.executewrite("UPDATE Link SET ProjectName=" + projectname + " WHERE Description=" + mDescription);
            mDatabase.close();
        }
        public string GetProjectName()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT ProjectName FROM Link WHERE Description=" + mDescription);
            mReader.Read();
            mDatabase.close();
            return mReader.GetString(0).ToString();
        }
        public void SetLink(string link)
        {
            mDatabase.connect();
            mDatabase.executewrite("UPDATE Link SET Link=" + link + " WHERE Description=" + mDescription);
            mDatabase.close();
        }
        public string GetLink()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Link FROM Link WHERE Description=" + mDescription);
            mReader.Read();
            mDatabase.close();
            return mReader.GetString(0).ToString();
        }
        #endregion
    }
}