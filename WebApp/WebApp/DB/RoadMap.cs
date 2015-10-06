using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.DB
{
    public class RoadMap
    {
        public RoadMap(string name )
        {
            mName = name;

            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Timestamp, Description, UserID FROM [dbo].[Roadmap] WHERE Name = '" + name + "'");
            mReader.Read();

            mTimeStamp = mReader.GetDateTime(0);
            mDescription = mReader.GetString(1);
            mUserID = mReader.GetString(2);

            mDatabase.close();

            mTimeline = new TimeLine(mName);

            //Get the StrategyPoints
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, Description FROM [dbo].[StrategyPoint] WHERE RoadmapName = '" + name + "'");
            while (mReader.Read())
            {
                StrategyPoint sp = new StrategyPoint(mReader.GetString(0), mReader.GetString(1));
                mStrategyPoints.Add(sp);
            }
            mDatabase.close();
        }

        //Getters if needed
        public string GetName() { return mName; }
        public DateTime GetTimeStamp() { return mTimeStamp; }
        public string GetDecription() { return mDescription; }
        public string GetUserID() { return mUserID; }
        public TimeLine GetTimeline() { return mTimeline; }
        public List<StrategyPoint> GetStrategyPoints() { return mStrategyPoints; }

        private string mName;
        private DateTime mTimeStamp;
        private string mDescription;
        private string mUserID;
        private TimeLine mTimeline;
        private List<StrategyPoint> mStrategyPoints = new List<StrategyPoint>();

        private WebApp.DB.Database mDatabase = new WebApp.DB.Database();
        private SqlDataReader mReader;
    }
}