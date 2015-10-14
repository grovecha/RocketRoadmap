using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using RocketRoadmap.DB;

namespace RocketRoadmap
{
    public partial class Roadmap : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static void AddStrat(string id, string name,string mapName)
        {
            
            RoadMap map = new RoadMap(mapName);

            int n = map.GetStrategyPoints().Count;

            StrategyPoint point = new StrategyPoint(id, name);
            string fart = point.GetName();
            map.AddStrategyPoint(point);

        }
        [WebMethod]
        public static void AddBusVal(string id,string name,string mapName,string stratID)
        {
            RoadMap map = new RoadMap(mapName);

            StrategyPoint point = map.GetPoint(stratID);

            BusinessValue newBusVal = new BusinessValue(id);

            //point.AddBusinessValue(newBusVal);
        }
        [WebMethod]
        public static void AddProject(string id,string name,string mapName,string stratID,string valID)
        {
            RoadMap map = new RoadMap(mapName);

            StrategyPoint point = map.GetPoint(stratID);

            BusinessValue val = point.GetVal(valID);

            Project newProj = new Project(id, name, valID);

            


            //val.addProject(newProj);
        }




    }

    public class Point
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}