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
                string name = url.Substring(index + 1);

                RoadMap roadmap = new RoadMap(name);

                List<StrategyPoint> strats = roadmap.GetStrategyPoints();


                HtmlTable table = FindControl("roadmapTable") as HtmlTable;

                int count = 0;
                HtmlInputText lasttext = new HtmlInputText();

                foreach (StrategyPoint p in strats)
            {

                #region Strategy Visual Creation
                HtmlTableRow row;

                row = new HtmlTableRow();


                row.ID = "StratVisual" + count.ToString() + "Row";

                HtmlInputButton but = new HtmlInputButton();
                but.Name = "Strat";
                but.ID = "StratBut" + count.ToString();
                but.Style.Add(HtmlTextWriterStyle.BackgroundColor, "red");
                but.Style.Add(HtmlTextWriterStyle.Height, "100px");
                but.Style.Add(HtmlTextWriterStyle.Width, "200px");
                but.Value = p.GetDescription();

                HtmlInputText textbox;
                if (count == 0)
                {
                    textbox = StratBox0;
                }
                else
                {
                    textbox = lasttext;
                }

<<<<<<< HEAD

                //textbox.Value = p.GetDescription();

=======
                //textbox.Value = p.GetDescription();
>>>>>>> ce04e15de3f04da2087c62da690175b8bfc59ce6
                #endregion


                #region Strategy Text Box Creation
                textbox.Value = p.GetDescription();
<<<<<<< HEAD

=======
>>>>>>> ce04e15de3f04da2087c62da690175b8bfc59ce6


                HtmlTableCell cell = new HtmlTableCell();

                row.Cells.Add(cell);
                cell.Controls.Add(but);
                table.Rows.Add(row);

                count++;

                HtmlTableRow lastRow = new HtmlTableRow();
                lastRow.ID = "StratBox" + count.ToString() + "Row";

                HtmlTableCell cell1 = new HtmlTableCell();

                HtmlInputText text = new HtmlInputText();
                lasttext = text;

                text.ID = "StratBox" + count.ToString();


                text.Attributes.Add("class", "txtStrat");
                text.Attributes.Add("BusTotal", "1");
                text.Attributes.Add("placeholder", "Add Strategy Point");
                text.Attributes.Add("runat", "server");
                text.Attributes.Add("onkeyup", "addStrat(event,this," + count.ToString() + ")");

                cell1.Controls.Add(text);

                HtmlButton deletebutton = new HtmlButton();

                deletebutton.Attributes.Add("class", "btnDelete");
                deletebutton.ID = "StratDelete" + count.ToString();
                deletebutton.Attributes.Add("onclick", "deleteStrat(event,this)");
                deletebutton.InnerText = "X";

                cell1.Controls.Add(deletebutton);

                cell1.Controls.Add(new LiteralControl("<br />"));

                HtmlTable newtable = new HtmlTable();

                newtable.ID = "StratBox" + count.ToString() + "Table";

                cell1.Controls.Add(newtable);

                //HtmlTableRow stratTableRow = new HtmlTableRow();

                //stratTableRow.ID = "StratBox" + count.ToString() + "BusBox0Row";

                //newtable.Rows.Add(stratTableRow);

                //HtmlTableCell stratCell = new HtmlTableCell();

                //stratTableRow.Cells.Add(stratCell);


                //HtmlInputText busVal = new HtmlInputText();

                //busVal.Attributes.Add("class", "txtBus");
                //busVal.Attributes.Add("ProjTotal", "1");
                //busVal.ID = "StratBox" + count.ToString() + "BusBox0";
                //busVal.Attributes.Add("placeholder", "Add Business Value");
                //busVal.Attributes.Add("runat", "server");
                //busVal.Attributes.Add("onkeyup", "addBus(event,this," + count.ToString() + ")");

                //stratCell.Controls.Add(busVal);

                //HtmlButton busDelete = new HtmlButton();
                //busDelete.Attributes.Add("class", "btnDelete");
                //busDelete.ID = "StratBox" + count.ToString() + "BusBox0Delete";
                //busDelete.Attributes.Add("onclick", "deleteBus(event,this)");
                //busDelete.InnerText = "X";

                //stratCell.Controls.Add(busDelete);

                //HtmlInputText projText = new HtmlInputText();

                //projText.Name = "DynmaicTextBox";
                //projText.ID = "StratBox" + count.ToString() + "BusBox0ProjBox0";
                //projText.Attributes.Add("class", "txtProj");
                //projText.Attributes.Add("placeholder", "Add Project");
                //projText.Attributes.Add("runat", "server");
                //projText.Attributes.Add("onkeyup", "addProj(event,this," + count.ToString() + ")");
                //stratCell.Controls.Add(projText);
                //stratCell.Controls.Add(new LiteralControl("<br />"));

                lastRow.Cells.Add(cell1);
                HtmlTable sideTable = FindControl("sidebarTable") as HtmlTable;

                sideTable.Rows.Add(lastRow);

                #endregion




            }

        }



        [WebMethod]
        public static void AddStrat(string id, string name,string mapName)
        {
            
            RoadMap map = new RoadMap(mapName);

            int n = map.GetStrategyPoints().Count;

            StrategyPoint point = new StrategyPoint(id, name, mapName);
            map.AddStrategyPoint(point);

        }
        [WebMethod]
        public static void AddBusVal(string id,string name,string mapName,string stratID)
        {
            RoadMap map = new RoadMap(mapName);

            StrategyPoint point = map.GetPoint(stratID);

            BusinessValue newBusVal = new BusinessValue(id, mapName);

            point.CreateBuisnessValue(id, name, mapName);

            //function to add to database

            
        }
        [WebMethod]
        public static void AddProject(string id,string name,string mapName,string stratID,string valID)
        {
            RoadMap map = new RoadMap(mapName);

            StrategyPoint point = map.GetPoint(stratID);

            BusinessValue val = point.GetBusinessValue(id);

            Project newProj = new Project(id, name, valID, mapName);

            val.AddProject(newProj);


            //val.addProject(newProj);
        }
        //Get Project Description
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

        //Get Project
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

        [WebMethod]
        public static void SetProjectDescription(string ProjectID, string RoadmapName, string desc)
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

            newproj.SetDescription(desc);
        }
        //Setting Dependency String
        public static void SetProjectStrDependency(string ProjectID, string RoadmapName, List<string> dep)
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

            for(int i = 0; i < dep.Count(); i++)
            {
                string strdep = dep[i].ToString();
                //newproj.CreateStrDependant(strdep);
            }
        }
        //Setting Dependency Project
        public static void SetProjectDependency(string ProjectID, string RoadmapName, List<Project> dep)
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

            for (int i = 0; i < dep.Count(); i++)
            { 
                newproj.CreateDependant(dep[i]);
            }
        }

        //Setting Project Risk
        public static void SetProjectRisk(string ProjectID, string RoadmapName, Issue risk)
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

            newproj.CreateIssue(risk);
            
        }

        //Setting Project Risk
        public static void SetProjectLink(string ProjectID, string RoadmapName, Link link)
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

            newproj.CreateLink(link);

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
