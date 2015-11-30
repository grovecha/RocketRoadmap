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
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT BusinessValueName FROM [dbo].[SP_BV_Crosswalk] WHERE RoadmapName =@Rname AND StrategyPointName =@Sname";
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Parameters.AddWithValue("@Sname", mName);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            string temp = Reader.GetString(0);
                            BusinessValue bv = new BusinessValue(Reader.GetString(0), mRoadmapName);
                            mValues.Add(bv);
                        }
                    }

                }



                using (SqlCommand cmd3 = new SqlCommand())
                {
                    cmd3.CommandText = "SELECT color FROM [dbo].[StrategyPoint] WHERE RoadmapName =@Rname AND Name = @sName";
                    cmd3.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd3.Parameters.AddWithValue("@Sname", mName);
                    cmd3.Connection = conn;

                    try
                    {
                        using (SqlDataReader Reader = cmd3.ExecuteReader())
                        {
                            Reader.Read();

                            mColor = Reader.GetString(0);
                        }
                    }
                    catch
                    {
                        mColor = "#000000";
                    }
                }

                foreach (BusinessValue bv in mValues)
               {
                   using (SqlCommand cmd2 = new SqlCommand())
                   {
                       cmd2.CommandText = "SELECT Description FROM [dbo].[BusinessValue] WHERE Name=@BVName AND RoadmapName=@Rname";
                       cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);
                       cmd2.Parameters.AddWithValue("@BVName", bv.GetName());
                       cmd2.Connection = conn;

                       //conn.Open();
                        using (SqlDataReader Reader = cmd2.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                               Reader.Read();
                               bv.SetDescription(Reader.GetString(0).ToString());
                            }
                        }
                        conn.Close();
                   }
               }
                
            }
        }

        //Getters
        public string GetName() { return mName; }
        public void SetName(string name) { mName = name; }
        public string GetDescription() { return mDescription; }
        public string GetColor() { return mColor; }
        public List<BusinessValue> GetBusinessValues() { return mValues; }

        //Edit name of spoint
        public bool EditName(string name)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[StrategyPoint] SET Name = @Sname WHERE Name =@oldname AND RoadmapName =@Rname";
                    cmd.Parameters.AddWithValue("@Sname", name);
                    cmd.Parameters.AddWithValue("@oldname", mName);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        mName = name;
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        public bool EditColor(string color)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[StrategyPoint] SET color = @col WHERE Name = @Sname AND RoadmapName =@Rname";
                    cmd.Parameters.AddWithValue("@col", color);
                    cmd.Parameters.AddWithValue("@Sname", mName);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        mColor = color;
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
            return toReturn;
        }

        public bool EditDescription(string desc)
        {
            bool toReturn = false;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE [dbo].[StrategyPoint] SET Description = @desc WHERE Name = @Sname AND RoadmapName =@Rname";
                    cmd.Parameters.AddWithValue("@desc", desc);
                    cmd.Parameters.AddWithValue("@Sname", mName);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Connection = conn;

                    conn.Open();
                    if (cmd.ExecuteNonQuery()!=0)
                    {
                        mDescription = desc;
                        toReturn = true;
                    }
                    conn.Close();
                }
            }
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
            try
            {
                bool flag;
                //Add to both the BV table, and the BV/SP ownership table

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
                {
                    using (SqlCommand cmd1 = new SqlCommand())
                    {
                        cmd1.CommandText = "INSERT INTO [dbo].[BusinessValue] (Name, Description, RoadmapName) VALUES (@BVName, @desc,@Rname)";
                        cmd1.Parameters.AddWithValue("@BVName", name);
                        cmd1.Parameters.AddWithValue("@desc", desc);
                        cmd1.Parameters.AddWithValue("@Rname", mRoadmapName);
                        cmd1.Connection = conn;

                        conn.Open();
                        flag = cmd1.ExecuteNonQuery() != 0;
                        conn.Close();
                    }
                    using (SqlCommand cmd2 = new SqlCommand())
                    {
                        cmd2.CommandText = "INSERT INTO [dbo].[SP_BV_Crosswalk] (StrategyPointName, BusinessValueName, RoadmapName) VALUES (@Sname,@BVName,@Rname)";
                        cmd2.Parameters.AddWithValue("@BVName", name);
                        cmd2.Parameters.AddWithValue("@Sname", mName);
                        cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);
                        cmd2.Connection = conn;

                        conn.Open();
                        flag = cmd2.ExecuteNonQuery() != 0;
                        conn.Close();
                    }
                }
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
            bool flag2, flag3;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd1 = new SqlCommand())
                {
                    cmd1.CommandText = "DELETE FROM [dbo].[BusinessValue] WHERE Name=@BVName AND RoadmapName=@Rname";
                    cmd1.Parameters.AddWithValue("@BVName", name);
                    cmd1.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd1.Connection = conn;

                    flag2 = cmd1.ExecuteNonQuery() != 0;
                }

                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "DELETE FROM [dbo].[SP_BV_Crosswalk] WHERE BusinessValueName=@BVName AND StrategyPointName=@Sname AND RoadmapName=@Rname";
                    cmd2.Parameters.AddWithValue("@BVName", name);
                    cmd2.Parameters.AddWithValue("@Sname", mName);
                    cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd2.Connection = conn;

                    flag3 = cmd2.ExecuteNonQuery() != 0;
                }
                conn.Close();
            }

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

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Description FROM [dbo].[BusinessValue] WHERE Name=@sname AND RoadmapName=@Rname";
                    cmd.Parameters.AddWithValue("@sname", selectname);
                    cmd.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader =cmd.ExecuteReader())
                    {
                        if (Reader.HasRows)
                        {
                            Reader.Read();
                            nextdesc = Reader.GetString(0);
                        }
                    }
                    conn.Close();
                }
            }


            BusinessValue next = new BusinessValue(nextID, mRoadmapName);
            next.SetDescription(nextdesc);
            BusinessValue nextdummy = new BusinessValue(currname, mRoadmapName);
            nextdummy.SetDescription(nextdesc);
            if (nextdesc != null)
            {
                nextdummy.SetName(nextID);
                ReorderBusinessValue(nextID, nextdesc, false);
            }
        }

        //Reload list of BV's
        public void ReloadBusinessValues()
        {
            mValues = new List<BusinessValue>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd1 = new SqlCommand())
                {
                    cmd1.CommandText = "SELECT BusinessValueName FROM [dbo].[SP_BV_Crosswalk] WHERE RoadmapName = @Rname AND StrategyPointName =@Sname";
                    cmd1.Parameters.AddWithValue("@Rname", mRoadmapName);
                    cmd1.Parameters.AddWithValue("@Sname", mName);
                    cmd1.Connection = conn;

                    conn.Open();
                    using (SqlDataReader Reader = cmd1.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            string temp = Reader.GetString(0);
                            BusinessValue bv = new BusinessValue(Reader.GetString(0), mRoadmapName);
                            mValues.Add(bv);
                        }
                    }
                }



                foreach (BusinessValue bv in mValues)
                {
                    using (SqlCommand cmd2 = new SqlCommand())
                    {
                        cmd2.CommandText = "SELECT Description FROM [dbo].[BusinessValue] WHERE Name=@BVName AND RoadmapName=@Rname";
                        cmd2.Parameters.AddWithValue("@Rname", mRoadmapName);
                        cmd2.Parameters.AddWithValue("@BVName", bv.GetName());
                        cmd2.Connection = conn;

                        using (SqlDataReader Reader = cmd2.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                Reader.Read();
                                bv.SetDescription(Reader.GetString(0).ToString());
                                Reader.Close();
                            }
                        }
                    }
                }
                conn.Close();
            }

        }

        private string mName;
        private string mDescription;
        private string mRoadmapName;
        private string mColor;
        private List<BusinessValue> mValues = new List<BusinessValue>();

      //  private RocketRoadmap.DB.Database mDatabase = new RocketRoadmap.DB.Database();
     //   private SqlDataReader mReader;
    }
}