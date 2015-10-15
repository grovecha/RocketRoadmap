using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    public class TimeLine 
    {
        public TimeLine (string roadmapname)
        {

            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, StartDate, EndDate FROM [dbo].[Timeline] WHERE RoadmapName = '" + roadmapname + "'");
            mReader.Read();

           // mName = mReader.GetString(0);
            mStartDate = mReader.GetDateTime(1);
            mEndDate = mReader.GetDateTime(2);

            mDatabase.close();

            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, XPlacement FROM [dbo].[TickMark] WHERE TimelineName = '" + mName + "'");
            while (mReader.Read())
            {
                TickMark tick = new TickMark(mReader.GetString(0).ToString(), mReader.GetInt32(1));
                mTicks.Add(tick);
            }
            mDatabase.close();
            //mDatabase.connect();
            //mReader = mDatabase.executeread("SELECT Name, StartDate, EndDate FROM [dbo].[Timeline] WHERE RoadmapName = '" + roadmapname + "'");
            //mReader.Read();

            //mName = mReader.GetString(0);
            //mStartDate = mReader.GetDateTime(1);
            //mEndDate = mReader.GetDateTime(2);

            //mDatabase.close();

            //mDatabase.connect();
            //mReader = mDatabase.executeread("SELECT Name, XPlacement FROM [dbo].[TickMark] WHERE TimelineName = '" + mName + "'");
            //while (mReader.Read())
            //{
            //    TickMark tick = new TickMark(mReader.GetString(0).ToString(), mReader.GetInt32(1));
            //    mTicks.Add(tick);
            //}
            //mDatabase.close();
        }

        public bool NewTickMark(TickMark tick)
        {
            mDatabase.connect();
            bool toReturn = false;

            //add Tickmark
            mTicks.Add(tick);

            if (mDatabase.executewrite("INSERT INTO [dbo].[TickMark] ( Name, XPlacement, TimelineName ) VALUES (" + "'" + tick.GetName() + "'" + ',' + "'" + tick.GetXPlacement() + "'" + ',' + "'" + mName + "')"))
            {
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public bool DeleteTickMark(TickMark tick)
        {
            mDatabase.connect();
            bool toReturn = false;

            //delete Tickmark
            mTicks.Remove(tick);

            if (mDatabase.executewrite("DELETE FROM [dbo].[TickMark] WHERE Name = '" + tick.GetName() + "' AND TimelineName = '" + mName + "'"))
            {
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public bool ClearTicks()
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("DELETE FROM [dbo].[TickMark] WHERE TimelineName = '" + mName + "'"))
            {
                //delete Tickmark
                mTicks.Clear();
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public bool EditStartDate(DateTime date, string rname)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("UPDATE[dbo].[Timeline] SET StartDate = '" + date + "' WHERE RoadMapName = '" + rname + "'"))
            {
                mStartDate = date;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public bool EditEndDate(DateTime date, string rname)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("UPDATE[dbo].[Timeline] SET EndDate = '" + date + "' WHERE RoadMapName = '" + rname + "'"))
            {
                mEndDate = date;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public String GetName() { return mName; }
        public DateTime GetStartDate() { return mStartDate; }
        public DateTime GetEndDate() { return mEndDate; }
        public List<TickMark> GetTicks() { return mTicks; }

        private String mName;
        private DateTime mStartDate;
        private DateTime mEndDate;
        private List<TickMark> mTicks = new List<TickMark>();

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;
    }
}