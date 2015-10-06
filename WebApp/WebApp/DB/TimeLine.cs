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
        public TimeLine (string roadmapname)
        {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT ID, StartDate, EndDate FROM [dbo].[Timeline] WHERE RoadmapName = '" + roadmapname + "'");
            mReader.Read();

            mID = mReader.GetInt32(0);
            mStartDate = mReader.GetDateTime(1);
            mEndDate = mReader.GetDateTime(2);

            mDatabase.close();

            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, XPlacement FROM [dbo].[TickMark] WHERE TimelineID = '" + mID + "'");
            while (mReader.Read())
            {
                TickMark tick = new TickMark(mReader.GetString(0).ToString(), mReader.GetInt32(1));
                mTicks.Add(tick);
            }
            mDatabase.close();
        }

        public int GetID() { return mID; }
        public DateTime GetStartDate() { return mStartDate; }
        public DateTime GetEndDate() { return mEndDate; }
        public List<TickMark> GetTicks() { return mTicks; }

        private int mID;
        private DateTime mStartDate;
        private DateTime mEndDate;
        private List<TickMark> mTicks = new List<TickMark>();

        private WebApp.DB.Database mDatabase = new WebApp.DB.Database();
        private SqlDataReader mReader;
    }
}