using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    //Roadmap class- holds an entire roadmap
    public class RoadMap
    {

        //CONSTRUCTOR
        public RoadMap( string name )
        {
            mName = name;

            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Timestamp, Description, UserID FROM [dbo].[Roadmap] WHERE Name =@Rname";
            cmd.Parameters.AddWithValue("@Rname", name);

            mReader = mDatabase.executereadparams(cmd);
            mReader.Read();

            mTimeStamp = mReader.GetDateTime(0);
            mDescription = mReader.GetString(1);
            string UID = mReader.GetString(2);

            mDatabase.close();

            mDatabase.connect();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "SELECT Name, Email, Password FROM [dbo].[User] WHERE ID = @User";
            cmd1.Parameters.AddWithValue("@User", UID);
            mReader = mDatabase.executereadparams(cmd1);
            mReader.Read();

            mUser = new User(mReader.GetString(0), UID, mReader.GetString(1), mReader.GetString(2));

            mDatabase.close();

            mDatabase.connect();
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandText = "SELECT Name, StartDate, EndDate FROM [dbo].[Timeline] WHERE RoadmapName = @Rname";
            cmd2.Parameters.AddWithValue("@Rname", mName);
            mReader = mDatabase.executereadparams(cmd2);
            mReader.Read();

            //mTimeline = new TimeLine(mName);

            mDatabase.close();

            //Get the StrategyPoints
            mDatabase.connect();
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandText = "SELECT Name, Description FROM [dbo].[StrategyPoint] WHERE RoadmapName =@Rname ORDER BY NAME ASC";
            cmd3.Parameters.AddWithValue("@Rname", mName);
            mReader = mDatabase.executereadparams(cmd3);
            while (mReader.Read())
            {
                StrategyPoint sp = new StrategyPoint(mReader.GetString(0), mReader.GetString(1), name);
                mStrategyPoints.Add(sp);
            }
            mDatabase.close();
        }

        //Creates timeline with Name=name
        public bool CreateTimeLine(string name)
        {
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[Timeline] (Name, StartDate, EndDate, RoadmapName ) VALUES ( @Tname, @sdate, @edate, @Rname)";
            cmd.Parameters.AddWithValue("@Tname", name);
            cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
            cmd.Parameters.AddWithValue("@edate", DateTime.Now.AddYears(1));
            cmd.Parameters.AddWithValue("@Rname", mName);
            bool toReturn = false;

            if (mDatabase.executewriteparam(cmd))
            {
                mTimeline = new TimeLine(name);
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        //Add strategy point to roadmap
        public bool AddStrategyPoint(StrategyPoint point)
        {
            mStrategyPoints.Add(point);

            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[StrategyPoint] ([Name],[Description],[RoadmapName]) VALUES (@Sname,@descrip,@Rname)";
            cmd.Parameters.AddWithValue("@Sname", point.GetName());
            cmd.Parameters.AddWithValue("@descrip", point.GetDescription());
            cmd.Parameters.AddWithValue("@Rname", mName);

            bool flag = mDatabase.executewriteparam(cmd);

            mDatabase.close();

            return flag;

        }

        //Delete timeline
        public bool DeleteTimeLine()
        {
            mDatabase.connect();
            bool toReturn = false;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM [dbo].[Timeline] WHERE RoadmapName = @Rname";
            cmd.Parameters.AddWithValue("@Rname",mName);
            if (mDatabase.executewriteparam(cmd))
            {
                mTimeline.ClearTicks();
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        //Edit roadmap name
           public bool EditName( string newname )
           {
               mDatabase.connect();
               SqlCommand cmd = new SqlCommand();
               cmd.CommandText = "UPDATE [dbo].[Roadmap] SET Name = @Rname WHERE Name =@oldname";
               cmd.Parameters.AddWithValue("@Rname", newname);
               cmd.Parameters.AddWithValue("@oldname", mName);

               bool toReturn = false;

               if (mDatabase.executewriteparam(cmd))
               {
                   mName = newname;
                   toReturn = true;
               }

               mDatabase.close();
               return toReturn;
           }

        //Edit roadmap description
        public bool EditDescription(string desc)
        {
            mDatabase.connect();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[Roadmap] SET Description = @descrip WHERE Name = @Rname";
            cmd.Parameters.AddWithValue("@descrip", desc);
            cmd.Parameters.AddWithValue("@Rname", mName);
            bool toReturn = false;

            if (mDatabase.executewriteparam(cmd))
            {
                mDescription = desc;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        //Get a strategy point from the list of points
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

        //Reorders strategy points to insert one in the middle
        public void ReOrderStrategyPoint(string currname,string desc, bool isFirst)
        {
            //Get current strategy point
            StrategyPoint current = new StrategyPoint(currname, desc, mName);
            int index = (int)Char.GetNumericValue(currname[8]) + 1;
            string nextID = "StratBox"+index.ToString();
            string nextdesc=null;

            //Move next one up
            string selectname = null;
            if (isFirst) selectname = currname;
            else selectname = nextID;
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Description FROM [dbo].[StrategyPoint] WHERE Name=@sname AND RoadmapName=@Rname";
            cmd.Parameters.AddWithValue("@sname", selectname);
            cmd.Parameters.AddWithValue("@Rname", mName);
            mReader = mDatabase.executereadparams(cmd);
            if(mReader.HasRows){
                mReader.Read();
                nextdesc=mReader.GetString(0);
            }
            mReader.Close();
            mDatabase.close();

            //Continue moving strat points up one
            StrategyPoint next = new StrategyPoint(nextID, nextdesc, mName);
            StrategyPoint nextdummy = new StrategyPoint(currname, nextdesc, mName);
            if (nextdesc != null)
            {
                nextdummy.EditName(nextID);
                ReOrderStrategyPoint(nextID, nextdesc, false);
            }
        }

        //Reloads list of strategy points
        public void ReloadStrategyPoints()
        {
            mStrategyPoints = new List<StrategyPoint>();
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name, Description FROM [dbo].[StrategyPoint] WHERE RoadmapName = @Rname ORDER BY NAME ASC";
            cmd.Parameters.AddWithValue("@Rname", mName);
            mReader = mDatabase.executereadparams(cmd);
            while (mReader.Read())
            {
                StrategyPoint sp = new StrategyPoint(mReader.GetString(0), mReader.GetString(1), mName);
                mStrategyPoints.Add(sp);
            }
            mDatabase.close();
        }

        //Gets all projects for a roadmap
        public List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();

            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Name, Description, BusinessValueName, RoadmapName FROM [dbo].[Project] WHERE RoadmapName = @Rname ORDER BY NAME ASC";
            cmd.Parameters.AddWithValue("@Rname", mName);
            mReader = mDatabase.executereadparams(cmd);
            while (mReader.Read())
            {
                Project temp = new Project(mReader.GetString(0), mReader.GetString(1), mReader.GetString(2), mReader.GetString(3));
                projects.Add(temp);
            }
            mReader.Close();
            mDatabase.close();

            return projects;
        }

        //Delete a strategy point
        public bool DeleteStrategyPoint(string name)
        {
            foreach (StrategyPoint sp in mStrategyPoints.ToList())
            {
                if (sp.GetName() == name) mStrategyPoints.Remove(sp);
            }
            mDatabase.connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM [dbo].[StrategyPoint] WHERE RoadmapName =@Rname AND Name = @sname";
            cmd.Parameters.AddWithValue("@Rname", mName);
            cmd.Parameters.AddWithValue("@sname", name);
            bool flag = mDatabase.executewriteparam(cmd);
            mDatabase.close();

            int index = (int)Char.GetNumericValue(name[8]);

            foreach (StrategyPoint sp in mStrategyPoints.ToList())
            {
                if ((int)Char.GetNumericValue(sp.GetName()[8]) > index)
                {
                    int newindex = (int)Char.GetNumericValue(sp.GetName()[8]) - 1;
                    string newname = sp.GetName().Substring(0, 8) + newindex.ToString();
                    sp.EditName(newname);
                }
            }

            return flag;
        }

        //Getters if needed
        public string GetName() { return mName; }
        public void SetName(string name) { mName = name; }
        public DateTime GetTimeStamp() { return mTimeStamp; }
        public string GetDecription() { return mDescription; }
        public User GetUser() { return mUser; }
        public TimeLine GetTimeline() { return mTimeline; }
        public List<StrategyPoint> GetStrategyPoints() { return mStrategyPoints; }

        //Roadmap name
        private string mName;
        //Last edit
        private DateTime mTimeStamp;
        private string mDescription;
        //Owner of roadmap
        private User mUser;
        private TimeLine mTimeline;
        private List<StrategyPoint> mStrategyPoints = new List<StrategyPoint>();

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;
    
    }
}

