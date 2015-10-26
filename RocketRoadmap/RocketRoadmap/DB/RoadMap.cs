using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    public class RoadMap
    {
        public RoadMap( string name )
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

            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, StartDate, EndDate FROM [dbo].[Timeline] WHERE RoadmapName = '" + mName + "'");
            mReader.Read();

            //mTimeline = new TimeLine(mName);

            mDatabase.close();

            //Get the StrategyPoints
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, Description FROM [dbo].[StrategyPoint] WHERE RoadmapName = '" + name + "' ORDER BY NAME ASC");
            while (mReader.Read())
            {
                StrategyPoint sp = new StrategyPoint(mReader.GetString(0), mReader.GetString(1), name);
                mStrategyPoints.Add(sp);
            }
            mDatabase.close();
        }

        public bool CreateTimeLine(string name)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("INSERT INTO [dbo].[Timeline] (Name, StartDate, EndDate, RoadmapName ) VALUES ( '" + name + "', '" + DateTime.Now + "'" + ',' + "'" + DateTime.Now.AddYears(1) + "'" + ',' + "'" + mName + "')"))
            {
                mTimeline = new TimeLine(name);
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
                mTimeline.ClearTicks();
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

        public bool AddStrategyPoint(StrategyPoint point)
        {
            mStrategyPoints.Add(point);

            mDatabase.connect();

            bool flag = mDatabase.executewrite("INSERT INTO [dbo].[StrategyPoint] ([Name],[Description],[RoadmapName]) VALUES ('" + point.GetName() + "','"+point.GetDescription()+"','"+mName+"')");

            mDatabase.close();

            return flag;

        }

        public bool DeleteStrategyPoint( StrategyPoint Point)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("DELETE FROM [dbo].[StrategyPoint] WHERE RoadmapName = '" + mName + "' AND Name = '" + Point.GetName() + "'"))
            {
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public StrategyPoint GetPoint(string id)
        {

            
            foreach(StrategyPoint p in mStrategyPoints)
            {
                if(p.GetName()==id)
                {
                    return p;
                }
            }
            // point doesn't exist, we gotta problem here cap'n
            return null;
        }

        public void ReOrderStrategyPoint(string currname,string desc, bool isFirst)
        {
            StrategyPoint current = new StrategyPoint(currname, desc, mName);
            int index = (int)Char.GetNumericValue(currname[8]) + 1;
            string nextID = "StratBox"+index.ToString();
            string nextdesc=null;

            string selectname = null;
            if (isFirst) selectname = currname;
            else selectname = nextID;
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Description FROM [dbo].[StrategyPoint] WHERE Name='"+selectname+"' AND RoadmapName='"+mName+"'");
            if(mReader.HasRows){
                mReader.Read();
                nextdesc=mReader.GetString(0);
            }
            mReader.Close();
            mDatabase.close();


            StrategyPoint next = new StrategyPoint(nextID, nextdesc, mName);
            StrategyPoint nextdummy = new StrategyPoint(currname, nextdesc, mName);
            if (nextdesc != null)
            {
                nextdummy.EditName(nextID);
                ReOrderStrategyPoint(nextID, nextdesc, false);
            }
            

        }

        public void ReloadStrategyPoints()
        {
            mStrategyPoints = new List<StrategyPoint>();
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name, Description FROM [dbo].[StrategyPoint] WHERE RoadmapName = '" + mName + "' ORDER BY NAME ASC");
            while (mReader.Read())
            {
                StrategyPoint sp = new StrategyPoint(mReader.GetString(0), mReader.GetString(1), mName);
                mStrategyPoints.Add(sp);
            }
            mDatabase.close();
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

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;
    
    }
}

