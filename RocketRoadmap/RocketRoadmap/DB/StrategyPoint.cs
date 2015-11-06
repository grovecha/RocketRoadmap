using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    //Strategy Point class
    public class StrategyPoint
    {
        //CONSTRUCTOR
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

        //Getters
        public string GetName() { return mName; }
        public void SetName(string name) { mName = name; }
        public string GetDescription() { return mDescription; }
        public List<BusinessValue> GetBusinessValues() { return mValues; }

        //Edit name of spoint
        public bool EditName(string name)
        {
            mDatabase.connect();
            bool toReturn = false;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText="UPDATE [dbo].[StrategyPoint] SET Name = @Sname WHERE Name =@oldname AND RoadmapName =@Rname";
            cmd.Parameters.AddWithValue("@Sname", name);
            cmd.Parameters.AddWithValue("@oldname", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            if (mDatabase.executewriteparam(cmd))
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

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [dbo].[StrategyPoint] SET Description = @desc WHERE Name = @Sname AND RoadmapName =@Rname";
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@Sname", mName);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
            if (mDatabase.executewriteparam(cmd))
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
                //Add to both the BV table, and the BV/SP ownership table
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "INSERT INTO [dbo].[BusinessValue] (Name, Description, RoadmapName) VALUES (@BVName, @desc,@Rname)";
                cmd1.Parameters.AddWithValue("@BVName", name);
                cmd1.Parameters.AddWithValue("@desc", desc);
                cmd1.Parameters.AddWithValue("@Rname", mRoadmapName);
                bool flag = mDatabase.executewriteparam(cmd1);
               
                
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandText = "INSERT INTO [dbo].[SP_BV_Crosswalk] (StrategyPointName, BusinessValueName, RoadmapName) VALUES (@Sname,@BVName,@Rname)";
                cmd2.Parameters.AddWithValue("@BVName", name);
                cmd2.Parameters.AddWithValue("@Sname", mName);
                cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);
                flag = mDatabase.executewriteparam(cmd2);
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

        public bool DeleteBusinessValue(string name)
        {
            bool flag = false;
            foreach (BusinessValue bv in mValues.ToList())
            {
                if (bv.GetName() == name)
                {
                    flag = true;
                    mValues.Remove(bv);
                    break;
                }
            }
            if (!flag) return false;

            //Delete from both the BV table and the SP/BV ownership table
            mDatabase.connect();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "DELETE FROM [dbo].[BusinessValue] WHERE Name=@BVName AND RoadmapName=@Rname";
            cmd1.Parameters.AddWithValue("@BVName", name);
            cmd1.Parameters.AddWithValue("@Rname", mRoadmapName);
            bool flag2 = mDatabase.executewriteparam(cmd1);

            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandText = "DELETE FROM [dbo].[SP_BV_Crosswalk] WHERE BusinessValueName=@BVName AND StrategyPointName=@Sname AND RoadmapName=@Rname";
            cmd2.Parameters.AddWithValue("@BVName", name);
            cmd2.Parameters.AddWithValue("@Sname", mName);
            cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);

            bool flag3 = mDatabase.executewriteparam(cmd2);
            mDatabase.close();

            if (!flag2) return false;
            if (!flag3) return false;

            int index = (int)Char.GetNumericValue(name[15]);

            foreach (BusinessValue bv in mValues.ToList())
            {
                if ((int)Char.GetNumericValue(bv.GetName()[15]) > index)
                {
                    int newindex = (int)Char.GetNumericValue(bv.GetName()[15]) - 1;
                    string newname = bv.GetName().Substring(0, 15) + newindex.ToString();
                    bv.SetName(newname);
                }
            }
            return true;
        }

        //Reorder BV to insert a new one in the middle
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
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Description FROM [dbo].[BusinessValue] WHERE Name=@sname AND RoadmapName=@Rname";
            cmd.Parameters.AddWithValue("@sname", selectname);
            cmd.Parameters.AddWithValue("@Rname", mRoadmapName);

            mReader = mDatabase.executereadparams(cmd);
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

        //Reload list of BV's
        public void ReloadBusinessValues()
        {
            mValues = new List<BusinessValue>();
            mDatabase.connect();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "SELECT BusinessValueName FROM [dbo].[SP_BV_Crosswalk] WHERE RoadmapName = @Rname AND StrategyPointName =@Sname";
            cmd1.Parameters.AddWithValue("@Rname", mRoadmapName);
            cmd1.Parameters.AddWithValue("@Sname", mName);
            mReader = mDatabase.executereadparams(cmd1);
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
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandText = "SELECT Description FROM [dbo].[BusinessValue] WHERE Name=@BVName AND RoadmapName=@Rname";
                cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);
                cmd2.Parameters.AddWithValue("@BVName", bv.GetName());
                mReader = mDatabase.executereadparams(cmd2);
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