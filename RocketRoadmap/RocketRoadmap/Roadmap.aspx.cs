using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RocketRoadmap.DB;

namespace RocketRoadmap
{
    public partial class Roadmap : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [System.Web.Services.WebMethod]
        public static void AddStrat(string name, string desc,string mapName)
        {

            RoadMap map = new RoadMap(mapName);

            StrategyPoint point = new StrategyPoint(name, desc);

            map.AddStrategyPoint(point);
            
            
        }




    }

    public class Point
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}