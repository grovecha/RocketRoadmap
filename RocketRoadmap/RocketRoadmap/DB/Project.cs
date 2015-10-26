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
        private string mRoadmapName;

        private List<string> mDependantString = new List<string>();
        private List<Issue> mIssues = new List<Issue>();
        private List<Link> mLinks = new List<Link>();
        private List<Project> mDependencies = new List<Project>();

        private RocketRoadmap.DB.Database mDatabase =  new Database();
        private SqlDataReader mReader;

        public Project(string name, string description, string businessvalue, string rname)
        {
            mName = name;
            mDescription = description;
            mBusinessValue = businessvalue;
            mRoadmapName = rname;


            mDatabase.connect();

            mReader=mDatabase.executeread("SELECT Description FROM [dbo].[Issues] WHERE ProjectName='" + mName + "' AND RoadmapName ='" + rname + "'");
            while (mReader.Read() )
            {
                mIssues.Add(new Issue(mReader.GetString(0).ToString(), mName, mRoadmapName));
            }
            mReader.Close();

            mReader = mDatabase.executeread("SELECT Description, Address FROM [dbo].[Link] WHERE ProjectName='" + mName + "' AND RoadmapName ='" + mRoadmapName + "'");
            while (mReader.Read() )
            {
                mLinks.Add(new Link(mReader.GetString(0).ToString(), mName, mReader.GetString(1).ToString(), mRoadmapName));
            }
            mReader.Close();

            mReader = mDatabase.executeread("SELECT DependantName, [dbo].[Project].Description, [dbo].[Project].BusinessValueName FROM [dbo].[Dependents], [dbo].[Project] WHERE ProjectName='" + mName + "' AND DependantName=Name");
            while (mReader.Read() )
            {
                mDependencies.Add(new Project(mReader.GetString(0).ToString(), mReader.GetString(1).ToString(), mReader.GetString(2).ToString(), mRoadmapName));
            }
            mReader.Close();
            mReader = mDatabase.executeread("SELECT StartDate,EndDate FROM [dbo].[Project] WHERE Name='" + mName + "'");
            if(mReader.HasRows)
            {
                mReader.Read();
                try { mStartDate = mReader.GetDateTime(0); }
                catch (Exception ex) { }
                try { mEndDate = mReader.GetDateTime(1); }
                catch (Exception ex) { }
            }
            mReader.Close();
            mDatabase.close();

            //mDatabase.connect();
            //mReader = mDatabase.executeread("SELECT Description FROM [dbo].[Dependents_string] WHERE ProjectName='" + mName + "' AND RoadmapName ='" + rname + "'");
            //while (mReader.Read())
            //{
            //    mDependantString.Add(mReader.GetString(0).ToString());
            //}
            //mReader.Close();

        }

        #region Getter's and Setters
        public bool SetName(string name) {
            mDatabase.connect();
            bool flag=mDatabase.executewrite("UPDATE [dbo].[Project] SET Name='" + name + "' WHERE Name='" + mName+ "' AND RoadmapName='" + mRoadmapName+ "'");
            mName = name;
            mDatabase.close();
            return flag;
        }
        public string GetName() {
            return mName;
        }

        public bool SetDescription(string description) {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("UPDATE [dbo].[Project] SET Description='" + description + "' WHERE Name='" + mName + "' AND RoadmapName='" + mRoadmapName + "'");
            mDatabase.close();
            mDescription = description;
            return flag;
        }
        public string GetDescription() {
            return mDescription;
        }

        public bool SetStartDate(DateTime startdate) {
            mDatabase.connect();
            bool flag= mDatabase.executewrite("UPDATE [dbo].[Project] SET StartDate='" + startdate + "' WHERE Name='" + mName+ "' AND RoadmapName='" + mRoadmapName + "'");
            mDatabase.close();
            mStartDate = startdate;
            return flag;
        }

        public bool UpdateDependantStrings(List<string> Dependants)
        {

                mDatabase.connect();
                bool flag = mDatabase.executewrite("DELETE FROM [dbo].[Dependents_string] WHERE ProjectName='" + mName + "' AND RoadmapName='" + mRoadmapName + "'");

                foreach (string element in Dependants)
                {
                     flag = mDatabase.executewrite("INSERT INTO [dbo].[Dependents_string](ProjectName, DependantString, RoadmapName) VALUES ( '" + mName + "','" + element + "','" + mRoadmapName + "')");
                }


            mDatabase.close();
            return flag;

        }

        public DateTime GetStartDate() {
            return mStartDate;
        }

        public bool SetEndDate(DateTime enddate) {
            mDatabase.connect();
            bool flag=mDatabase.executewrite("UPDATE [dbo].[Project] SET EndDate='" + enddate + "' WHERE Name='" + mName+ "' AND RoadmapName='" + mRoadmapName + "'");
            mDatabase.close();
            mEndDate = enddate;
            return flag;
        }
        public DateTime GetEndDate() {
            return mEndDate;
        }

        public bool SetBusinessValue(string businessvalue) {
            mDatabase.connect();
            bool flag=mDatabase.executewrite("UPDATE [dbo].[Project] SET BusinessValueName='" + businessvalue + "' WHERE Name='" + mName+ "' AND RoadmapName='" + mRoadmapName + "'");
            mDatabase.close();
            mBusinessValue = businessvalue;
            return flag;
        }
        public string GetBusinessValue() {
            return mBusinessValue;
        }
        public List<Link> GetLinks() { return mLinks; }
        public List<Issue> GetIssues() { return mIssues; }
        public List<Project> GetDependencies() { return mDependencies; }
        #endregion

        public bool CreateLink(Link link)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("INSERT INTO [dbo].[Link] (Description, ProjectName, Address, RoadmapName) VALUES ('" + link.GetDescription() + "','" + link.GetProjectName() + "','" + link.GetLink() + "'" + mRoadmapName + ")");
            mDatabase.close();
            return flag;
        }

        public bool CreateIssue(Issue i)
        {
            mDatabase.connect();
            bool flag = mDatabase.executewrite("INSERT INTO [dbo].[Issues] (Description, ProjectName, RoadmapName) VALUES ('" + i.GetDescription() + "','" + i.GetProjectName() + "'," + mRoadmapName + "')");
            mDatabase.close();
            return flag;
        }

        public bool CreateDependant(Project dependant)
        {
            //assume already created
            mDatabase.connect();
            bool flag = mDatabase.executewrite("INSERT INTO [dbo].[Dependents] (ProjectName, Dependantname, Description, RoadmapName) VALUES ('" + mName + "','" + dependant.GetName() + "','" + dependant.GetDescription() + "','" + mRoadmapName + "')");

            mDependencies.Add(dependant);

            mDatabase.close();
            return flag;
        }

    }
}