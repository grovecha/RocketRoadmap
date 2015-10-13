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
        public StrategyPoint(string name,string desc) 
        {
            mName = name;
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



        private string mName;
        private string mDescription;
        private List<BusinessValue> mValues = new List<BusinessValue>();

        private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
        private SqlDataReader mReader;
    }
}