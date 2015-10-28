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
            int index = url.IndexOf("=");
            string name = url.Substring(index + 1);

            RoadMap roadmap = new RoadMap(name);

            List<StrategyPoint> strats = roadmap.GetStrategyPoints();


            HtmlTable table = FindControl("roadmapTable") as HtmlTable;

            int count = 0;
            HtmlInputText lasttext = new HtmlInputText();
            HtmlInputText busVal = new HtmlInputText();

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




                //textbox.Value = p.GetDescription();


                //textbox.Value = p.GetDescription();

                #endregion


                #region Strategy Text Box Creation
                textbox.Value = p.GetDescription();



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

                //deletebutton.Attributes.Add("class", "btnDelete");
                //deletebutton.ID = "StratDelete" + count.ToString();
                //deletebutton.Attributes.Add("onclick", "deleteStrat(event,this)");
                //deletebutton.InnerText = "X";


                //cell1.Controls.Add(deletebutton);

                cell1.Controls.Add(new LiteralControl("<br />"));

                HtmlTable newtable = new HtmlTable();

                newtable.ID = "StratBox" + count.ToString() + "Table";

                cell1.Controls.Add(newtable);

                HtmlTableRow stratTableRow = new HtmlTableRow();

                stratTableRow.ID = "StratBox" + count.ToString() + "BusBox0Row";

                newtable.Rows.Add(stratTableRow);

                HtmlTableCell stratCell = new HtmlTableCell();

                stratTableRow.Cells.Add(stratCell);

                int valcount = 0;

                HtmlTable StratVisTable = new HtmlTable();
                StratVisTable.ID = p.GetName() + "VisualTable";
                HtmlInputText lastBusVal = new HtmlInputText();
                foreach (BusinessValue b in p.GetBusinessValues())
                {
                    if (valcount == 0)
                    {
                        HtmlTableCell sCell = new HtmlTableCell();
                        sCell.Style.Add(HtmlTextWriterStyle.Width, "3000px");
                        sCell.Style.Add(HtmlTextWriterStyle.BackgroundColor, "yellow");

                        row.Cells.Add(sCell);


                        sCell.Controls.Add(StratVisTable);

                        HtmlTableRow visRow = new HtmlTableRow();

                        visRow.Attributes.Add("style", "height:100px; border-bottom:1pt solid black;");
                        StratVisTable.Rows.Add(visRow);

                        HtmlTableCell bc1 = new HtmlTableCell();
                        bc1.ID = b.GetName() + "td";
                        bc1.Style.Add(HtmlTextWriterStyle.Padding, "0");

                        visRow.Cells.Add(bc1);
                        visRow.Cells.Add(new HtmlTableCell());
                        visRow.Cells.Add(new HtmlTableCell());

                        HtmlTableCell bc2 = new HtmlTableCell();

                        bc2.ID = p.GetName() + "BusVisual" + valcount.ToString();
                        bc2.Attributes.Add("style", "width:1000px; text-align:right; background-color:yellow; padding:0");

                        bc2.InnerText = b.GetDescription();

                        visRow.Cells.Add(bc2);


                    }
                    else
                    {

                        HtmlTableRow newPRow = new HtmlTableRow();
                        StratVisTable.Rows.Add(newPRow);
                        newPRow.Attributes.Add("style", "height:100px;border-bottom: 1pt solid black;");

                        HtmlTableCell bc1 = new HtmlTableCell();
                        bc1.ID = b.GetName() + "td";

                        newPRow.Cells.Add(bc1);

                        newPRow.Cells.Add(new HtmlTableCell());
                        newPRow.Cells.Add(new HtmlTableCell());

                        HtmlTableCell bc2 = new HtmlTableCell();

                        bc2.ID = p.GetName() + "BusVisual" + valcount.ToString();
                        bc2.Attributes.Add("style", "width:1000px; text-align:right; background-color:yellow; padding:0");

                        bc2.InnerText = b.GetDescription();

                        newPRow.Cells.Add(bc2);

                        int h = valcount * 100 + 100;
                        but.Style.Add(HtmlTextWriterStyle.Height, h.ToString() + "px");



                    }

                    HtmlInputText bustextbox;
                    if (count == 1 && valcount == 0)
                    {
                        bustextbox = StratBox0BusBox0;

                        bustextbox.Value = b.GetDescription();
                        valcount++;
                    }
                    else
                    {
                        bustextbox = busVal;



                        bustextbox.Value = b.GetDescription();
                        valcount++;

                        //HtmlTableRow stratTableRow2 = new HtmlTableRow();

                        //stratTableRow2.ID = "StratBox" + count.ToString() + "BusBox" + valcount.ToString() + "Row";

                        //newtable.Rows.Add(stratTableRow2);

                        //HtmlTableCell stratCel2 = new HtmlTableCell();

                        //stratTableRow.Cells.Add(stratCel2); stratTableRow = new HtmlTableRow();

                        //stratTableRow2.ID = "StratBox" + count.ToString() + "BusBox" + valcount.ToString() + "Row";

                        //newtable.Rows.Add(stratTableRow2);

                        //HtmlTableCell stratCell2 = new HtmlTableCell();

                        //stratTableRow2.Cells.Add(stratCell2);

                        //busVal = new HtmlInputText();


                        //busVal.Attributes.Add("class", "txtBus");
                        //busVal.Attributes.Add("ProjTotal", "1");
                        //busVal.ID = "StratBox" + count.ToString() + "BusBox" + valcount.ToString();
                        //busVal.Attributes.Add("placeholder", "Add Business Value");
                        //busVal.Attributes.Add("runat", "server");
                        //busVal.Attributes.Add("onkeyup", "addBus(event,this," + count.ToString() + ")");

                        //stratCell2.Controls.Add(busVal);


                    }


                    //LoadBusVal(b, p,table);
                }

                busVal = new HtmlInputText();


                busVal.Attributes.Add("class", "txtBus");
                busVal.Attributes.Add("ProjTotal", "1");
                busVal.ID = "StratBox" + count.ToString() + "BusBox0";
                busVal.Attributes.Add("placeholder", "Add Business Value");
                busVal.Attributes.Add("runat", "server");
                busVal.Attributes.Add("onkeyup", "addBus(event,this," + count.ToString() + ")");

                stratCell.Controls.Add(busVal);

                //HtmlButton busDelete = new HtmlButton();
                //busDelete.Attributes.Add("class", "btnDelete");
                //busDelete.ID = "StratBox" + count.ToString() + "BusBox0Delete";
                //busDelete.Attributes.Add("onclick", "deleteBus(event,this)");
                //busDelete.InnerText = "X";

                //stratCell.Controls.Add(busDelete);

                HtmlInputText projText = new HtmlInputText();

                projText.Name = "DynmaicTextBox";
                projText.ID = "StratBox" + count.ToString() + "BusBox0ProjBox0";
                projText.Attributes.Add("class", "txtProj");
                projText.Attributes.Add("placeholder", "Add Project");
                projText.Attributes.Add("runat", "server");
                projText.Attributes.Add("onkeyup", "addProj(event,this," + count.ToString() + ")");
                stratCell.Controls.Add(projText);
                stratCell.Controls.Add(new LiteralControl("<br />"));

                lastRow.Cells.Add(cell1);
                HtmlTable sideTable = FindControl("sidebarTable") as HtmlTable;

                sideTable.Rows.Add(lastRow);

                #endregion





            }

        }

        #region Adding functions

        [WebMethod]
        public static void AddStrat(string id, string name, string mapName)
        {

            RoadMap map = new RoadMap(mapName);

            int n = map.GetStrategyPoints().Count;

            StrategyPoint point = new StrategyPoint(id, name, mapName);
            map.AddStrategyPoint(point);

        }

        [WebMethod]
        public static void AddBusVal(string id, string name, string mapName, string stratID)
        {
            RoadMap map = new RoadMap(mapName);

            StrategyPoint point = map.GetPoint(stratID);

            BusinessValue newBusVal = new BusinessValue(id, mapName);

            point.CreateBuisnessValue(id, name, mapName);

            //function to add to database



        }
        [WebMethod]
        public static void AddProject(string id, string name, string mapName, string stratID, string valID)
        {
            RoadMap map = new RoadMap(mapName);

            StrategyPoint point = map.GetPoint(stratID);

            BusinessValue val = point.GetBusinessValue(valID);

            Project newProj = new Project(id, name, valID, mapName);

            val.CreateNewProject(newProj);


            //val.addProject(newProj);
        }

        #endregion

        #region Modal Getters
        //Get Project Name
        [WebMethod]
        public static string GetProjectName(string ProjectID, string RoadmapName)
        {
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);

            return newproj.GetDescription();
        }


        //Get Project Description
        [WebMethod]
        public static string GetProjectDescription(string ProjectID, string RoadmapName)
        {

            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);

            return newproj.GetDescription();

        }

        //Get String Dependecies
        [WebMethod]
        public static List<string> GetProjectDependencyText(string ProjectID, string RoadmapName)
        {
            List<string> Dep_Names = new List<string>();
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);
            //Project_Names = newproj.GetStrDependencies();


            return Dep_Names;
        }
        
        //Get Project Depencies
        [WebMethod]
        public static List<string> GetProjectDependency(string ProjectID, string RoadmapName)
        {
            List<string> Project_Names = new List<string>();
            List<Project> Project_List = new List<Project>();
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(pointindex, valindex);
            string pro = ProjectID.Substring(valindex, -1);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(pro);
            Project_List = newproj.GetDependencies();
            int count = 0;

            foreach (Project p in Project_List)
            {
                Project_Names[count] = p.GetName();
                count++;
            }
            //Send name

            return Project_Names;
        }

        //Get Proejct Risks
        [WebMethod]
        public static string GetProjectRisk(string ProjectID, string RoadmapName)
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
            
            string risks="";//= newproj.GetIssues(); 

            return risks;
        }

        //Get Project Links
        [WebMethod]
        public static List<string> GetProjectLinks(string ProjectID, string RoadmapName)
        {
            List<string> Project_Links = new List<string>();
            List<Link> link_list = new List<Link>();
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(pointindex, valindex);
            string pro = ProjectID.Substring(valindex, -1);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(pro);
            link_list =  newproj.GetLinks();
            int count = 0;

            foreach(Link l in link_list){
                Project_Links[count] = l.GetLink().ToString();
                count++;
            }
            return Project_Links;
        }

        //Set Project Description
        #endregion

        #region Modal Setters
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

        //Set String Dependency
        public static void SetProjectStrDependency(string ProjectID, string RoadmapName, List<string> dep)
        {
            List<string> Dep_Names = new List<string>();

            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(pointindex, valindex);
            string pro = ProjectID.Substring(valindex, -1);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(pro);
            //Dep_Names = newproj.GetStrDependencies();

            foreach (string s in dep)
            {
                if (!Dep_Names.Contains(s))
                {
                    Dep_Names.Add(s);
                }
            }

            foreach (string s in Dep_Names)
            {
                if (!dep.Contains(s))
                {
                    Dep_Names.Remove(s);
                }
            }

            //newproj.SetProjectStrDependency();


        }
            
                //newproj.CreateStrDependant(strdep);
           
        //Set Project Dependency 
        public static void SetProjectDependency(string ProjectID, string RoadmapName, List<string> dep)
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
               // newproj.CreateDependant(dep[i]);
            }
        }

        //Set Project Risk
        public static void SetProjectRisk(string ProjectID, string RoadmapName, string risk)
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

            //newproj.SetRisk(risk);

        }

        //Set Project Link
        public static void SetProjectLink(string ProjectID, string RoadmapName, List<string> link)
        {
            List<Link> link_list = new List<Link>();
            List<string> linkstr_list = new List<string>();
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(pointindex, valindex);
            string pro = ProjectID.Substring(valindex, -1);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(pro);
            linkstr_list = GetProjectLinks(ProjectID, RoadmapName);

            //need to take the list and check for new ones and create links
            //foreach (string l  in link)
            //{
            //    if (!linkstr_list.Contains(l))
            //    {

            //        Dep_Names.Add(l);
            //    }
            //}

            //foreach (string s in Dep_Names)
            //{
            //    if (!dep.Contains(s))
            //    {
            //        Dep_Names.Remove(s);
            //    }
            //}

        }

        #endregion

    }

}




