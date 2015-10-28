using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    public class StrategyPoint
    {
        public StrategyPoint(string id,string desc, string rname) 
        {
            mName = id;
            mDescription = desc;
            mRoadmapName = rname;

            //Get the StrategyPoints
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT BusinessValueName FROM [dbo].[SP_BV_Crosswalk] WHERE RoadmapName = '" + rname + "' AND StrategyPointName ='" + mName + "'");
            while (mReader.Read())
            {
                string temp = mReader.GetString(0);
                BusinessValue bv = new BusinessValue(mReader.GetString(0), mRoadmapName);
                mValues.Add(bv);
            }
            mDatabase.close();

            foreach (BusinessValue bv in mValues)
            {
                mDatabase.connect();
                mReader = mDatabase.executeread("SELECT Description FROM [dbo].[BusinessValue] WHERE Name='" + bv.GetName() + "' AND RoadmapName='" + mRoadmapName + "'");
                if (mReader.HasRows)
                {
                    mReader.Read();
                    bv.SetDescription(mReader.GetString(0).ToString());
                    mReader.Close();
                }
            }
            mDatabase.close();
        }

        public string GetName() { return mName; }
        public string GetDescription() { return mDescription; }
        public List<BusinessValue> GetBusinessValues() { return mValues; }

        public bool EditName(string name)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("UPDATE [dbo].[StrategyPoint] SET Name = '" + name + "' WHERE Name = '" + mName + "' AND RoadmapName ='" + mRoadmapName + "'"))
            {
                mName = name;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public bool EditDescription(string desc)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("UPDATE [dbo].[StrategyPoint] SET Description = '" + desc + "' WHERE Name = '" + mName + "' AND RoadmapName ='" + mRoadmapName + "'"))
            {
                mDescription = desc;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public BusinessValue GetBusinessValue(string id)
        {
            foreach(BusinessValue v in mValues)
            {
                if(v.GetName()== id)
                {
                    return v;
                }
            }
            //oh no! Something went wrong! I blame brian.
            return null;
        }

        public bool CreateBuisnessValue(string name, string desc, string rname)
        {
            mDatabase.connect();
            try
            {
                bool flag = mDatabase.executewrite("INSERT INTO [dbo].[BusinessValue] (Name, Description, RoadmapName) VALUES ('" + name + "', '" + desc+ "','" + rname + "')");
                flag = mDatabase.executewrite("INSERT INTO [dbo].[SP_BV_Crosswalk] (StrategyPointName, BusinessValueName, RoadmapName) VALUES ('" + mName + "','" + name + "','" + rname + "')");
                mDatabase.close();
                BusinessValue bis = new BusinessValue(name, rname);
                bis.SetDescription(desc);
                mValues.Add(bis);
                return flag;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void ReorderBusinessValue(string currname, string desc, bool isFirst)
        {
            BusinessValue current = new BusinessValue(currname, mRoadmapName);
            current.SetDescription(desc);
            int index = (int)Char.GetNumericValue(currname[15]) + 1;
            string nextID = currname.Substring(0,15) + index.ToString();
            string nextdesc = null;

            string selectname = null;
            if (isFirst) selectname = currname;
            else selectname = nextID;
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Description FROM [dbo].[BusinessValue] WHERE Name='" + selectname + "' AND RoadmapName='" + mRoadmapName + "'");
            if (mReader.HasRows)
            {
                mReader.Read();
                nextdesc = mReader.GetString(0);
                mReader.Close();
            }
            mDatabase.close();


            BusinessValue next = new BusinessValue(nextID, mRoadmapName);
            next.SetDescription(nextdesc);
            BusinessValue nextdummy = new BusinessValue(currname, mRoadmapName);
            nextdummy.SetDescription(nextdesc);
            if (nextdesc != null)
            {
                nextdummy.SetName(nextID);
                ReorderBusinessValue(nextID, nextdesc, false);
            }
            mDatabase.close();
        }

        public void ReloadBusinessValues()
        {
            mValues = new List<BusinessValue>();
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT BusinessValueName FROM [dbo].[SP_BV_Crosswalk] WHERE RoadmapName = '" + mRoadmapName + "' AND StrategyPointName ='" + mName + "'");
            while (mReader.Read())
            {
                string temp = mReader.GetString(0);
                BusinessValue bv = new BusinessValue(mReader.GetString(0), mRoadmapName);
                mValues.Add(bv);
            }
            mReader.Close();
            mDatabase.close();

            foreach(BusinessValue bv in mValues){
                mDatabase.connect();
                mReader = mDatabase.executeread("SELECT Description FROM [dbo].[BusinessValue] WHERE Name='" + bv.GetName() + "' AND RoadmapName='" + mRoadmapName + "'");
                if (mReader.HasRows)
                {
                    mReader.Read();
                    bv.SetDescription(mReader.GetString(0).ToString());
                    mReader.Close();
                }
            }
            mDatabase.close();

        }
        private string mName;
        private string mDescription;
        private string mRoadmapName;
        private List<BusinessValue> mValues = new List<BusinessValue>();

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;
    }
}