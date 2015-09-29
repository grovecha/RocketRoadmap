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
        public void SetName(string name) {
            SqlConnection conn = WebApp.DB.connect();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "UPDATE Name from [dbo].[Project] WHERE Name='bpchiv@gmail.com'";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();
            string name = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    name = reader.GetString(0);

                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
            conn.Close();
            return name;
        }
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