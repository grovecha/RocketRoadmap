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
        public TimeLine (int id, DateTime start, DateTime end, string roadmapname)
        {
            mID = id;
            mStartDate = start;
            mEndDate = end;

            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, XPlacement FROM [dbo].[TickMark] WHERE TimelineID = '" + mID + "'");
            while (mReader.Read())
            {
                TickMark tick = new TickMark(mReader.GetString(0).ToString(), mReader.GetInt32(1));
                mTicks.Add(tick);
            }
            mDatabase.close();
        }

        public bool NewTickMark(TickMark tick)
        {
            mDatabase.connect();
            bool toReturn = false;

            //add Tickmark
            mTicks.Add(tick);

            if (mDatabase.executewrite("INSERT INTO [dbo].[TickMark] ( Name, XPlacement, TimelineID ) VALUES (" + "'" + tick.GetName() + "'" + ',' + "'" + tick.GetXPlacement() + "'" + ',' + "'" + mID + "')"))
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

            if (mDatabase.executewrite("DELETE FROM [dbo].[TickMark] WHERE Name = '" + tick.GetName() + "' AND TimelineID = '" + mID + "'"))
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

            if (mDatabase.executewrite("DELETE FROM [dbo].[TickMark] WHERE TimelineID = '" + mID + "'"))
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

        public int GetID() { return mID; }
        public DateTime GetStartDate() { return mStartDate; }
        public DateTime GetEndDate() { return mEndDate; }
        public List<TickMark> GetTicks() { return mTicks; }

        private int mID;
        private DateTime mStartDate;
        private DateTime mEndDate;
        private List<TickMark> mTicks = new List<TickMark>();

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;
    }
}