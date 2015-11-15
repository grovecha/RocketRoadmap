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
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                
                //using (SqlCommand cmd = new SqlCommand())
                //{
                //    cmd.CommandText = "SELECT Name, StartDate, EndDate FROM [dbo].[Timeline] WHERE RoadmapName =@Rname";
                //    cmd.Parameters.AddWithValue("@Rname", roadmapname);
                //    mReader = mDatabase.executereadparams(cmd);
                //    mReader.Read();

                //    // mName = mReader.GetString(0);
                //    // mStartDate = mReader.GetDateTime(1);
                //    // mEndDate = mReader.GetDateTime(2);
                //}

                mName = roadmapname;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO [dbo].[Timeline] (Name, RoadmapName) VALUES (@name,@Rname)";
                    cmd.Parameters.AddWithValue("@Rname", roadmapname);
                    cmd.Parameters.AddWithValue("@name", mName);
                    cmd.Connection = conn;

                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) { }
                    conn.Close();
                }
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "SELECT Name, XPlacement FROM [dbo].[TickMark] WHERE TimelineName =@Tname";
                    cmd2.Parameters.AddWithValue("@Tname", mName);
                    cmd2.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd2.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            TickMark tick = new TickMark(Reader.GetString(0).ToString(), Reader.GetInt32(1));
                            mTicks.Add(tick);
                        }
                    }
                    conn.Close();
                }

                using (SqlCommand cmd3 = new SqlCommand())
                {
                    cmd3.CommandText = "SELECT Name, StartDate, EndDate FROM [dbo].[Timeline] WHERE RoadmapName =@Rname";
                    cmd3.Parameters.AddWithValue("@Rname", roadmapname);
                    cmd3.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd3.ExecuteReader())
                    {
                        Reader.Read();

                        mName = Reader.GetString(0);

                    }
                    conn.Close();
                }

                using (SqlCommand cmd4 = new SqlCommand())
                {
                    cmd4.CommandText = "SELECT Name, XPlacement FROM [dbo].[TickMark] WHERE TimelineName = @TName";
                    cmd4.Parameters.AddWithValue("@TName", mName);
                    cmd4.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd4.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            TickMark tick = new TickMark(Reader.GetString(0).ToString(), Reader.GetInt32(1));
                            mTicks.Add(tick);
                        }
                    }
                    conn.Close();
                }
            }
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
            bool toReturn = false;

            //add Tickmark
            mTicks.Add(tick);
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO [dbo].[TickMark] ( Name, XPlacement, TimelineName ) VALUES (@tname,@X,@Timeline)";
                    cmd.Parameters.AddWithValue("@tname", tick.GetName());
                    cmd.Parameters.AddWithValue("@X", tick.GetXPlacement());
                    cmd.Parameters.AddWithValue("@Timeline", mName);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        public bool DeleteTickMark(TickMark tick)
        {
            bool toReturn = false;

            //delete Tickmark
            mTicks.Remove(tick);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "DELETE FROM [dbo].[TickMark] WHERE Name = @tName AND TimelineName =@timeline";
                    cmd.Parameters.AddWithValue("@tName", tick.GetName());
                    cmd.Parameters.AddWithValue("@timeline", mName);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        public bool ClearTicks()
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM [dbo].[TickMark] WHERE TimelineName = @Tname";
                cmd.Parameters.AddWithValue("@Tname", mName);
                cmd.Connection = conn;

                conn.Open();
                if (cmd.ExecuteNonQuery()!=0)
                {
                    //delete Tickmark
                    mTicks.Clear();
                    toReturn = true;
                }
                conn.Close();
            }
            return toReturn;
        }

        public bool EditStartDate(DateTime date, string rname)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE[dbo].[Timeline] SET StartDate =@sname WHERE RoadMapName =@Rname";
                    cmd.Parameters.AddWithValue("@sname", date);
                    cmd.Parameters.AddWithValue("@Rname", rname);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        mStartDate = date;
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        public bool EditEndDate(DateTime date, string rname)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE[dbo].[Timeline] SET EndDate =@edate WHERE RoadMapName =@Rname";
                    cmd.Parameters.AddWithValue("@edate", date);
                    cmd.Parameters.AddWithValue("@Rname", rname);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        mEndDate = date;
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
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

        //private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        //private SqlDataReader mReader;
    }
}