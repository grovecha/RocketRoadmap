using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    //Roadmap class- holds an entire roadmap
    public class RoadMap
    {

        //CONSTRUCTOR
        public RoadMap( string name )
        {
            mName = name;
            string UID;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Timestamp, Description, UserID, color_dep_of, color_dep_on FROM [dbo].[Roadmap] WHERE Name =@Rname";
                    cmd.Parameters.AddWithValue("@Rname", name);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        Reader.Read();

                        mTimeStamp = Reader.GetDateTime(0);
                        mDescription = Reader.GetString(1);
                        UID = Reader.GetString(2);

                        try
                        {
                            mColor_of = Reader.GetString(3);
                            mColor_on = Reader.GetString(4);
                        }
                        catch
                        {
                            mColor_of = "#FFFFFFFF";
                            mColor_of = "#BBBBBBBB";
                        }
                    }
                }

                using (SqlCommand cmd1 = new SqlCommand())
                {
                    cmd1.CommandText = "SELECT Name, Email, Password FROM [dbo].[User] WHERE ID = @User";
                    cmd1.Parameters.AddWithValue("@User", UID);
                    cmd1.Connection = conn;

                    using (SqlDataReader Reader = cmd1.ExecuteReader())
                    {
                        Reader.Read();

                        mUser = new User(Reader.GetString(0), UID, Reader.GetString(1), Reader.GetString(2));
                    }
                }


                    mTimeline = new TimeLine(mName);


                //Get the StrategyPoints
                using (SqlCommand cmd3 = new SqlCommand())
                {
                    cmd3.CommandText = "SELECT Name, Description FROM [dbo].[StrategyPoint] WHERE RoadmapName =@Rname ORDER BY SORT ASC";
                    cmd3.Parameters.AddWithValue("@Rname", mName);
                    cmd3.Connection = conn;

                    using (SqlDataReader Reader = cmd3.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            StrategyPoint sp = new StrategyPoint(Reader.GetString(0), Reader.GetString(1), name);
                            mStrategyPoints.Add(sp);
                        }
                    }
                }
                conn.Close();    
            }
        }

        //Creates timeline with Name=name
        public bool CreateTimeLine(string name)
        {
            bool toReturn;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO [dbo].[Timeline] (Name, StartDate, EndDate, RoadmapName ) VALUES ( @Tname, @sdate, @edate, @Rname)";
                    cmd.Parameters.AddWithValue("@Tname", name);
                    cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@edate", DateTime.Now.AddYears(1));
                    cmd.Parameters.AddWithValue("@Rname", mName);
                    cmd.Connection = conn;
                    toReturn = false;
                   
                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        mTimeline = new TimeLine(name);   
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        //Add strategy point to roadmap
        public bool AddStrategyPoint(StrategyPoint point)
        {
            mStrategyPoints.Add(point);
            bool flag;

            int order = Convert.ToInt32(point.GetName().Substring(8, (point.GetName().Length) - 8));
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO [dbo].[StrategyPoint] ([Name],[Description],[RoadmapName],[Sort]) VALUES (@Sname,@descrip,@Rname,@sort)";
                    cmd.Parameters.AddWithValue("@Sname", point.GetName());
                    cmd.Parameters.AddWithValue("@descrip", point.GetDescription());
                    cmd.Parameters.AddWithValue("@Rname", mName);
                    cmd.Parameters.AddWithValue("@sort", order);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }

            return flag;

        }

        //Delete timeline
        public bool DeleteTimeLine()
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "DELETE FROM [dbo].[Timeline] WHERE RoadmapName = @Rname";
                    cmd.Parameters.AddWithValue("@Rname", mName);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        mTimeline.ClearTicks();
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        //Edit roadmap name
           public bool EditName( string newname )
           {
               bool toReturn = false;
               using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
               {
                   using (SqlCommand cmd = new SqlCommand())
                   {
                       cmd.CommandText = "UPDATE [dbo].[Roadmap] SET Name = @Rname WHERE Name =@oldname";
                       cmd.Parameters.AddWithValue("@Rname", newname);
                       cmd.Parameters.AddWithValue("@oldname", mName);
                       cmd.Connection = conn;

                       conn.Open();
                       if (cmd.ExecuteNonQuery()!=0)
                       {
                           mName = newname;
                           toReturn = true;
                       }
                       conn.Close();
                   }
               }
               return toReturn;
           }

        //Edit roadmap description
        public bool EditDescription(string desc)
        {
            bool toReturn = false;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[Roadmap] SET Description = @descrip WHERE Name = @Rname";
                    cmd.Parameters.AddWithValue("@descrip", desc);
                    cmd.Parameters.AddWithValue("@Rname", mName);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        mDescription = desc;
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        //Get a strategy point from the list of points
        public StrategyPoint GetPoint(string id)
        {

            
            foreach(StrategyPoint p in mStrategyPoints)
            {
                if(p.GetName()==id)
                {
                    return p;
                }
            }
            // point doesn't exist, we gotta problem here cap'n
            return null;
        }

        //Reorders strategy points to insert one in the middle
        public void ReOrderStrategyPoint(string currname,string desc, bool isFirst)
        {
            //Get current strategy point
            StrategyPoint current = new StrategyPoint(currname, desc, mName);
            int index = (int)Char.GetNumericValue(currname[8]) + 1;
            string nextID = "StratBox"+index.ToString();
            string nextdesc=null;

            //Move next one up
            string selectname = null;
            if (isFirst) selectname = currname;
            else selectname = nextID;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Description FROM [dbo].[StrategyPoint] WHERE Name=@sname AND RoadmapName=@Rname";
                    cmd.Parameters.AddWithValue("@sname", selectname);
                    cmd.Parameters.AddWithValue("@Rname", mName);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        if (Reader.HasRows)
                        {
                            Reader.Read();
                            nextdesc = Reader.GetString(0);
                        }
                        Reader.Close();
                    }
                    conn.Close();
                }
            }

            //Continue moving strat points up one
            StrategyPoint next = new StrategyPoint(nextID, nextdesc, mName);
            StrategyPoint nextdummy = new StrategyPoint(currname, nextdesc, mName);
            if (nextdesc != null)
            {
                nextdummy.EditName(nextID);
                ReOrderStrategyPoint(nextID, nextdesc, false);
            }
        }

        //Reloads list of strategy points
        public void ReloadStrategyPoints()
        {
            mStrategyPoints = new List<StrategyPoint>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name, Description FROM [dbo].[StrategyPoint] WHERE RoadmapName = @Rname ORDER BY NAME ASC";
                    cmd.Parameters.AddWithValue("@Rname", mName);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            StrategyPoint sp = new StrategyPoint(Reader.GetString(0), Reader.GetString(1), mName);
                            mStrategyPoints.Add(sp);
                        }
                    }
                    conn.Close();
                }
            }
        }

        //Gets all projects for a roadmap
        public List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name, Description, BusinessValueName, RoadmapName FROM [dbo].[Project] WHERE RoadmapName = @Rname ORDER BY NAME ASC";
                    cmd.Parameters.AddWithValue("@Rname", mName);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            Project temp = new Project(Reader.GetString(0), Reader.GetString(1), Reader.GetString(2), Reader.GetString(3));
                            projects.Add(temp);
                        }
                        Reader.Close();
                    }
                    conn.Close();
                }
            }
            return projects;
        }

        //Delete a strategy point
        public bool DeleteStrategyPoint(string name)
        {
            foreach (StrategyPoint sp in mStrategyPoints.ToList())
            {
                if (sp.GetName() == name) mStrategyPoints.Remove(sp);
            }

            bool flag;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "DELETE FROM [dbo].[StrategyPoint] WHERE RoadmapName =@Rname AND Name = @sname";
                    cmd.Parameters.AddWithValue("@Rname", mName);
                    cmd.Parameters.AddWithValue("@sname", name);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
                    using (SqlCommand cmd2 = new SqlCommand())
                    {
                        cmd2.CommandText = "DELETE FROM [dbo].[SP_BV_Crosswalk] WHERE RoadmapName =@Rname AND StrategyPointName = @sname";
                        cmd2.Parameters.AddWithValue("@Rname", mName);
                        cmd2.Parameters.AddWithValue("@sname", name);
                        cmd2.Connection = conn;

                        conn.Open();
                        flag = cmd2.ExecuteNonQuery() != 0;
                        conn.Close();
                    }
                }

            int index = (int)Char.GetNumericValue(name[8]);

            foreach (StrategyPoint sp in mStrategyPoints.ToList())
            {
                if ((int)Char.GetNumericValue(sp.GetName()[8]) > index)
                {
                    int newindex = (int)Char.GetNumericValue(sp.GetName()[8]) - 1;
                    string newname = sp.GetName().Substring(0, 8) + newindex.ToString();
                    sp.EditName(newname);
                }
            }

            return flag;
        }

        public bool SetDependenyColor(string on, string of)
        {
            mColor_of = of;
            mColor_on = on;
            bool flag;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[Roadmap] SET [color_dep_of]= @on ,[color_dep_on]= @of WHERE Name=@Rname";
                    cmd.Parameters.AddWithValue("@of", of);
                    cmd.Parameters.AddWithValue("@on", on);
                    cmd.Parameters.AddWithValue("@Rname", mName);
                    cmd.Connection = conn;

                    conn.Open();
                    flag = cmd.ExecuteNonQuery() != 0;
                    conn.Close();
                }
            }

            return flag;
        }

        public string GetDependeny_of_Color(string on, string of)
        {
            return mColor_of;
        }

        public string GetDependeny_on_Color(string on, string of)
        {
            return mColor_on;
        }

        //Getters if needed
        public string GetName() { return mName; }
        public void SetName(string name) { mName = name; }
        public DateTime GetTimeStamp() { return mTimeStamp; }
        public string GetDecription() { return mDescription; }
        public User GetUser() { return mUser; }
        public TimeLine GetTimeline() { return mTimeline; }
        public List<StrategyPoint> GetStrategyPoints() { return mStrategyPoints; }

        //Roadmap name
        private string mName;
        //Last edit
        private DateTime mTimeStamp;
        private string mDescription;
        //Owner of roadmap
        private User mUser;
        private TimeLine mTimeline;
        private List<StrategyPoint> mStrategyPoints = new List<StrategyPoint>();
        private string mColor_on;
        private string mColor_of;

    //    private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
    //    private SqlDataReader mReader;
    
    }
}

