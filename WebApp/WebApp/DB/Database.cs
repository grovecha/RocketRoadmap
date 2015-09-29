using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.DB
{
    public class Database
    {
        //Connection
        private SqlConnection mConnection;

        //Actually initiates connection to database
        public void connect()
        {
            string connstring=ConfigurationManager.ConnectionStrings["QLDB"].ToString();
            SqlConnection conn= new SqlConnection(connstring);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("SQL Connection Error");
            }
            mConnection= conn;
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

        //Executes a write command (UPDATE, INSERT, DELETE....)
        public bool executewrite(string command)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = command;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = mConnection;

            return (cmd.ExecuteNonQuery()!=0);
        }
    }
}