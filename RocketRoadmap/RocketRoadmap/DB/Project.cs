using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    //Project class
    public class Project
    {
        //Name of Projecrt
        private string mName;
        //Project Description
        private string mDescription;
        //Modal Description
        private string mModalDescription;
        //Start date of project (For timeline)
        private DateTime mStartDate;
        //End date of project (For timeline)
        private DateTime mEndDate;
        //Business value the project belongs to
        private string mBusinessValue;
        //Roadmap the project belongs to
        private string mRoadmapName;
        //Risk string
        private string mRiskString;

        //Dependants (NON PROJECTS)
        private List<string> mDependantString = new List<string>();
        //Links this project owns
        private List<Link> mLinks = new List<Link>();
        //Dependences (PROJECTS)
        private List<Project> mDependencies = new List<Project>();

        private RocketRoadmap.DB.Database mDatabase =  new Database();
        private SqlDataReader mReader;

        public Project(string name, string description, string businessvalue, string rname)
        {
            //Set all member variables
            mName = name;
            mDescription = description;
            mBusinessValue = businessvalue;
            mRoadmapName = rname;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
                {
                    //Get modal description in DB
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT ModalDescription FROM [dbo].[Project] WHERE Name=@name AND RoadmapName =@Rname AND BusinessValueName=@BVName";
                        cmd.Parameters.AddWithValue("@name", mName);
                        cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                        cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                mModalDescription = (Reader.GetString(0).ToString());
                            }
                        }
                    }
                }
            }
            catch { ;  }

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                //Grab risks this project owns
                using (SqlCommand cmd1 = new SqlCommand())
                {
                    cmd1.CommandText = "SELECT Risks FROM [dbo].[Project] WHERE Name='" + mName + "' AND RoadmapName ='" + rname + "' AND BusinessValueName='" + mBusinessValue + "'";
                    cmd1.Parameters.AddWithValue("@name", mName);
                    cmd1.Parameters.AddWithValue("@BVName", mBusinessValue);
                    cmd1.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd1.Connection = conn;
                    
                    conn.Open();
                    using (SqlDataReader Reader = cmd1.ExecuteReader())
                    {
                        while (Reader.Read())
                        {

                            try
                            {
                                mRiskString = Reader.GetString(0).ToString();
                            }
                            catch (Exception ex) { }
                        }
                        Reader.Close();
                    }
                    conn.Close();
                }
                
                //Get links this project owns
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "SELECT Description, Address FROM [dbo].[Link] WHERE ProjectName=@Pname AND RoadmapName =@Rname";
                    cmd2.Parameters.AddWithValue("@Pname", mName);
                    cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd2.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd2.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            mLinks.Add(new Link(Reader.GetString(0).ToString(), mName, Reader.GetString(1).ToString(), mRoadmapName));
                        }
                        Reader.Close();
                    }
                    conn.Close();
                }

                //Grab project dependencies
                using (SqlCommand cmd3 = new SqlCommand())
                {
                    cmd3.CommandText = "SELECT P.Name, P.Description, P.BusinessValueName FROM(SELECT DependantName, RoadmapName, ProjectName FROM Dependents AS D WHERE(ProjectName = @Pname) AND(RoadmapName = @Rname)) AS S INNER JOIN Project AS P ON S.DependantName = P.Name AND P.RoadmapName = S.RoadmapName ";
                    cmd3.Parameters.AddWithValue("@Pname", mName);
                    cmd3.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd3.Connection = conn;
                    conn.Open();

                    using (SqlDataReader Reader = cmd3.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            mDependencies.Add(new Project(Reader.GetString(0).ToString(), Reader.GetString(1).ToString(), Reader.GetString(2).ToString(), mRoadmapName));
                        }
                        Reader.Close();
                    }
                    conn.Close();
                }

                using (SqlCommand cmd4 = new SqlCommand())
                {
                    cmd4.CommandText = "SELECT StartDate,EndDate FROM [dbo].[Project] WHERE Name=@name AND BusinessValueName=@BVName AND RoadmapName=@Rname";
                    cmd4.Parameters.AddWithValue("@name", mName);
                    cmd4.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd4.Parameters.AddWithValue("@BVName", mBusinessValue);
                    cmd4.Connection = conn;
                   
                    conn.Open();
                    using (SqlDataReader Reader = cmd4.ExecuteReader())
                    {
                        if (Reader.HasRows)
                        {
                            Reader.Read();
                            try { mStartDate = Reader.GetDateTime(0); }
                            catch (Exception ex) { }
                            try { mEndDate = Reader.GetDateTime(1); }
                            catch (Exception ex) { }
                        }
                        Reader.Close();
                    }
                    conn.Close();
                }

                //Get Dependants NON PROJECT
                using (SqlCommand cmd5 = new SqlCommand())
                {
                    cmd5.CommandText = "SELECT DependantString FROM [dbo].[Dependents_string] WHERE ProjectName=@Pname AND RoadmapName =@Rname";
                    cmd5.Parameters.AddWithValue("@Pname", mName);
                    cmd5.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd5.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader =cmd5.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            mDependantString.Add(Reader.GetString(0).ToString());
                        }
                        Reader.Close();
                    }
                    conn.Close();
                }
            }
        }

        #region Getter's and Setters
        public bool SetName(string name) {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Project] SET Name=@Sname WHERE Name=@Pname AND RoadmapName=@Rname AND BusinessValueName=@BVName";
            cmd.Parameters.AddWithValue("@Sname", name);
            cmd.Parameters.AddWithValue("@Pname", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            cmd.Parameters.AddWithValue("@BVName", mBusinessValue);

            bool flag=mDatabase.executewriteparam(cmd);
            mName = name;
            mDatabase.close();

            return flag;
        }
        public string GetName() {
            return mName;
        }

        public bool SetDescription(string description) {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Project] SET Description=@descrip WHERE Name=@Pname AND RoadmapName=@Rname AND BusinessValueName=@BVName";
            cmd.Parameters.AddWithValue("@descrip", description);
            cmd.Parameters.AddWithValue("@Pname", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
            bool flag = mDatabase.executewriteparam(cmd);
            mDatabase.close();
            mDescription = description;
            return flag;
        }
        public string GetDescription() {
            return mDescription;
        }


        public bool SetModalDescription(string description)
        {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Project] SET ModalDescription=@descrip WHERE Name=@PName AND RoadmapName=@Rname AND BusinessValueName=@BVName";
            cmd.Parameters.AddWithValue("@descrip", description);
            cmd.Parameters.AddWithValue("@PName", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
            bool flag = mDatabase.executewriteparam(cmd);
            mDatabase.close();
            mModalDescription = description;
            return flag;
        }
        public string GetModalDescription()
        {
            return mModalDescription;
        }

        public bool SetStartDate(DateTime startdate) {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Project] SET StartDate=@sdate WHERE Name=@PName AND RoadmapName=@Rname AND BusinessValueName=@BVName";
            cmd.Parameters.AddWithValue("@sdate", startdate);
            cmd.Parameters.AddWithValue("@PName", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
            bool flag= mDatabase.executewriteparam(cmd);
            mDatabase.close();
            mStartDate = startdate;
            return flag;
        }

        public bool UpdateDependantStrings(List<string> Dependants)
        {

                //Delete all dependants
                mDatabase.connect();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "DELETE FROM [dbo].[Dependents_string] WHERE ProjectName=@Pname AND RoadmapName=@Rname";
                cmd1.Parameters.AddWithValue("@Pname", mName);
                cmd1.Parameters.AddWithValue("@Rname", mRoadmapName);
                cmd1.Parameters.AddWithValue("@BVName", mBusinessValue);
                bool flag = mDatabase.executewriteparam(cmd1);

                //Add all back in
                foreach (string element in Dependants)
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "INSERT INTO [dbo].[Dependents_string](ProjectName, DependantString, RoadmapName) VALUES (@Pname,@element,@Rname)";
                    cmd2.Parameters.AddWithValue("@Pname", mName);
                    cmd2.Parameters.AddWithValue("@element", element);
                    cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);

                    flag = mDatabase.executewriteparam(cmd2);
                }


            mDatabase.close();
            return flag;

        }

        public List<string> GetDependantStrings()
        {
            return mDependantString;
        }

        public DateTime GetStartDate() {
            return mStartDate;
        }

        public bool SetEndDate(DateTime enddate) {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Project] SET EndDate=@edate WHERE Name=@Pname AND RoadmapName=@Rname AND BusinessValueName=@BVName";
            cmd.Parameters.AddWithValue("@edate", enddate);
            cmd.Parameters.AddWithValue("@Pname", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            cmd.Parameters.AddWithValue("@BVName", mBusinessValue);

            bool flag=mDatabase.executewriteparam(cmd);
            mDatabase.close();
            mEndDate = enddate;
            return flag;
        }
        public DateTime GetEndDate() {
            return mEndDate;
        }

        public bool SetBusinessValue(string businessvalue) {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Project] SET BusinessValueName=@bname WHERE Name=@Pname AND RoadmapName=@Rname AND BusinessValueName=@BVName";
            cmd.Parameters.AddWithValue("@bname", businessvalue);
            cmd.Parameters.AddWithValue("@Pname", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
            bool flag=mDatabase.executewriteparam(cmd);
            mDatabase.close();
            mBusinessValue = businessvalue;
            return flag;
        }
        public string GetBusinessValue() {
            return mBusinessValue;
        }

        public List<Link> GetLinks() { return mLinks; }
        //public List<Issue> GetIssues() { return mIssues; }
        public List<Project> GetDependencies() { return mDependencies; }
        #endregion
        
        //Create and delete links in list and DB
        public bool CreateLink(Link link)
        {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[Link] (Description, ProjectName, Address, RoadmapName) VALUES (@descrip,@Pname, @addr,@Rname)";
            cmd.Parameters.AddWithValue("@descrip", link.GetDescription());
            cmd.Parameters.AddWithValue("@Pname", mName);
            cmd.Parameters.AddWithValue("@addr", link.GetLink());
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag = mDatabase.executewriteparam(cmd);
            mDatabase.close();
            return flag;
        }

        public bool DeleteLink(Link link)
        {
            //assume already created
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE [dbo].[Links] WHERE RoadmapName = @Rname AND ProjectName = @Pname AND Address = @addr";
            cmd.Parameters.AddWithValue("@Pname", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            cmd.Parameters.AddWithValue("@addr", link.GetLink());
            bool flag = mDatabase.executewriteparam(cmd);

            mLinks.Remove(link);

            mDatabase.close();
            return flag;
        }

        //Create issue
        public bool CreateIssue(Issue i)
        {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[Issues] (Description, ProjectName, RoadmapName) VALUES (@descrip,@Pname, @Rname)";
            cmd.Parameters.AddWithValue("@descrip", i.GetDescription());
            cmd.Parameters.AddWithValue("@Pname", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag = mDatabase.executewriteparam(cmd);
            mDatabase.close();
            
            return flag;
        }

        //Create/delete dependents in list and DB
        public bool CreateDependant(Project dependant)
        {
            //assume already created
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[Dependents] (ProjectName, DependantName, Description, RoadmapName) VALUES (@Pname,@name,@descrip,@Rname)";
            cmd.Parameters.AddWithValue("@Pname", mName);
            cmd.Parameters.AddWithValue("@name", dependant.GetName());
            cmd.Parameters.AddWithValue("@descrip", dependant.GetDescription());
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag = mDatabase.executewriteparam(cmd);

            mDependencies.Add(dependant);

            mDatabase.close();
            return flag;
        }

        public bool DeleteDependant(Project dependant)
        {
            //assume already created
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE [dbo].[Dependents] WHERE RoadmapName = @Rname AND ProjectName = @Pname AND DependantName = @name";
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            cmd.Parameters.AddWithValue("@Pname", mName);
            cmd.Parameters.AddWithValue("@name", dependant.GetName());
            bool flag = mDatabase.executewriteparam(cmd);

            mDependencies.Remove(dependant);

            mDatabase.close();
            return flag;
        }

        public bool SetProjectRisks(string risks)
        {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Project] SET Risks=@risks WHERE Name=@Pname AND RoadmapName=@Rname AND BusinessValueName =@BVName";
            cmd.Parameters.AddWithValue("@risks", risks);
            cmd.Parameters.AddWithValue("@Pname", mName);
            cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag = mDatabase.executewriteparam(cmd);
            mDatabase.close();
            mRiskString = risks;
            return flag;
        }
        public string GetProjectRisks()
        {
            return mRiskString;
        }
        public string QuickDBTest()
        {
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name FROM [dbo].[Project] WHERE Name=@Name AND BusinessValueName=@BVName AND RoadmapName=@RName";
            cmd.Parameters.AddWithValue("@Name", "Tested");
            cmd.Parameters.AddWithValue("@BVName", "test");
            cmd.Parameters.AddWithValue("@Rname", "Test");
            mDatabase.connect();
            mReader = mDatabase.executereadparams(cmd);
            if (mReader.HasRows)
            {
                mReader.Read();
                string temp = mReader.GetString(0).ToString();
                mReader.Close();
                return temp;
            }
            mDatabase.close();
            return "";
        }

    }
}