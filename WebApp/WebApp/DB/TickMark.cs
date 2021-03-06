﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.DB
{
    public class TickMark
    {
        public TickMark(string name, int x)
        {
            mName = name;
            mXPlacement = x;
        }

        public bool EditTickName(string name, int id)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("UPDATE [dbo].[TickMark] SET Name = '" + name + "' WHERE TimeLineID = '" + id + "'" ))
            {
                mName = name;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public bool EditTickLocation(int X, int id)
        {
            mDatabase.connect();
            bool toReturn = false;

            if (mDatabase.executewrite("UPDATE[dbo].[TickMark] SET XPlacement = '" + X + "' WHERE TimeLineID = '" + id + "'" ))
            {
                mXPlacement = X;
                toReturn = true;
            }

            mDatabase.close();
            return toReturn;
        }

        public string GetName() { return mName; } //label for the tickmark (ex. December)
        public int GetXPlacement() { return mXPlacement; } //Used for placement on the timeline

        private string mName;
        private int mXPlacement;

        private WebApp.DB.Database mDatabase = new WebApp.DB.Database();
        private SqlDataReader mReader;
    }
}