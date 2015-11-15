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
        //Business value the project belongs to
        private string mBusinessValue;
        //Roadmap the project belongs to
        private string mRoadmapName;
        //Risk string
        private string mRiskString;
        //Width of project
        private int mWidth=160;
        //Left point of project
        private int mLeft=0;

        //Dependants (NON PROJECTS)
        private List<string> mDependantString = new List<string>();
        //Links this project owns
        private List<Link> mLinks = new List<Link>();
        //Dependences (PROJECTS)
        private List<Project> mDependencies = new List<Project>();

        //private RocketRoadmap.DB.Database mDatabase =  new Database();
        //private SqlDataReader mReader;

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
                    cmd4.CommandText = "SELECT Width, StartPosition FROM [dbo].[Project] WHERE Name=@name AND BusinessValueName=@BVName AND RoadmapName=@Rname";
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
                            try { mWidth = Reader.GetInt32(0); }
                            catch (Exception ex) { }
                            try { mLeft = Reader.GetInt32(1); }
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
        public bool SetWidth(int newwidth)
        {
            mWidth = newwidth;
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[Project] SET Width=@width WHERE Name=@Pname AND RoadmapName=@Rname and BusinessValueName=@BVName";
                    cmd.Parameters.AddWithValue("@width", newwidth);
                    cmd.Parameters.AddWithValue("@Pname", mName);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
                    cmd.Connection = conn;
                    conn.Open();
                    flag= cmd.ExecuteNonQuery()!=0;
                    conn.Close();
                }
            }
            return flag;
        }
        public int GetWidth() { return mWidth; }

        public bool SetLeft(int left)
        {
            mLeft = left;
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[Project] SET Left=@left WHERE Name=@Pname AND RoadmapName=@Rname and BusinessValueName=@BVName";
                    cmd.Parameters.AddWithValue("@left", left);
                    cmd.Parameters.AddWithValue("@Pname", mName);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
                    cmd.Connection = conn;
                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }
            return flag;
        }
        public int GetLeft() { return mLeft; }

        public bool SetName(string name) {
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[Project] SET Name=@Sname WHERE Name=@Pname AND RoadmapName=@Rname AND BusinessValueName=@BVName";
                    cmd.Parameters.AddWithValue("@Sname", name);
                    cmd.Parameters.AddWithValue("@Pname", mName);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }
            mName = name;

            return flag;
        }
        public string GetName() {
            return mName;
        }

        public bool SetDescription(string description) {
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[Project] SET Description=@descrip WHERE Name=@Pname AND RoadmapName=@Rname AND BusinessValueName=@BVName";
                    cmd.Parameters.AddWithValue("@descrip", description);
                    cmd.Parameters.AddWithValue("@Pname", mName);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0; ;
                    conn.Close();
                }
            }
            mDescription = description;
            return flag;
        }
        public string GetDescription() {
            return mDescription;
        }


        public bool SetModalDescription(string description)
        {
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[Project] SET ModalDescription=@descrip WHERE Name=@PName AND RoadmapName=@Rname AND BusinessValueName=@BVName";
                    cmd.Parameters.AddWithValue("@descrip", description);
                    cmd.Parameters.AddWithValue("@PName", mName);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                 }
            }
            mModalDescription = description;
            return flag;
        }
        public string GetModalDescription()
        {
            return mModalDescription;
        }


        public bool UpdateDependantStrings(List<string> Dependants)
        {

            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd1 = new SqlCommand())
                {
                    cmd1.CommandText = "DELETE FROM [dbo].[Dependents_string] WHERE ProjectName=@Pname AND RoadmapName=@Rname";
                    cmd1.Parameters.AddWithValue("@Pname", mName);
                    cmd1.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd1.Parameters.AddWithValue("@BVName", mBusinessValue);
                    cmd1.Connection = conn;

                    conn.Open();
                    flag = cmd1.ExecuteNonQuery() != 0;
                    conn.Close();
                }
                //Add all back in
                foreach (string element in Dependants)
                {
                    using (SqlCommand cmd2 = new SqlCommand())
                    {
                        cmd2.CommandText = "INSERT INTO [dbo].[Dependents_string](ProjectName, DependantString, RoadmapName) VALUES (@Pname,@element,@Rname)";
                        cmd2.Parameters.AddWithValue("@Pname", mName);
                        cmd2.Parameters.AddWithValue("@element", element);
                        cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);
                        cmd2.Connection = conn;

                        conn.Open();
                        flag = cmd2.ExecuteNonQuery() != 0;
                        conn.Close();
                    }
                }
            }
            return flag;

        }

        public List<string> GetDependantStrings()
        {
            return mDependantString;
        }

        public bool SetBusinessValue(string businessvalue) {
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[Project] SET BusinessValueName=@bname WHERE Name=@Pname AND RoadmapName=@Rname AND BusinessValueName=@BVName";
                    cmd.Parameters.AddWithValue("@bname", businessvalue);
                    cmd.Parameters.AddWithValue("@Pname", mName);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }
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
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO [dbo].[Link] (Description, ProjectName, Address, RoadmapName) VALUES (@descrip,@Pname, @addr,@Rname)";
                    cmd.Parameters.AddWithValue("@descrip", link.GetDescription());
                    cmd.Parameters.AddWithValue("@Pname", mName);
                    cmd.Parameters.AddWithValue("@addr", link.GetLink());
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }
            return flag;
        }

        public bool DeleteLink(Link link)
        {
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "DELETE [dbo].[Links] WHERE RoadmapName = @Rname AND ProjectName = @Pname AND Address = @addr";
                    cmd.Parameters.AddWithValue("@Pname", mName);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Parameters.AddWithValue("@addr", link.GetLink());
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }
            mLinks.Remove(link);

            return flag;
        }

        //Create issue
        public bool CreateIssue(Issue i)
        {
           bool flag;
           using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
           {
               using (SqlCommand cmd = new SqlCommand())
               {
                   cmd.CommandText = "INSERT INTO [dbo].[Issues] (Description, ProjectName, RoadmapName) VALUES (@descrip,@Pname, @Rname)";
                   cmd.Parameters.AddWithValue("@descrip", i.GetDescription());
                   cmd.Parameters.AddWithValue("@Pname", mName);
                   cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                   cmd.Connection = conn;

                   conn.Open();
                   flag = cmd.ExecuteNonQuery() != 0;
                   conn.Close();
               }
           }
            return flag;
        }

        //Create/delete dependents in list and DB
        public bool CreateDependant(Project dependant)
        {
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO [dbo].[Dependents] (ProjectName, DependantName, Description, RoadmapName) VALUES (@Pname,@name,@descrip,@Rname)";
                    cmd.Parameters.AddWithValue("@Pname", mName);
                    cmd.Parameters.AddWithValue("@name", dependant.GetName());
                    cmd.Parameters.AddWithValue("@descrip", dependant.GetDescription());
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }
            mDependencies.Add(dependant);

            return flag;
        }

        public bool DeleteDependant(Project dependant)
        {
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "DELETE [dbo].[Dependents] WHERE RoadmapName = @Rname AND ProjectName = @Pname AND DependantName = @name";
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Parameters.AddWithValue("@Pname", mName);
                    cmd.Parameters.AddWithValue("@name", dependant.GetName());
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }
            mDependencies.Remove(dependant);

            return flag;
        }

        public bool SetProjectRisks(string risks)
        {
            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[Project] SET Risks=@risks WHERE Name=@Pname AND RoadmapName=@Rname AND BusinessValueName =@BVName";
                    cmd.Parameters.AddWithValue("@risks", risks);
                    cmd.Parameters.AddWithValue("@Pname", mName);
                    cmd.Parameters.AddWithValue("@BVName", mBusinessValue);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }
            mRiskString = risks;
            return flag;
        }
        public string GetProjectRisks()
        {
            return mRiskString;
        }

    }
}