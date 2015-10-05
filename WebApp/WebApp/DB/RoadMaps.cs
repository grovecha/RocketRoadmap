using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.DB
{
    public class RoadMaps
    {
        //Constructor
        public RoadMaps() { }
        
        /**
        * Creates a list of all the roadmaps
        */
        public List<RoadMap> GetAllMaps()
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name TimelineID FROM [dbo].[RoadMap]");

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
            mReader = mDatabase.executeread("SELECT Name FROM [dbo].[RoadMap] WHERE UserID = '" + username + "'");

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

        /**
        * Create a new user 
        **/
        public bool CreateRoadMap(string name, string description, string userid )
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("INSERT INTO [dbo].[RoadMap] ( Name, Description, Timestamp, UserID ) VALUES ( '" + name + "', '" +  description + "', GETDATE(), '" + userid + "')"))
            {
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public bool DeleteRoadMap(string name)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("DELETE FROM [dbo].[RoadMap] WHERE Name = '" + name + "'"))
            {
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        private WebApp.DB.Database mDatabase = new WebApp.DB.Database();
        private SqlDataReader mReader;
    }
}