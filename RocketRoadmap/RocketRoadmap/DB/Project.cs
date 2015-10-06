using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RocketRoadmap.DB
{
    public class Project
    {
        private string mName;
        private string mDescription;
        private DateTime mStartDate;
        private DateTime mEndDate;
        private string mBusinessValue;

        private List<Issue> mIssues = new List<Issue>();
        private List<Link> mLinks = new List<Link>();
        private List<Project> mDependencies = new List<Project>();

        private RocketRoadmap.DB.Database mDatabase =  new Database();
        private SqlDataReader mReader;

        public Project(string name, string description, string businessvalue)
        {
            mName = name;
            mDescription = description;
            mBusinessValue = businessvalue;

            mDatabase.connect();

            mReader=mDatabase.executeread("SELECT Description FROM [dbo].[Issues] WHERE ProjectName='" + mName + "'");
            while (mReader.Read() )
            {
                mIssues.Add(new Issue(mReader.GetString(0).ToString(), mName));
            }
            mReader.Close();

            mReader = mDatabase.executeread("SELECT Description, Address FROM [dbo].[Link] WHERE ProjectName='" + mName + "'");
            while (mReader.Read() )
            {
                mLinks.Add(new Link(mReader.GetString(0).ToString(), mName, mReader.GetString(1).ToString()));
            }
            mReader.Close();

            mReader = mDatabase.executeread("SELECT DependantName, [dbo].[Project].Description, [dbo].[Project].BusinessValueName FROM [dbo].[Dependents], [dbo].[Project] WHERE ProjectName='" + mName + "' AND DependantName=Name");
            while (mReader.Read() )
            {
                mDependencies.Add(new Project(mReader.GetString(0).ToString(), mReader.GetString(1).ToString(), mReader.GetString(2).ToString()));
            }
            mReader.Close();

        }

        #region Getter's and Setters
        public bool SetName(string name) {
            mDatabase.connect();
            bool flag=mDatabase.executewrite("UPDATE [dbo].[Project] SET Name='" + name + "' WHERE Name='" + mName+"'");
            mName = name;
            mDatabase.close();
            return flag;
        }
        public string GetName() {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name FROM [dbo].[Project] WHERE Name='" + mName + "'");
            mReader.Read();
            string name = mReader.GetString(0).ToString();
            mDatabase.close();
            return name;
        }

        public bool SetDescription(string description) {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Project] SET Description='" + description + "' WHERE Name='" + mName + "'");
            mDatabase.close();
            mDescription = description;
            return flag;
        }
        public string GetDescription() {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Description FROM [dbo].[Project] WHERE Name='" + mName+"'");
            mReader.Read();
            string descrip = mReader.GetString(0).ToString();
            mDatabase.close();
            return descrip;
        }

        public bool SetStartDate(DateTime startdate) {
            mDatabase.connect();
            bool flag= mDatabase.executewrite("UPDATE [dbo].[Project] SET StartDate='" + startdate + "' WHERE Name='" + mName+"'");
            mDatabase.close();
            mStartDate = startdate;
            return flag;
        }
        public DateTime GetStartDate() {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT StartDate FROM [dbo].[Project] WHERE Name='" + mName+"'");
            mReader.Read();
            DateTime start = mReader.GetDateTime(0);
            mDatabase.close();
            return start;
        }

        public bool SetEndDate(DateTime enddate) {
            mDatabase.connect();
            bool flag=mDatabase.executewrite("UPDATE [dbo].[Project] SET EndDate='" + enddate + "' WHERE Name='" + mName+"'");
            mDatabase.close();
            mEndDate = enddate;
            return flag;
        }
        public DateTime GetEndDate() {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT EndDate FROM [dbo].[Project] WHERE Name='" + mName+"'");
            mReader.Read();
            DateTime end = mReader.GetDateTime(0);
            mDatabase.close();
            return end;
        }

        public bool SetBusinessValue(string businessvalue) {
            mDatabase.connect();
            bool flag=mDatabase.executewrite("UPDATE [dbo].[Project] SET BusinessValueName='" + businessvalue + "' WHERE Name='" + mName+"'");
            mDatabase.close();
            mBusinessValue = businessvalue;
            return flag;
        }
        public string GetBusinessValue() {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT BusinessValueName FROM [dbo].[Project] WHERE Name='" + mName+"'");
            mReader.Read();
            string bus = mReader.GetString(0).ToString();
            mDatabase.close();
            return bus;
        }
        #endregion

        public bool InsertDB()
        {
            mDatabase.connect();
            try
            {
                bool flag = mDatabase.executewrite("INSERT INTO [dbo].[Project] (Name,Description, BusinessValueName) VALUES ('" + mName + "', '"+mDescription+"','"+mBusinessValue+"')");
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