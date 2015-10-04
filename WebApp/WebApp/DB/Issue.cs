using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebApp.DB
{
    public class Issue
    {
        private string mDescription;
        private string mProjectName;

        private WebApp.DB.Database mDatabase;
        private SqlDataReader mReader;

        #region Getter's and Setter's
        public void SetDescription(string description)
        {
            mDatabase.connect();
            mDatabase.executewrite("UPDATE Issues SET Description=" + description + " WHERE Description=" + mDescription);
            mDatabase.close();
        }
        public string GetDescription()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Description FROM Issues WHERE Description=" + mDescription);
            mReader.Read();
            mDatabase.close();
            return mReader.GetString(0).ToString();
        }
        public void SetProjectName(string projectname)
        {
            mDatabase.connect();
            mDatabase.executewrite("UPDATE Issues SET ProjectName=" + projectname + " WHERE Description=" + mDescription);
            mDatabase.close();
        }
        public string GetProjectName()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT ProjectName FROM Issues WHERE Description=" + mDescription);
            mReader.Read();
            mDatabase.close();
            return mReader.GetString(0).ToString();
        }
        #endregion
    }
}