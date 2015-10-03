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
        private SqlConnection mConnection;

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

        public void close(SqlConnection conn)
        {
            mConnection.Close();
        }

        public SqlDataReader execute(string command)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = command;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = mConnection;

            reader = cmd.ExecuteReader();

            return reader;
        }

        public string getusername()
        {
            SqlConnection conn= connect();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT Name from [dbo].[User] WHERE Email='bpchiv@gmail.com'";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();
            string name= "";
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
    }
}