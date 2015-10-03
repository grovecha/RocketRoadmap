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
            mDatabase.executewrite("UPDATE Project SET Name=" + name + " WHERE Name=" + mName);
            mDatabase.close();
        }
        public string GetName() {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Name FROM Project WHERE Name=" + mName);
            mReader.Read();
            mDatabase.close();
            return mReader.GetString(0).ToString();
        }

        public void SetDescription(string description) {
            mDatabase.connect();
            mDatabase.executewrite("UPDATE Project SET Description=" + description + " WHERE Name=" + mName);
            mDatabase.close();
        }
        public string GetDescription() {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT Description FROM Project WHERE Name=" + mName);
            mReader.Read();
            mDatabase.close();
            return mReader.GetString(0).ToString();
        }

        public void SetStartDate(DateTime startdate) {
            mDatabase.connect();
            mDatabase.executewrite("UPDATE Project SET StartDate=" + startdate + " WHERE Name=" + mName);
            mDatabase.close(); 
        }
        public DateTime GetStartDate() {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT StartDate FROM Project WHERE Name=" + mName);
            mReader.Read();
            mDatabase.close();
            return mReader.GetDateTime(0);
        }

        public void SetEndDate(DateTime enddate) {
            mDatabase.connect();
            mDatabase.executewrite("UPDATE Project SET EndDate=" + enddate + " WHERE Name=" + mName);
            mDatabase.close();
        }
        public DateTime GetEndDate() {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT EndDate FROM Project WHERE Name=" + mName);
            mReader.Read();
            mDatabase.close();
            return mReader.GetDateTime(0);
        }

        public void SetBusinessValue(string businessvalue) {
            mDatabase.connect();
            mDatabase.executewrite("UPDATE Project SET BusinessValue=" + businessvalue + " WHERE Name=" + mName);
            mDatabase.close();
        }
        public string GetBusinessValue() {
            mDatabase.connect();
            mReader = mDatabase.executeread("SELECT BusinessValue FROM Project WHERE Name=" + mName);
            mReader.Read();
            mDatabase.close();
            return mReader.GetString(0).ToString();
        }
        #endregion
    }
}