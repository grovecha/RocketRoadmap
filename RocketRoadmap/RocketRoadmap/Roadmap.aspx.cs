using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using RocketRoadmap.DB;

namespace RocketRoadmap
{
    public partial class Roadmap : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            string url = Request.Url.AbsoluteUri;

            


           
        }

        [WebMethod]
        public static void AddStrat(string id, string name,string mapName)
        {
            
            RoadMap map = new RoadMap(mapName);

            int n = map.GetStrategyPoints().Count;

            StrategyPoint point = new StrategyPoint(id, name, mapName);
   //         map.AddStrategyPoint(point);

        }
        [WebMethod]
        public static void AddBusVal(string id,string name,string mapName,string stratID)
        {
            RoadMap map = new RoadMap(mapName);

            StrategyPoint point = map.GetPoint(stratID);

            BusinessValue newBusVal = new BusinessValue(id, mapName);

           // point.AddBusinessValue(newBusVal);
            //function to add to database

            
        }
        [WebMethod]
        public static void AddProject(string id,string name,string mapName,string stratID,string valID)
        {
            RoadMap map = new RoadMap(mapName);

            StrategyPoint point = map.GetPoint(stratID);

          //  BusinessValue val = point.GetVal(valID, mapName);

            Project newProj = new Project(id, name, valID, mapName);

           // val.AddProject(newProj);


            //val.addProject(newProj);
        }

        //[WebMethod]
        //public static string GetProjectDescription(string ProjectID,string RoadmapName)
        //{
        //    //RoadMap map = new RoadMap(RoadmapName);


        //    //Project proj;

        //    //return proj.GetDescription();
        //}


        //[WebMethod]
        //public static string GetProjectDependencyText(string ProjectID,string RoadmapName)
        //{
        //    //RoadMap map = new RoadMap(RoadmapName);


        //    //Project proj;

        //    //return proj.GetDescription();
        //}


        //[WebMethod]
        //public static string GetProjectDependencyProject(string ProjectID,string RoadmapName)
        //{
        //    //RoadMap map = new RoadMap(RoadmapName);


        //    //Project proj;

        //    //return proj.GetDescription();
        //}

        //[WebMethod]
        //public static string GetProjectRisk(string ProjectID,string RoadmapName)
        //{
        //    //RoadMap map = new RoadMap(RoadmapName);


        //    //Project proj;

        //    //return proj.GetDescription();
        //}

        //[WebMethod]
        //public static string GetProjectLinks(string ProjectID,string RoadmapName)
        //{
        //    //RoadMap map = new RoadMap(RoadmapName);


        //    //Project proj;

        //    //return proj.GetDescription();
        //}



    }

    public class Point
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}