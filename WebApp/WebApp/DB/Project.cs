using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebApp.DB
{
    public class Project
    {
        private string mName;
        private string mDescription;
        private DateTime mStartDate;
        private DateTime mEndDate;
        private string mBusinessValue;

        private WebApp.DB.Database mDatabase;
        private SqlDataReader mReader;


        #region Getter's and Setters
        public void SetName(string name) {
            mDatabase.connect();
            mReader = mDatabase.execute("UPDATE Project SET Name=" + name + " WHERE Name=" + mName);
            mDatabase.close();
        }
        public string GetName() {
            mDatabase.connect();
            mReader = mDatabase.execute("SELECT Name FROM Project WHERE Name=" + mName);
            mReader.Read();
            return mReader.GetString(0).ToString();
        }

        public void SetDescription(string description) { mDescription = description; }
        public string GetDescription() { return mDescription; }

        public void SetStartDate(DateTime startdate) { mStartDate = startdate; }
        public DateTime GetStartDate() { return mStartDate; }

        public void SetEndDate(DateTime enddate) { mEndDate = enddate; }
        public DateTime GetEndDate() { return mEndDate; }

        public void SetBusinessValue(string businessvalue) { mBusinessValue = businessvalue; }
        public string GetBusinessValue() { return mBusinessValue; }
        #endregion
    }
}