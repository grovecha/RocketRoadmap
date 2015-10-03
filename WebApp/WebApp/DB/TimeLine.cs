using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.DB
{
    public class TimeLine
    {
        public TimeLine (int id, DateTime start, DateTime end)
        {
            mID = id;
            mStartDate = start;
            mEndDate = end;

            //grab all the tickmarks for the timeline
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, XPlacement FROM [dbo].[TickMark] WHERE Timelineid = " + id);
            while (mReader.Read())
            {
                TickMark tick = new TickMark(mReader.GetString(0).ToString(), Convert.ToInt32(mReader.GetString(0)));
                mTicks.Add(tick);
            }
        }

        public int GetID() { return mID; }
        public DateTime GetStartDate() { return mStartDate; }
        public DateTime GetEndDate() { return mEndDate; }
        public List<TickMark> GetTicks() { return mTicks; }

        private int mID;
        private DateTime mStartDate;
        private DateTime mEndDate;
        private List<TickMark> mTicks;

        private WebApp.DB.Database mDatabase;
        private SqlDataReader mReader;
    }
}