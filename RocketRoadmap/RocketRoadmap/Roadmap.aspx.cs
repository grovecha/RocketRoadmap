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

            //User login check
            DB.User user = new DB.User((string)Session["username"], (string)Session["password"]);
            if (!user.Login())
            {
                Response.Redirect("index.aspx", false);
            }
           
            
            string url = Request.Url.AbsoluteUri;
            int index = url.IndexOf("=");
            string name = url.Substring(index + 1);

            roadmapnamelabel.InnerText = name;
            RoadMap roadmap = new RoadMap(name);

            List<StrategyPoint> strats = roadmap.GetStrategyPoints();


            HtmlTable table = FindControl("roadmapTable") as HtmlTable;

            int count = 0;
            HtmlInputText lasttext = new HtmlInputText();
            HtmlInputText busVal = new HtmlInputText();
            HtmlTable newtable = new HtmlTable();
            HtmlTable lastTable = new HtmlTable();


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


                cell1.Controls.Add(new LiteralControl("<br />"));


                lastTable = newtable;
                newtable = new HtmlTable();
                newtable.ID = "StratBox" + count.ToString() + "Table";

                cell1.Controls.Add(newtable);

                HtmlTableRow stratTableRow = new HtmlTableRow();

                stratTableRow.ID = "StratBox" + count.ToString() + "BusBox0Row";

                newtable.Rows.Add(stratTableRow);

                HtmlTableCell stratCell = new HtmlTableCell();

                stratTableRow.Cells.Add(stratCell);


                #region Business Values
                int valcount = 0;

                HtmlTable StratVisTable = new HtmlTable();
                StratVisTable.ID = p.GetName() + "VisualTable";
                HtmlInputText lastBusVal = new HtmlInputText();
                HtmlTable BusTable = new HtmlTable();
                foreach (BusinessValue b in p.GetBusinessValues())
                {
                    HtmlTableCell bc1 = new HtmlTableCell();
                    HtmlTableCell bc2 = new HtmlTableCell();

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


                        bc1.ID = b.GetName() + "td";
                        bc1.Style.Add(HtmlTextWriterStyle.Padding, "0");

                        visRow.Cells.Add(bc1);
                        visRow.Cells.Add(new HtmlTableCell());
                        visRow.Cells.Add(new HtmlTableCell());

                        bc2 = new HtmlTableCell();

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

                        bc1 = new HtmlTableCell();
                        bc1.ID = b.GetName() + "td";

                        newPRow.Cells.Add(bc1);

                        newPRow.Cells.Add(new HtmlTableCell());
                        newPRow.Cells.Add(new HtmlTableCell());



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
                    }
                    else
                    {
                        bustextbox = busVal;
                        bustextbox.Value = b.GetDescription();
                    }

                    if (count == 1)
                    {
                        BusTable = StratBox0Table;
                    }
                    else
                    {
                        BusTable = lastTable;
                    }

                    valcount++;

                    HtmlTableRow NextRow = new HtmlTableRow();

                    NextRow.ID = "StratBox" + count.ToString() + "BusBox" + valcount.ToString() + "ROW";

                    HtmlTableCell NextInputCell = new HtmlTableCell();

                    NextInputCell.ID = "StratBox" + count.ToString() + "BusBox" + valcount.ToString() + "inputtd";

                    HtmlInputText NextBox = new HtmlInputText();

                    NextBox.Attributes.Add("class", "txtBus");
                    NextBox.Attributes.Add("ProjTotal", "1");
                    NextBox.ID = "StratBox" + count.ToString() + "BusBox" + valcount.ToString();
                    NextBox.Attributes.Add("placeholder", "Add Business Value");
                    NextBox.Attributes.Add("runat", "server");
                    NextBox.Attributes.Add("onkeyup", "addBus(event,this," + valcount.ToString() + ")");

                    BusTable.Rows.Add(NextRow);
                    NextRow.Cells.Add(NextInputCell);
                    NextInputCell.Controls.Add(NextBox);

                    HtmlInputText nextText = new HtmlInputText();
                    

                    int projCount = 0;
                    foreach(Project proj in b.GetProjects())
                    {



                        //< input type = "button" id = "StratBox0BusBox0ProjBox0But" value = "proj1" onclick = "showModal(this.id)" class="proj1" style="height: 33px; width: 150px; vertical-align: top; background-color: green;">
                        HtmlInputButton projBut = new HtmlInputButton();
                        projBut.ID = proj.GetName() + "But";
                        projBut.Attributes.Add("value", "proj"+(projCount+1).ToString());
                        projBut.Attributes.Add("onclick", "showModal(this.id)");
                        projBut.Attributes.Add("class", "proj"+ (projCount + 1).ToString());
                        projBut.Attributes.Add("style", "height: 33px; width: 150px; vertical-align: top; background-color: green;");
                        projBut.Value = proj.GetDescription();
                        bc1.Controls.Add(projBut);

                        HtmlInputText projTextBox = new HtmlInputText();

                        if(count==1 && valcount==1 && projCount==0)
                        {
                            StratBox0BusBox0ProjBox0.Value = proj.GetDescription();
                        }
                        else
                        {
                            projTextBox.Value = proj.GetDescription();
                        }

                        projCount++;
                    }

                    nextText.Name = "DynmaicTextBox";
                    nextText.ID = "StratBox" + count.ToString() + "BusBox" + valcount.ToString() + "ProjBox0";
                    nextText.Attributes.Add("class", "txtProj");
                    nextText.Attributes.Add("placeholder", "Add Project");
                    nextText.Attributes.Add("runat", "server");
                    nextText.Attributes.Add("onkeyup", "addProj(event,this," + count.ToString() + ")");
                    NextInputCell.Controls.Add(nextText);
                    NextInputCell.Controls.Add(new LiteralControl("<br />"));


                    busVal = NextBox;

                }
                #endregion

                busVal = new HtmlInputText();


                busVal.Attributes.Add("class", "txtBus");
                busVal.Attributes.Add("ProjTotal", "1");
                busVal.ID = "StratBox" + count.ToString() + "BusBox0";
                busVal.Attributes.Add("placeholder", "Add Business Value");
                busVal.Attributes.Add("runat", "server");
                busVal.Attributes.Add("onkeyup", "addBus(event,this," + count.ToString() + ")");

                stratCell.Controls.Add(busVal);



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

                //hide example
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "hideExample();", true);

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
        public static void EditStrat(string id, string name, string mapName)
        {
            RoadMap map = new RoadMap(mapName);
            map.GetPoint(id).EditDescription(name);
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
        public static void EditBusVal(string id, string name, string mapName, string stratID)
        {
            int pointindex = id.IndexOf("Bus");
            string point = id.Substring(0, pointindex);
            RoadMap map = new RoadMap(mapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(id);
            newval.SetDescription(name);
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

        [WebMethod]
        public static void EditProject(string id, string name, string mapName, string stratID, string valID)
        {
            int pointindex = id.IndexOf("Bus");
            int valindex = id.IndexOf("Proj");
            string point = id.Substring(0, pointindex);
            //string val = id.Substring(pointindex, valindex);
            //string pro = id.Substring(valindex, -1);
            RoadMap map = new RoadMap(mapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(valID);
            Project newproj = newval.GetProject(id);
            newproj.SetDescription(name);
        }

        [WebMethod]
        public static string GetValue(string id, string mapName)
        {
            RoadMap map = new RoadMap(mapName);
            return map.GetPoint(id).GetDescription();

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
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);

            newproj.SetModalDescription(desc);
        }

        //Set String Dependency
        [WebMethod]
        public static void SetProjectStrDependency(string ProjectID, string RoadmapName, List<string> dep)
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
            Dep_Names = newproj.GetDependantStrings();

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

            newproj.UpdateDependantStrings(Dep_Names);


        }

        //Set Project Dependency 
        [WebMethod]
        public static void SetProjectDependency(string ProjectID, string RoadmapName, List<string> dep)
        {
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);


            for (int i = 0; i < dep.Count(); i++)
            {
               // newproj.CreateDependant(dep[i]);
            }
        }

        //Set Project Risk
        [WebMethod]
        public static void SetProjectRisk(string ProjectID, string RoadmapName, string risk)
        {
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);

            newproj.SetProjectRisks(risk);

        }

        //Set Project Link
        [WebMethod]
        public static void SetProjectLink(string ProjectID, string RoadmapName, List<string> link)
        {
            List<Link> link_list = new List<Link>();
            List<string> linkstr_list = new List<string>();
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);
            linkstr_list = GetProjectLinks(ProjectID, RoadmapName);

            //need to take the list and check for new ones and create links
            //foreach (string l in link)
            //    {
            //        if (!linkstr_list.Contains(l))
            //        {

            //            Dep_Names.Add(l);
            //        }
            //    }

            //foreach (string s in Dep_Names)
            //{
            //    if (!dep.Contains(s))
            //    {
            //        Dep_Names.Remove(s);
            //    }
            //}

        }
        //Getting all projects
        [WebMethod]
        public static List<List<string>> GetAllRoadmapProjects(string RoadmapName)
        {
            RoadMap map = new RoadMap(RoadmapName);
            List<List<string>> test = new List<List<string>>();
            test = map.GetAllProjects();
            return test;
        }

        #endregion

    }

}




