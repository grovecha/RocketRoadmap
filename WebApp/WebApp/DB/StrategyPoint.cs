using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.DB
{
    public class StrategyPoint
    {
        public StrategyPoint(string name, string desc) 
        {
            mName = name;
            mDescription = desc;
        }

        public string GetName() { return mName; }
        public string GetDescription() { return mDescription; }

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

        private WebApp.DB.Database mDatabase = new WebApp.DB.Database();
        private SqlDataReader mReader;
    }
}