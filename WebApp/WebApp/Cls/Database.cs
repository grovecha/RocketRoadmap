﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.Cls
{
    public class Database
    {
        public SqlConnection connect()
        {
            string connstring=ConfigurationManager.ConnectionStrings["QLDB"].ToString();
            SqlConnection conn= new SqlConnection(connstring);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("error");
            }
            return conn;
        }

        public string getusername()
        {
            SqlConnection conn= connect();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT Name from User WHERE Email='bpchiv@gmail.com'";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();
            string name = reader.GetString(0);
            conn.Close();
            return name;
        }
    }
}