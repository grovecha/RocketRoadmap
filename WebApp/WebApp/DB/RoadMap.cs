﻿using System;
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
            string UID = mReader.GetString(2);

            mDatabase.close();

            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, Email, Password FROM [dbo].[User] WHERE ID = '" + UID + "'");
            mReader.Read();

            mUser = new User(mReader.GetString(0), UID, mReader.GetString(1), mReader.GetString(2));

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

        public bool CreateTimeLine()
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("INSERT INTO [dbo].[Timeline] (StartDate, EndDate, RoadmapName ) VALUES (" + "'" + DateTime.Now + "'" + ',' + "'" + DateTime.Now.AddYears(1) + "'" + ',' + "'" + mName + "')"))
            {
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public bool DeleteTimeLine()
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("DELETE FROM [dbo].[Timeline] WHERE RoadmapName = '" + mName + "'"))
            {
                //remove all the tick marks
                TimeLine tline = new TimeLine(mName);
                tline.ClearTicks();

                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

           public bool EditName( string newname )
           {
               mDatabase.connect();
               bool toReturn = false;

               if (mDatabase.executewrite("UPDATE [dbo].[Roadmap] SET Name = '" + newname + "' WHERE Name = '" + mName + "'"))
               {
                   mName = newname;
                   toReturn = true;
               }

               mDatabase.close();
               return toReturn;
           }

        public bool EditDescription(string desc)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("UPDATE [dbo].[Roadmap] SET Description = '" + desc + "' WHERE Name = '" + mName + "'"))
            {
                mDescription = desc;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        //Getters if needed
        public string GetName() { return mName; }
        public DateTime GetTimeStamp() { return mTimeStamp; }
        public string GetDecription() { return mDescription; }
        public User GetUser() { return mUser; }
        public TimeLine GetTimeline() { return mTimeline; }
        public List<StrategyPoint> GetStrategyPoints() { return mStrategyPoints; }

        private string mName;
        private DateTime mTimeStamp;
        private string mDescription;
        private User mUser;
        private TimeLine mTimeline;
        private List<StrategyPoint> mStrategyPoints = new List<StrategyPoint>();

        private WebApp.DB.Database mDatabase = new WebApp.DB.Database();
        private SqlDataReader mReader;
    
    }
}

