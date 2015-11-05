using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RocketRoadmap.DB
{
    public class BusinessValue
    {
        //Name of the Business Value
        private string mName;
        //Description of the Business Value
        private string mDescription;
        //Roadmap that owns the Business Value
        private string mRoadmapName;

        //List of projects under this business value
        private List<Project> mProjects = new List<Project>();

        //Database object and data reader
        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;

        //CONSTRUCTOR
        public BusinessValue(string name, string rname)
        {
            //Set name and Rname
            mName = name;
            mRoadmapName = rname;

            //Add projects (If business value already exists, and has projects)
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText= "SELECT Name, Description FROM [dbo].[Project] WHERE BusinessValueName=@Name AND RoadmapName =@Rname";
            cmd.Parameters.AddWithValue("@Name", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            mReader = mDatabase.executereadparams(cmd);
            while (mReader.Read())
            {
                mProjects.Add(new Project(mReader.GetString(0).ToString(), mReader.GetString(1).ToString(), mName, mRoadmapName));
            }
            mReader.Close();

            //Grab the description if the BV exists
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandText = "SELECT Description FROM [dbo].[BusinessValue] WHERE Name=@Name AND RoadmapName=@Rname";
            cmd2.Parameters.AddWithValue("@Name", mName);
            cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);
            
            mReader = mDatabase.executereadparams(cmd2);
            if (mReader.HasRows)
            {
                mReader.Read();
                mDescription = mReader.GetString(0).ToString();
                mReader.Close();

            }
            mDatabase.close();

        }

        //Add a project to the project list
        public void AddProject(Project proj)
        {
            //Add it to the list
            mProjects.Add(proj);
            //Set the BV Name
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
            string descripset = "UPDATE [dbo].[BusinessValue] SET Description=@Descrip WHERE Name=@Name AND RoadmapName =@Rname";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = descripset;
            cmd.Parameters.AddWithValue("@Descrip", descrip);
            cmd.Parameters.AddWithValue("@Name", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);

            mDatabase.connect();
            bool flag=mDatabase.executewriteparam(cmd);
            mDatabase.close();
            return flag;

        }

        public bool SetName(string name)
        {
            mDatabase.connect();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "UPDATE [dbo].[SP_BV_Crosswalk] SET BusinessValueName=@BVName WHERE BusinessValueName=@BVName1 AND RoadmapName=@Rname";
            cmd1.Parameters.AddWithValue("@BVName",name);
            cmd1.Parameters.AddWithValue("@BVName1",mName);
            cmd1.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag1 = mDatabase.executewriteparam(cmd1);

            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandText = "UPDATE [dbo].[BusinessValue] SET Name=@AddName WHERE Name=@BVName AND RoadmapName =@Rname";
            cmd2.Parameters.AddWithValue("@AddName", name);
            cmd2.Parameters.AddWithValue("@BVName", mName);
            cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag = mDatabase.executewriteparam(cmd2);
            mName = name;
            mDatabase.close();
            return flag;
        }
        public string GetName() { return mName; }
        public List<Project> GetProjects() { return mProjects; }
        public Project GetProject(string id)
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

        #endregion

        //Delete project
        public bool DeleteProject(string name)
        {
            //Delete project from list
            bool flag = false;
            foreach (Project proj in mProjects.ToList())
            {
                if (proj.GetName() == name)
                {
                    flag = true;
                    mProjects.Remove(proj);
                    break;
                }
            }
            if (!flag) return false;
            mDatabase.connect();

            //Delete in database
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Pname", name);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            cmd.Parameters.AddWithValue("@BVName", mName);
            cmd.CommandText = "DELETE FROM [dbo].[Project] WHERE Name=@Pname AND RoadmapName=@Rname AND BusinessValueName=@BVName";
            bool flag2 = mDatabase.executewriteparam(cmd);
            mDatabase.close();

            if (!flag2) return false;

            //Reorder projects below this
            int index = (int)Char.GetNumericValue(name[23]);

            foreach (Project proj in mProjects.ToList())
            {
                if ((int)Char.GetNumericValue(proj.GetName()[23]) > index)
                {
                    int newindex = (int)Char.GetNumericValue(proj.GetName()[23]) - 1;
                    string newname = proj.GetName().Substring(0, 23) + newindex.ToString();
                    proj.SetName(newname);
                }
            }
            return true;
        }       

        //Reorders projects based on deleting currname
        public void ReorderProject(string currname, string desc, bool isFirst)
        {
            Project current = new Project(currname, desc, mName, mRoadmapName);
            //Grab the # of the project to be deleted
            int index = (int)Char.GetNumericValue(currname[23]) + 1;
            string nextID = currname.Substring(0, 23) + index.ToString();
            string nextdesc = null;

            string selectname = null;
            if (isFirst) selectname = currname;
            else selectname = nextID;

            //Select the description of the project to be moved
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Description FROM [dbo].[Project] WHERE Name=@PName AND RoadmapName=@Rname AND BusinessValueName=@BVName";
            cmd.Parameters.AddWithValue("@PName", selectname);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            cmd.Parameters.AddWithValue("@BVName", mName);
            mDatabase.connect();
            mReader = mDatabase.executereadparams(cmd);
            if (mReader.HasRows)
            {
                mReader.Read();
                nextdesc = mReader.GetString(0);
                mReader.Close();
            }
            mDatabase.close();

            //Change the name of the project to reorder
            Project next = new Project(nextID,desc, mName, mRoadmapName);
            Project nextdummy = new Project(currname, nextdesc,mName,mRoadmapName);
            if (nextdesc != null)
            {
                nextdummy.SetName(nextID);
                ReorderProject(nextID, nextdesc, false);
            }
            mDatabase.close();
        }

        //Reload project list.  Useful for reordering projects
        public void ReloadProjects()
        {
            mProjects = new List<Project>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name, Description FROM [dbo].[Project] WHERE BusinessValueName=@BVName AND RoadmapName =@Rname";
            cmd.Parameters.AddWithValue("@BVName", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
     
            mDatabase.connect();
            mReader = mDatabase.executereadparams(cmd);
            while (mReader.Read())
            {
                mProjects.Add(new Project(mReader.GetString(0).ToString(), mReader.GetString(1).ToString(), mName, mRoadmapName));
            }
            mReader.Close();
            mDatabase.close();
        }

        public bool CreateNewProject( Project proj)
        {
            mDatabase.connect();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO [dbo].[Project] (Name, Description, BusinessValueName, RoadmapName) VALUES ('" + proj.GetName() + "', '" + proj.GetDescription() + "','" + proj.GetBusinessValue() + "','" + mRoadmapName + "')";
                cmd.Parameters.AddWithValue("@PName", proj.GetName());
                cmd.Parameters.AddWithValue("@PDescrip", proj.GetDescription());
                cmd.Parameters.AddWithValue("@BVName", proj.GetBusinessValue());
                cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                bool flag = mDatabase.executewriteparam(cmd);
                mDatabase.close();
                mProjects.Add(proj);
                return flag;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}