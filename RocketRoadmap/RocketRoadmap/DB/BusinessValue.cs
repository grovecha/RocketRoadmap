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
        private string mRoadmapName;

        private List<Project> mProjects = new List<Project>();

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;


        public BusinessValue(string name, string rname)
        {
            mName = name;
            mRoadmapName = rname;

            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, Description FROM [dbo].[Project] WHERE BusinessValueName='" + mName + "' AND RoadmapName ='" + mRoadmapName + "'");
            while (mReader.Read())
            {
                mProjects.Add(new Project(mReader.GetString(0).ToString(), mReader.GetString(1).ToString(), mName, mRoadmapName));
            }
            mReader.Close();
            mReader = mDatabase.executeread("SELECT Description FROM [dbo].[BusinessValue] WHERE Name='" + mName + "'");
            if (mReader.HasRows)
            {
                mReader.Read();
                mDescription = mReader.GetString(0).ToString();
            }
            mDatabase.close();

        }

        public void AddProject(Project proj)
        {
            mProjects.Add(proj);
            proj.SetBusinessValue(mName);
        }

        #region Getter's and Setter's
        public string GetDescription()
        {
            return mDescription;
        }

        public bool SetDescription(string descrip)
        {
            mDescription = descrip;
            mDatabase.connect();
            bool flag=mDatabase.executewrite("UPDATE [dbo].[BusinessValue] SET Description='"+descrip+"' WHERE Name='"+mName+"' AND RoadmapName ='" + mRoadmapName + "'");
            mDatabase.close();
            return flag;

        }

        public bool SetName(string name)
        {
            mDatabase.connect();
            bool flag1 = mDatabase.executewrite("UPDATE [dbo].[SP_BV_Crosswalk] SET BusinessValueName='" + name + "' WHERE BusinessValueName='" + mName + "'");
            bool flag = mDatabase.executewrite("UPDATE [dbo].[BusinessValue] SET Name='" + name + "' WHERE Name='" + mName + "'AND RoadmapName ='" + mRoadmapName + "'");
            mName = name;
            mDatabase.close();
            return flag;
        }
        public string GetName() { return mName; }
        public List<Project> GetProjects() { return mProjects; }
        #endregion

        public Project GetProject(string id )
        {
            foreach (Project p in mProjects)
            {
                if (p.GetName() == id)
                {
                    return p;
                }
            }
            //oh no! Something went wrong! I blame brian.
            return null;
        }
   

        public bool CreateNewProject( Project proj)
        {
            mDatabase.connect();
            try
            {
                bool flag = mDatabase.executewrite("INSERT INTO [dbo].[Project] (Name, Description, BusinessValueName, RoadmapName) VALUES ('" + proj.GetName() + "', '" + proj.GetDescription() + "','" + proj.GetBusinessValue() + "','" + mRoadmapName + "')");
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