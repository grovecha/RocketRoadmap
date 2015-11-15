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
            mName = roadmapname;

            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name, StartDate, EndDate FROM [dbo].[Timeline] WHERE RoadmapName =@Rname";
            cmd.Parameters.AddWithValue("@Rname", roadmapname);
            mReader = mDatabase.executereadparams(cmd);
            mReader.Read();

           // mName = mReader.GetString(0);
           // mStartDate = mReader.GetDateTime(1);
           // mEndDate = mReader.GetDateTime(2);

            mDatabase.close();

            mDatabase.connect();
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandText = "SELECT Name, XPlacement FROM [dbo].[TickMark] WHERE TimelineName =@Tname";
            cmd2.Parameters.AddWithValue("@Tname", mName);
            mReader = mDatabase.executereadparams(cmd2);
            while (mReader.Read())
            {
                TickMark tick = new TickMark(mReader.GetString(0).ToString(), mReader.GetInt32(1));
                mTicks.Add(tick);
            }
            mDatabase.close();

            mDatabase.connect();
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandText = "SELECT Name, StartDate, EndDate FROM [dbo].[Timeline] WHERE RoadmapName =@Rname";
            cmd3.Parameters.AddWithValue("@Rname", roadmapname);
            mReader = mDatabase.executereadparams(cmd3);
            mReader.Read();

            mName = mReader.GetString(0);
            mStartDate = mReader.GetDateTime(1);
            mEndDate = mReader.GetDateTime(2);

            mDatabase.close();

            mDatabase.connect();
            SqlCommand cmd4 = new SqlCommand();
            cmd4.CommandText = "SELECT Name, XPlacement FROM [dbo].[TickMark] WHERE TimelineName = @TName";
            cmd4.Parameters.AddWithValue("@TName", mName);
            mReader = mDatabase.executereadparams(cmd4);
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

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "INSERT INTO [dbo].[TickMark] ( Name, XPlacement, TimelineName ) VALUES (@tname,@X,@Timeline)";
                cmd.Parameters.AddWithValue("@tname", tick.GetName());
                cmd.Parameters.AddWithValue("@X", tick.GetXPlacement());
                cmd.Parameters.AddWithValue("@Timeline", mName);
                if (mDatabase.executewriteparam(cmd))
                {
                    toReturn = true;
                }
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

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "DELETE FROM [dbo].[TickMark] WHERE Name = @tName AND TimelineName =@timeline";
                cmd.Parameters.AddWithValue("@tName", tick.GetName());
                cmd.Parameters.AddWithValue("@timeline", mName);
                if (mDatabase.executewriteparam(cmd))
                {
                    toReturn = true;
                }
            }
            mDatabase.close();
            return toReturn;
        }

        public bool ClearTicks()
        {
            mDatabase.connect();
            bool toReturn = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM [dbo].[TickMark] WHERE TimelineName = @Tname";
            cmd.Parameters.AddWithValue("@Tname", mName);
            if (mDatabase.executewriteparam(cmd))
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

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "UPDATE[dbo].[Timeline] SET StartDate =@sname WHERE RoadMapName =@Rname";
                cmd.Parameters.AddWithValue("@sname", date);
                cmd.Parameters.AddWithValue("@Rname", rname);
                if (mDatabase.executewriteparam(cmd))
                {
                    mStartDate = date;
                    toReturn = true;
                }
            }
            mDatabase.close();
            return toReturn;
        }

        public bool EditEndDate(DateTime date, string rname)
        {
            mDatabase.connect();
            bool toReturn = false;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "UPDATE[dbo].[Timeline] SET EndDate =@edate WHERE RoadMapName =@Rname";
                cmd.Parameters.AddWithValue("@edate", date);
                cmd.Parameters.AddWithValue("@Rname", rname);
                if (mDatabase.executewriteparam(cmd))
                {
                    mEndDate = date;
                    toReturn = true;
                }
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