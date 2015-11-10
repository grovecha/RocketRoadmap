using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap.DB
{
    public class Database
    {
        //Connection
        private SqlConnection mConnection;

        //Actually initiates connection to database
        public void connect()
        {
            string connstring = "Data Source=35.9.22.106;Initial Catalog=QuickenLoansDB;User ID=QuickenLoans;Password=ChaseSmells2015;Connect Timeout=60";
            SqlConnection conn = new SqlConnection(connstring);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("SQL Connection Error");
            }
            mConnection = conn;
        }

        //Closes database connection
        public void close()
        {
            mConnection.Close();
        }

        //Executes a read command (SELECT)
        public SqlDataReader executeread(string command)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = command;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = mConnection;

            reader = cmd.ExecuteReader();

            return reader;
        }

        //Executes read command (Safe Parameters)
        public SqlDataReader executereadparams(SqlCommand cmd){
            SqlDataReader reader;
            cmd.Connection = mConnection;


            reader = cmd.ExecuteReader();

            return reader;
    }

        //Executes a write command (UPDATE, INSERT, DELETE....)
        public bool executewrite(string command)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = command;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = mConnection;

            return (cmd.ExecuteNonQuery() != 0);
        }

        //Execute write command with parameters
        public bool executewriteparam(SqlCommand cmd)
        {

            cmd.Connection = mConnection;
            if (cmd.Parameters.Contains("@Rname"))
            {
              
                SqlCommand timestamp = new SqlCommand();
                timestamp.Connection = mConnection;
                timestamp.CommandText = "UPDATE [dbo].[Roadmap] SET Timestamp=getdate() WHERE Name=@Roadmap";
                timestamp.Parameters.AddWithValue("@Roadmap", cmd.Parameters["@Rname"].Value.ToString());

                int rows=timestamp.ExecuteNonQuery();

            }

            return (cmd.ExecuteNonQuery()!=0);
        }
    }
}