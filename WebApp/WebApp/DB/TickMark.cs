using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DB
{
    public class TickMark
    {
        public TickMark(string name, int x)
        {
            mName = name;
            mXPlacement = x;
        }

        public string GetName() { return mName; } //label for the tickmark (ex. December)
        public int GetXPlacement() { return mXPlacement; } //Used for placement on the timeline

        private string mName;
        private int mXPlacement;

    }
}