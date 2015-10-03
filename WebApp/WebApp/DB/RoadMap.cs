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
        public RoadMap(string name, DateTime time, string desc, string userid)
        {
            mName = name;
            mTimeStamp = time;
            mDescription = desc;
            mUserID = userid;

            //Get the Timeline for the RoadMap
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT ID, StartDate, EndDate FROM [dbo].[Timeline] WHERE RoadMapName = " + name);
            mReader.Read();

            mTimeline = new TimeLine(Convert.ToInt32(mReader.GetString(0)), Convert.ToDateTime(mReader.GetString(1)), Convert.ToDateTime(mReader.GetString(2)));

            //Get the StrategyPoints
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, Description FROM [dbo].[StrategyPoint] WHERE RoadMapName = " + name);
            while (mReader.Read())
            {
                StrategyPoint sp = new StrategyPoint(mReader.GetString(0).ToString(), mReader.GetString(1).ToString());
                mStrategyPoints.Add(sp);
            }
        }

        //Getters if needed
        public string GetName() { return mName; }
        public DateTime GetTimeStamp() { return mTimeStamp; }
        public string GetDecription() { return mDescription; }
        public string GetUserID() { return mUserID; }
        public TimeLine GetTimeline() { return mTimeline; }

        private string mName;
        private DateTime mTimeStamp;
        private string mDescription;
        private string mUserID;
        private TimeLine mTimeline;
        private List<StrategyPoint> mStrategyPoints;

        private WebApp.DB.Database mDatabase;
        private SqlDataReader mReader;
    }
}