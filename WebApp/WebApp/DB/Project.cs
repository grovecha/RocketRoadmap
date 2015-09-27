using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DB
{
    public class Project
    {
        private string mName;
        private string mDescription;
        private DateTime mStartDate;
        private DateTime mEndDate;
        private string mBusinessValue;

        #region Getter's and Setters
        public void SetName(string name) { mName = name; }
        public string GetName() { return mName; }

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