using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using RocketRoadmap.DB;
using System.Web.UI.HtmlControls;

namespace RocketRoadmap
{
    public partial class Roadmap : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            string url = Request.Url.AbsoluteUri;
            int index = url.IndexOf("=");
            string name = url.Substring(index+1);

            RoadMap roadmap = new RoadMap(name);

            List<StrategyPoint> strats = roadmap.GetStrategyPoints();

            HtmlTable table = FindControlRecursive(this, "roadmap-table") as HtmlTable;

            int count = 0;
            foreach (StrategyPoint p in strats)
            {
                HtmlTableRow row = new HtmlTableRow();
                row.ID = "StratVisual" + count.ToString() + "Row";

                HtmlInputButton but = new HtmlInputButton();
                but.Name = "Strat";
                but.ID = "StratBut" + count.ToString();
                but.Style.Add(HtmlTextWriterStyle.BackgroundColor, "red");
                but.Style.Add(HtmlTextWriterStyle.Height, "100px");
                but.Style.Add(HtmlTextWriterStyle.Width, "200px");


                HtmlTableCell cell = new HtmlTableCell();

                row.Cells.Add(cell);

                cell.Controls.Add(but);

                table.Rows.Add(row);

            }







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

        [WebMethod]
        public static string GetProjectDescription(string ProjectID, string RoadmapName)
        {
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(pointindex, valindex);
            string pro = ProjectID.Substring(valindex, -1);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(pro);

            return newproj.GetDescription();
        }


        [WebMethod]
        public static List<Project> GetProjectDependencyText(string ProjectID, string RoadmapName)
        {
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(pointindex, valindex);
            string pro = ProjectID.Substring(valindex, -1);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(pro);

            return newproj.GetDependencies();
        }


        //[WebMethod]
        //public static string GetProjectDependencyProject(string ProjectID, string RoadmapName)
        //{
        //    RoadMap map = new RoadMap(RoadmapName);


        //    Project proj;

        //    return proj.GetDescription();
        //}

        [WebMethod]
        public static List<Issue> GetProjectRisk(string ProjectID, string RoadmapName)
        {
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(pointindex, valindex);
            string pro = ProjectID.Substring(valindex, -1);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(pro);

            return newproj.GetIssues();
        }

        [WebMethod]
        public static List<Link> GetProjectLinks(string ProjectID, string RoadmapName)
        {
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(pointindex, valindex);
            string pro = ProjectID.Substring(valindex, -1);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(pro);

            return newproj.GetLinks();
        }

        public Control FindControlRecursive(Control control, string id)
        {
            if (control == null) return null;
            //try to find the control at the current level
            Control ctrl = control.FindControl(id);

            if (ctrl == null)
            {
                //search the children
                foreach (Control child in control.Controls)
                {
                    ctrl = FindControlRecursive(child, id);

                    if (ctrl != null) break;
                }
            }
            return ctrl;
        }
    }

}



    public class Point
    {
        public string id { get; set; }
        public string name { get; set; }
    }
