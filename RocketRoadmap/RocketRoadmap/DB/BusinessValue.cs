using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RocketRoadmap.DB
{
    public class BusinessValue
    {
        private string mName;
        private string mDescription;

        private List<Project> mProjects = new List<Project>();

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;


        public BusinessValue(string name)
        {
            mName = name;

            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, Description FROM [dbo].[Project] WHERE BusinessValueName='" + mName + "'");
            while (mReader.Read())
            {
                mProjects.Add(new Project(mReader.GetString(0).ToString(), mReader.GetString(1).ToString(), mName));
            }
            mDatabase.close();
        }

        private void AddProject(Project proj)
        {
            mProjects.Add(proj);
            proj.InsertDB();
        }

        #region Getter's and Setter's
        public string GetDescription()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Description FROM [dbo].[BusinessValue] WHERE Name='" + mName + "'");
            mReader.Read();
            if (mReader.HasRows)
            {
                mDescription = mReader.GetString(0).ToString();
                return mDescription;
            }
            else return "";
        }

        public bool SetDescription(string descrip)
        {
            mDescription = descrip;
            mDatabase.connect();
            bool flag=mDatabase.executewrite("UPDATE [dbo].[BusinessValue] SET Description='"+descrip+"' WHERE Name='"+mName+"'");
            mDatabase.close();
            return flag;

        }

        public bool SetName(string name)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[BusinessValue] SET Name='" + name + "' WHERE Name='" + mName + "'");
            mName = name;
            mDatabase.close();
            return flag;
        }
        public string GetName() { return mName; }
        public List<Project> GetProjects() { return mProjects; }
        #endregion


        public bool InsertDB()
        {
            mDatabase.connect();
            try
            {
                bool flag = mDatabase.executewrite("INSERT INTO [dbo].[BusinessValue] (Name) VALUES ('" + mName + "')");
                mDatabase.close();
                return flag;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}