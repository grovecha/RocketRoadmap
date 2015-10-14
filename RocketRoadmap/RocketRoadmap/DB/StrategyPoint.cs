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
        public StrategyPoint(string id,string desc) 
        {
            mName = id;
            mDescription = desc;

            //Get the StrategyPoints
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT BusinessValueName FROM [dbo].[SP_BV_Crosswalk] WHERE StrategyPointName = '" + mName + "'");
            while (mReader.Read())
            {
                string temp = mReader.GetString(0);
                BusinessValue bv = new BusinessValue(mReader.GetString(0));
                mValues.Add(bv);
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

            if (mDatabase.executewrite("UPDATE [dbo].[StrategyPoint] SET Name = '" + name + "' WHERE Name = '" + mName + "'"))
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

            if (mDatabase.executewrite("UPDATE [dbo].[StrategyPoint] SET Description = '" + desc + "' WHERE Name = '" + mName + "'"))
            {
                mDescription = desc;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public BusinessValue GetVal(string id)
        {
            foreach(BusinessValue v in mValues)
            {
                if(v.GetName()== id)
                {
                    return v;
                }
            }
            //oh no! Something went wrong! I blame brian.
            //wat - brian
            return null;
        }

        public void AddBusinessValue(BusinessValue bv)
        {
            mValues.Add(bv);
            mDatabase.connect();

            bool flag=mDatabase.executewrite("INSERT INTO [dbo].[BusinessValue] (Name, Description) VALUES ('" + bv.GetName() + "','" + bv.GetDescription() + "')");
            bool flag2=mDatabase.executewrite("INSERT INTO [dbo].[SP_BV_Crosswalk] (StrategyPointName, BusinessValueName) VALUES ('" + mName + "','" + bv.GetName() + "')");

            mDatabase.close();
        }



        private string mName;
        private string mDescription;
        private List<BusinessValue> mValues = new List<BusinessValue>();

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;
    }
}