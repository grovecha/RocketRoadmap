using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    public class RoadMaps
    {

        private static RoadMaps instance = null;
  
        private static RoadMaps Get
        {
            get
            {
                if(instance== null)
                {
                    instance = new RoadMaps();
                }
                return instance;
            }
        }
        //Constructor
        public RoadMaps() { }
        
        /**
        * Creates a list of all the roadmaps
        */
        public List<RoadMap> GetAllMaps()
        {
            List<RoadMap> Maps = new List<RoadMap>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name TimelineID FROM [dbo].[RoadMap]";
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            //create a new roadmap object and add to list
                            RoadMap map = new RoadMap(Reader.GetString(0));
                            Maps.Add(map);
                        }
                    }
                    conn.Close();
                }
            }
            //return list of roadmaps
            return Maps;
        }

        /**
        * Fetches the roadmaps of a single user
        */
        public List<RoadMap> GetUserMaps(string username)
        {
            List<RoadMap> maps = new List<RoadMap>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name FROM [dbo].[RoadMap] WHERE UserID = @User";
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {

                        while (Reader.Read())
                        {
                            //create a new roadmap object and add to list
                            RoadMap map = new RoadMap(Reader.GetString(0));
                            maps.Add(map);
                        }
                    }
                    conn.Close();
                }
            }
            //return list of roadmaps
            return maps;
        }

        public List<List<string>> GetUserMapsInfo(string username)
        {
            List<List<string>> maps = new List<List<string>>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name, Description, Timestamp FROM [dbo].[RoadMap] WHERE UserID =@User";
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            List<string> temp = new List<string>();
                            temp.Add(Reader.GetString(0));
                            temp.Add(username);
                            temp.Add(Reader.GetString(1));
                            temp.Add(Reader.GetDateTime(2).ToString());

                            maps.Add(temp);
                        }
                    }
                    conn.Close();
                }
            }
            //return list of roadmaps
            return maps;
        }

        public List<List<string>> GetAllMapsInfo()
        {
            List<List<string>> maps = new List<List<string>>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name, UserID, Description, Timestamp FROM [dbo].[RoadMap]";
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            List<string> temp = new List<string>();
                            temp.Add(Reader.GetString(0));
                            temp.Add(Reader.GetString(1));
                            temp.Add(Reader.GetString(2));
                            temp.Add(Reader.GetDateTime(3).ToString());


                            maps.Add(temp);
                        }
                    }
                    conn.Close();
                }
            }
            //return list of roadmaps
            return maps;
        }

        public bool CreateTimeLine(string name, string rname)
        {
            bool toReturn = false;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO [dbo].[Timeline] (Name, StartDate, EndDate, RoadmapName ) VALUES ( @Tname, @sdate,@edate,@Rname)";
                    cmd.Parameters.AddWithValue("@Tname", name);
                    cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@edate", DateTime.Now.AddYears(1));
                    cmd.Parameters.AddWithValue("@Rname", rname);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        /**
        * Create a new roadmap 
        **/
        public bool CreateRoadMap(string name, string description, string userid )
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO [dbo].[RoadMap] ( Name, Description, Timestamp, UserID ) VALUES ( @Rname, @descrip, GETDATE(), @User)";
                    cmd.Parameters.AddWithValue("@Rname", name);
                    cmd.Parameters.AddWithValue("@descrip", description);
                    cmd.Parameters.AddWithValue("@User", userid);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        //create a new timeline
                        //CreateTimeLine(name, name);

                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        public bool DeleteRoadMap(string name)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "DELETE FROM [dbo].[RoadMap] WHERE Name =@name";
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        public List<List<string>> Search( string key )
        {
            List<List<string>> maps = new List<List<string>>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SELECT Name, UserID, Description, Timestamp FROM [dbo].[RoadMap] WHERE UserID LIKE @key  OR Name LIKE @key OR Description LIKE @key";
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            List<string> temp = new List<string>();
                            temp.Add(Reader.GetString(0));
                            temp.Add(Reader.GetString(1));
                            temp.Add(Reader.GetString(2));
                            temp.Add(Reader.GetDateTime(3).ToString());

                            maps.Add(temp);
                        }
                    }
                    conn.Close();
                }
            }
            //return list of roadmaps
            return maps;
        }
        public void Test()
        {

        }

       // private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
       // private SqlDataReader mReader;
    }
}