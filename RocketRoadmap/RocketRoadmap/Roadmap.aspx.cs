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
            string name= Request.QueryString["n"];
            Page.Title=Request.QueryString["n"];
        }

        [System.Web.Services.WebMethod]
        public static void AddStrat(string id, string name,string mapName)
        {
            
            RoadMap map = new RoadMap(mapName);

            int n = map.GetStrategyPoints().Count;

            StrategyPoint point = new StrategyPoint(id, name);
            string fart = point.GetName();
            map.AddStrategyPoint(point);


            
            
            
        }




    }

    public class Point
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}