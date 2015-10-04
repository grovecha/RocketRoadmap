using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DB
{
    public class StrategyPoint
    {
        public StrategyPoint(string name, string desc) 
        {
            mName = name;
            mDescription = desc;
        }

        public string GetName() { return mName; }
        public string GetDescription() { return mDescription; }


        private string mName;
        private string mDescription;
    }
}