﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.DB
{
    public class User
    {
       public User( string name, string username, string email, string password )
        {
            mName = name;
            mUserName = username;
            mEmail = email;
            mPassword = password;
        }

        public bool EditName(string name)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("UPDATE [dbo].[User] SET Name = '" + name + "' WHERE ID = '" + mUserName + "'"))
            {
                mName = name;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public bool EditEmail(string mail)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("UPDATE [dbo].[User] SET Email = '" + mail + "' WHERE ID = '" + mUserName + "'"))
            {
                mEmail = mail;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public bool EditPassword(string pass)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("UPDATE [dbo].[User] SET Password = '" + pass + "' WHERE ID = '" + mUserName + "'"))
            {
                mPassword = pass;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public string GetName(){ return mName; }
        public string GetUserName() { return mUserName; }
        public string GetEmail() { return mEmail; }
        public string GetPassword() { return mPassword; }

        private string mName;
        private string mUserName;
        private string mEmail;
        private string mPassword;

        private WebApp.DB.Database mDatabase = new WebApp.DB.Database();
        private SqlDataReader mReader;
    }

}