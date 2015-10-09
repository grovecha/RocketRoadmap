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

        public bool CreateTimeLine(string name, string rname)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("INSERT INTO [dbo].[Timeline] (Name, StartDate, EndDate, RoadmapName ) VALUES ( '" + name + "', '" + DateTime.Now + "'" + ',' + "'" + DateTime.Now.AddYears(1) + "'" + ',' + "'" + rname + "')"))
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

            if (mDatabase.executewrite("INSERT INTO [dbo].[RoadMap] ( Name, Description, Timestamp, UserID ) VALUES ( '" + name + "', '" +  description + "', GETDATE(), '" + userid + "')"))
            {
                //create a new timeline
                CreateTimeLine(name, name);

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

        public List<RoadMap> Search( string key )
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name FROM [dbo].[RoadMap] WHERE UserID LIKE '" + key + "', OR Name LIKE '" + key + "', OR Description LIKE '" + key + '"' );

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

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;
    }
}