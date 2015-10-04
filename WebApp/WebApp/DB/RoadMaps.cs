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
            mReader = mDatabase.executeread("SELECT Name, TimeStamp, Description, UserID, TimelineID FROM [dbo].[RoadMap]");

            List<RoadMap> Maps = new List<RoadMap>();

            while (mReader.Read())
            {
                //create a new roadmap object and add to list
               // RoadMap map = new RoadMap(mReader.GetString(0).ToString(), Convert.ToDateTime(mReader.GetString(1)), mReader.GetString(2).ToString(), mReader.GetString(3).ToString());
               // Maps.Add(map);
            }
            //return list of roadmaps
            return Maps;
        }

        /**
        * Fetches the roadmaps of a single user
        */
        public List<RoadMap> GetUserMaps(string username)
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, TimeStamp, Description, UserID, TimelineID FROM [dbo].[RoadMap] WHERE UserID = " + username);

            List<RoadMap> Maps = new List<RoadMap>();

            while (mReader.Read())
            {
                //create a new roadmap object and add to list
              //  RoadMap map = new RoadMap(mReader.GetString(0).ToString(), Convert.ToDateTime(mReader.GetString(1)), mReader.GetString(2).ToString(), mReader.GetString(3).ToString());
              //  Maps.Add(map);
            }
            //return list of roadmaps
            return Maps;
        }

        private WebApp.DB.Database mDatabase;
        private SqlDataReader mReader;
    }
}