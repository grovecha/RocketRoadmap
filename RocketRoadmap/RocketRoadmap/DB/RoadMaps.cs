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
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name TimelineID FROM [dbo].[RoadMap]";

            mReader = mDatabase.executereadparams(cmd);

            List<RoadMap> Maps = new List<RoadMap>();

            while (mReader.Read())
            {
                //create a new roadmap object and add to list
                RoadMap map = new RoadMap(mReader.GetString(0));
                Maps.Add(map);
            }

            mDatabase.close();
            //return list of roadmaps
            return Maps;
        }

        /**
        * Fetches the roadmaps of a single user
        */
        public List<RoadMap> GetUserMaps(string username)
        {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name FROM [dbo].[RoadMap] WHERE UserID = @User";
            cmd.Parameters.AddWithValue("@User", username);
            mReader = mDatabase.executereadparams(cmd);

            List<RoadMap> maps = new List<RoadMap>();

            while (mReader.Read())
            {
                //create a new roadmap object and add to list
                RoadMap map = new RoadMap(mReader.GetString(0));
                maps.Add(map);
            }

            mDatabase.close();
            //return list of roadmaps
            return maps;
        }

        public List<List<string>> GetUserMapsInfo(string username)
        {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name, Description, Timestamp FROM [dbo].[RoadMap] WHERE UserID =@User";
            cmd.Parameters.AddWithValue("@User", username);
            mReader = mDatabase.executereadparams(cmd);

            List<List<string>> maps = new List<List<string>>();

            while (mReader.Read())
            {
                List<string> temp = new List<string>();
                temp.Add(mReader.GetString(0));
                temp.Add(username);
                temp.Add(mReader.GetString(1));
                temp.Add(mReader.GetDateTime(2).ToString());


                maps.Add(temp);
            }

            mDatabase.close();
            //return list of roadmaps
            return maps;
        }

        public List<List<string>> GetAllMapsInfo()
        {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name, UserID, Description, Timestamp FROM [dbo].[RoadMap]";

            mReader = mDatabase.executereadparams(cmd);

            List<List<string>> maps = new List<List<string>>();

            while (mReader.Read())
            {
                List<string> temp = new List<string>();
                temp.Add(mReader.GetString(0));
                temp.Add(mReader.GetString(1));
                temp.Add(mReader.GetString(2));
                temp.Add(mReader.GetDateTime(3).ToString());


                maps.Add(temp);
            }

            mDatabase.close();
            //return list of roadmaps
            return maps;
        }

        public bool CreateTimeLine(string name, string rname)
        {
            mDatabase.connect();
            bool toReturn = false;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[Timeline] (Name, StartDate, EndDate, RoadmapName ) VALUES ( @Tname, @sdate,@edate,@Rname)";
            cmd.Parameters.AddWithValue("@Tname", name);
            cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
            cmd.Parameters.AddWithValue("@edate", DateTime.Now.AddYears(1));
            cmd.Parameters.AddWithValue("@Rname", rname);
            if (mDatabase.executewriteparam(cmd))
            {
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        /**
        * Create a new roadmap 
        **/
        public bool CreateRoadMap(string name, string description, string userid )
        {
            mDatabase.connect();
            bool toReturn = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[RoadMap] ( Name, Description, Timestamp, UserID ) VALUES ( @Rname, @descrip, GETDATE(), @User)";
            cmd.Parameters.AddWithValue("@Rname", name);
            cmd.Parameters.AddWithValue("@descrip", description);
            cmd.Parameters.AddWithValue("@User", userid);
            if (mDatabase.executewriteparam(cmd))
            {
                //create a new timeline
                //CreateTimeLine(name, name);

                toReturn = true;
            }
            mDatabase.close();

            return toReturn;
        }

        public bool DeleteRoadMap(string name)
        {
            mDatabase.connect();
            bool toReturn = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM [dbo].[RoadMap] WHERE Name =@name";
            cmd.Parameters.AddWithValue("@name", name);
            if (mDatabase.executewriteparam(cmd))
            {
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public List<RoadMap> Search( string key )
        {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name FROM [dbo].[RoadMap] WHERE UserID LIKE @key  OR Name LIKE @key OR Description LIKE @key";
            cmd.Parameters.AddWithValue("@key", key);
            mReader = mDatabase.executereadparams(cmd);

            List<RoadMap> maps = new List<RoadMap>();

            while (mReader.Read())
            {
                //create a new roadmap object and add to list
                RoadMap map = new RoadMap(mReader.GetString(0));
                maps.Add(map);
            }

            mDatabase.close();
            //return list of roadmaps
            return maps;
        }
        public void Test()
        {

        }

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;
    }
}