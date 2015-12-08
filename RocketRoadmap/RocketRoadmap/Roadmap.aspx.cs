using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using RocketRoadmap.DB;


namespace RocketRoadmap
{
    public partial class Roadmap : System.Web.UI.Page
    {
        List<string> colorList = new List<string> { "#DC381F", "#33cccc", "#6CBB3C", "#A23BEC", "#157DEC", "#F87217" };

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //User login check
            DB.User user = new DB.User((string)Session["username"], (string)Session["password"]);
            if (!user.Login())
            {
                Response.Redirect("index.aspx", false);
            }

            //getting map name
            string url = Request.Url.AbsoluteUri;
            int index = url.IndexOf("=");
            string name = HttpUtility.UrlDecode(url.Substring(index + 1));


            roadmapnamelabel.InnerText = name;
            RoadMap roadmap = new RoadMap(name);


            //getting list of strategy points
            List<StrategyPoint> strats = roadmap.GetStrategyPoints();

            // our main table 
            HtmlTable table = FindControl("roadmapTable") as HtmlTable;

            int count = 0;

            double totalHeight = 0;
            // the last textbox we made
            HtmlInputText lasttext = new HtmlInputText();
            // business value textbox
            HtmlInputText busVal = new HtmlInputText();

            //table that hold all text boxes for strategy point
            HtmlTable newtable = new HtmlTable();

            // our last table
            HtmlTable lastTable = new HtmlTable();

            // the project text box
            HtmlInputText projText = new HtmlInputText();

            // the next input cell we will use
            HtmlTableCell NextInputCell = new HtmlTableCell();

            HtmlTableCell mainTextCell = new HtmlTableCell();
            mainTextCell = null;
             
            // the delete hyperlink
            HyperLink delete = new HyperLink();
            #region Loading Strats, Vals, and Projects

            

            foreach (StrategyPoint p in strats)
            {

                
                /*
                *    Creating the visual strategy points
                */


                //making a row for this point
                HtmlTableRow row;
                row = new HtmlTableRow();
                row.ID = "StratVisual" + count.ToString() + "Row";
     

                // making the button
                HtmlInputButton but = new HtmlInputButton();
                but.Name = "Strat";
                but.ID = "StratBut" + count.ToString();
                // adding the style class
                but.Attributes.Add("class", "StratVis");

                //loading the color
                string color = p.GetColor();
                but.Attributes.Add("style", "background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, " + color + "), color-stop(1, " + color + ")); background:-moz-linear-gradient(top, " + color + " 5%, " + color + " 100%); background:-webkit-linear-gradient(top, " + color + " 5%, " + color + " 100%); background:-o-linear-gradient(top, " + color + " 5%, " + color + " 100%); background:-ms-linear-gradient(top, " + color + " 5%, " + color + " 100%); background:linear-gradient(to bottom, " + color + " 5%, " + color + " 100%); filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='" + color + "', endColorstr='" + color + "',GradientType=0);");
                but.Style.Add(HtmlTextWriterStyle.Height, "3.4em");
                if (count == 0)
                {
                    totalHeight = totalHeight + 5.2;
                }
                else
                {
                    totalHeight = totalHeight + 5;
                }
                but.Value = p.GetDescription();


                /*
                * Loading in info to edit panel
                */
                
                // setting the text box to edit
                HtmlInputText textbox;
                if (count == 0)
                {
                    textbox = StratBox0;
                    
                }
                else
                {
                    textbox = lasttext;
                }

                // setting correct text value
                textbox.Value = p.GetDescription();


                //allow deletion of all stratboxes except the last
                if (count<strats.Count())
                {
                    textbox.Attributes.Add("firstadd", "1");
                }

                //cell for text boxes
                HtmlTableCell cell = new HtmlTableCell();

                // adding in elementes to table
                row.Cells.Add(cell);
                cell.Controls.Add(but);
                table.Rows.Add(row);


                //increasing index to next point
                count++;

                //making a new row and setting it to last row so we can grab it in next loop
                HtmlTableRow lastRow = new HtmlTableRow();
                lastRow.ID = "StratBox" + count.ToString() + "Row";

                // if this is the first point set the first color box if not reset the elements and set the color for the last point
                if (mainTextCell == null)
                {
                    ColorPicker0.Value = color;
                }
                else
                {
                    mainTextCell.InnerHtml = "<input type=\"color\" class=\"stratColor\" id=\"ColorPicker" + (count - 1).ToString() + "\" onchange=\"changeColor(" + (count - 1).ToString() + ")\" value =\"" + color + "\">";
                    mainTextCell.Controls.Add(lasttext);

                    delete = new HyperLink();
                    delete.ID = "StratDelete" + (count - 1).ToString();
                    delete.Attributes.Add("class", "remove_strat");
                    delete.Text = " X";
                    mainTextCell.Controls.Add(delete);
                    mainTextCell.Controls.Add(newtable);
                }

                //making the next strategy text input

                mainTextCell = new HtmlTableCell();
                HtmlInputText text = new HtmlInputText();
                lasttext = text;
                text.ID = "StratBox" + count.ToString();
                text.Attributes.Add("class", "txtStrat");
                text.Attributes.Add("BusTotal", "1");
                text.Attributes.Add("placeholder", "Add Strategy Point");
                text.Attributes.Add("runat", "server");
                text.Attributes.Add("onkeyup", "addStrat(event,this," + count.ToString() + ")");

                //setting color to default value in case this is our last point
                int colorNum = count % colorList.Count;
                mainTextCell.InnerHtml = "<input type=\"color\" class=\"stratColor\" id=\"ColorPicker" + count.ToString() + "\" onchange=\"changeColor(" + count.ToString() + ")\" value =\"" + colorList[colorNum] + "\">";
                mainTextCell.Controls.Add(text);

                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), text.ID + "BusBox0Row", "showStrat(" + text.ID + "BusBox0Row);", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), text.ID + "BusBox0ProjBox0", "hideProj("+ text.ID + "BusBox0ProjBox0);", true);
                
                //adding the delete button
                delete = new HyperLink();
                delete.ID = "StratDelete" + count.ToString();
                delete.Attributes.Add("class", "remove_strat");
                delete.Text = " X";
                mainTextCell.Controls.Add(delete);

                //making the new table
                mainTextCell.Controls.Add(new LiteralControl("<br />"));
                lastTable = newtable;
                newtable = new HtmlTable();
                newtable.ID = "StratBox" + count.ToString() + "Table";
                mainTextCell.Controls.Add(newtable);
                HtmlTableRow stratTableRow = new HtmlTableRow();
                stratTableRow.ID = "StratBox" + count.ToString() + "BusBox0Row";
                newtable.Rows.Add(stratTableRow);
                HtmlTableCell stratCell = new HtmlTableCell();
                stratTableRow.Cells.Add(stratCell);

                /*
                * Loading Business Values
                */

                #region Business Values
                int valcount = 0;

                // the table that the visual business values are in
                HtmlTable StratVisTable = new HtmlTable();
                StratVisTable.ID = p.GetName() + "VisualTable";
                // the last business value we made
                HtmlInputText lastBusVal = new HtmlInputText();

                // the table for projects
                HtmlTable BusTable = new HtmlTable();

                float butheight = 3.4f;

                HtmlInputText nextText = new HtmlInputText();


                foreach (BusinessValue b in p.GetBusinessValues())
                {
                    /*
                    * Visual business values
                    */

                    // adding on to the BusTotal of the strategy point
                    textbox.Attributes.Add("BusTotal", (valcount + 2).ToString());

                    
                    HtmlTableCell bc1 = new HtmlTableCell();
                    HtmlTableCell bc2 = new HtmlTableCell();
                    HtmlTableRow visRow = new HtmlTableRow();

                    // if valcount is 0 insert the business value as normal
                    if (valcount == 0)
                    {
                        // making the visual cell
                        HtmlTableCell sCell = new HtmlTableCell();
                        sCell.Attributes.Add("class", "NewCellVis");
                        sCell.Controls.Add(StratVisTable);
                        row.Cells.Add(sCell);

                        // making the visual row for value
                        visRow.Attributes.Add("class", "RowVis");
                        visRow.ID = b.GetName() + "RowVis";
                        if (count == 1 && valcount == 0)
                        {
                            visRow.Attributes.Add("style", "border-top: 2pt solid; border-top-color: #D3D3D3;");
                        }
                        StratVisTable.Rows.Add(visRow);

                        bc1.ID = b.GetName() + "td";
                        bc1.Attributes.Add("class", "projtd");
                        visRow.Cells.Add(bc1);
                        bc2 = new HtmlTableCell();
                        bc2.ID = p.GetName() + "BusVisual" + valcount.ToString();
                        bc2.Attributes.Add("class", "BusVis");
                        //setting text to correct value
                        bc2.InnerText = b.GetDescription();
                        visRow.Cells.Add(bc2);


                    }
                    else
                    {

                        HtmlTableRow newPRow = new HtmlTableRow();
                        StratVisTable.Rows.Add(newPRow);
                        newPRow.Attributes.Add("class", "RowVis");
                        newPRow.ID = b.GetName() + "RowVis";

                        bc1 = new HtmlTableCell();
                        bc1.ID = b.GetName() + "td";
                        bc1.Attributes.Add("class", "projtd");

                        newPRow.Cells.Add(bc1);
                        bc2.ID = p.GetName() + "BusVisual" + valcount.ToString();
                        bc2.Attributes.Add("class", "BusVis");
                        
                        //setting text to correct value
                        bc2.InnerText = b.GetDescription();

                        newPRow.Cells.Add(bc2);

                        //changing height of strategy point

                        //currentheight = document.getElementById("StratBut" + String(CurrentStratCount)).style.height.split('em')[0];
                        //but.Attributes.g
                        //document.getElementById("StratBut" + String(CurrentStratCount)).style.height = String(parseFloat(currentheight) + 3.27) + "em";
                        butheight = butheight + 3.27f;
                        totalHeight = totalHeight + 5; 
                        but.Style.Add(HtmlTextWriterStyle.Height, butheight.ToString() + "em");

                    }

                    /*
                    * Loading info into edit panel
                    */



                    //getting correct text box
                    HtmlInputText bustextbox = new HtmlInputText();

                    if (count == 1 && valcount == 0)
                    {
                        bustextbox = StratBox0BusBox0;
                        bustextbox.Attributes.Add("firstadd", "1");
                    }
                    else
                    {
                        bustextbox = busVal;

                    }
                    
                    //setting text to correct value

                    bustextbox.Value = b.GetDescription();

                    //getting correct table
                    if (count == 1)
                    {
                        BusTable = StratBox0Table;
                    }
                    else
                    {
                        BusTable = lastTable;
                    }

                    
                    //increaing index to for next business value
                    valcount++;

                    //creating next business value text box

                    HtmlTableRow NextRow = new HtmlTableRow();
                    NextRow.ID = "StratBox" + (count - 1).ToString() + "BusBox" + valcount.ToString() + "Row";

                    NextInputCell = new HtmlTableCell();
                    NextInputCell.ID = "StratBox" + (count - 1).ToString() + "BusBox" + valcount.ToString() + "Cell";

                    //adding new input box
                    HtmlInputText NextBox = new HtmlInputText();
                    NextBox.Attributes.Add("class", "txtBus");
                    NextBox.Attributes.Add("ProjTotal", "1");
                    NextBox.ID = "StratBox" + (count - 1).ToString() + "BusBox" + valcount.ToString();
                    NextBox.Attributes.Add("placeholder", "Add Business Value");
                    NextBox.Attributes.Add("runat", "server");
                    NextBox.Attributes.Add("onkeyup", "addBus(event,this," + valcount.ToString() + ")");

                    // making it so you can't delete last text box
                    if (valcount < p.GetBusinessValues().Count)
                    {
                        NextBox.Attributes.Add("firstadd", "1");
                    }

                    BusTable.Rows.Add(NextRow);
                    NextInputCell.Controls.Add(NextBox);


                    //adding delete button
                    delete = new HyperLink();
                    delete.ID = "StratBox" + (count - 1).ToString() + "BusBox" + valcount.ToString() + "Delete";
                    delete.Attributes.Add("class", "remove_bus");
                    delete.Text = " X";
                    NextInputCell.Controls.Add(delete);


                    NextRow.Cells.Add(NextInputCell);

                    

                    /*
                    * Loading Projects
                    */


                    #region Loading Projects
                    int projCount = 0;
                    HtmlTableCell lastCell = new HtmlTableCell();
                    HtmlInputText newprojText = new HtmlInputText();
                    foreach (Project proj in b.GetProjects())
                    {
                        //updating the project count for business value
                        bustextbox.Attributes.Add("ProjTotal", (projCount + 2).ToString());
                        
                        /*
                        * Visual Projects
                        */
                        bc1.InnerHtml = bc1.InnerHtml + "<div id=\"" + proj.GetName() + "But" + "\" ondblclick=\"showModal(this.id)\" onclick=\"Highlight(this.id)\" onmouseout =\"UnHighlight(this.id)\" class=\"proj1\" style=\"cursor: auto; left: " + proj.GetLeft().ToString() + "px; top: 0px; width: " + proj.GetWidth().ToString() + "px; background-color: " + color + ";\">" +
                            "<span style='width:" +(proj.GetWidth()-15).ToString() + "px;' class='projLabel' id='" +proj.GetName()+"Label'>" + proj.GetDescription() + "</span>" +
                            "</div>" +
                            "<div class=\"space\" id=\"" + proj.GetName() + "space\"></div>";

                        

                        /*
                        * Updating edit info
                        */

                        //finding correct text box

                        HtmlInputText projTextBox = new HtmlInputText();

                        if (count == 1 && valcount == 1 && projCount == 0)
                        {
                            StratBox0BusBox0ProjBox0.Value = proj.GetDescription();
                            lastCell = StratBox0BusBox0Cell;
                            
                            if (StratBox0BusBox0ProjBox0.Value == "")
                            {
                                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), StratBox0BusBox0ProjBox0.ID + "Hide", "showProj(" + StratBox0BusBox0ProjBox0.ID + ");", true);
                            }
                        }
                        else if (valcount == 1 && projCount == 0)
                        {
                            projText.Value = proj.GetDescription();
                            lastCell = projText.Parent as HtmlTableCell;
                            if ( projText.Value == "")
                            {
                                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), projText.ID + "Hide", "showProj(" + projText.ID + ");", true);
                            }
                            

                        }
                        else if (projCount == 0)
                        {

                            nextText.Value = proj.GetDescription();
                            lastCell = nextText.Parent as HtmlTableCell;
                            if (nextText.Value == "")
                            {
                                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), nextText.ID + "show", "showProj(" + nextText.ID + ");", true);
                            }
                            

                        }
                        else
                        {
                            newprojText.Value = proj.GetDescription();

                            
                        }



                        //updaing size of strategy point

                        if (projCount > 1) {
                            //increase stratbut height
                            //var currentheight = document.getElementById("StratBut" + String(CurrentStratCount)).style.height.split("em")[0];
                            //document.getElementById("StratBut" + String(CurrentStratCount)).style.height = String(parseFloat(currentheight) + 1.7) + "em";
                            butheight = butheight + 1.7f;
                            totalHeight = totalHeight + 2.5;
                            //but.Style.Add(HtmlTextWriterStyle.Height, "6em");
                            but.Style.Add(HtmlTextWriterStyle.Height, butheight.ToString() + "em");
                            //increase RowVis height

                            //visRow
                            visRow.Style.Add(HtmlTextWriterStyle.Height, ((projCount+1) * 2.5).ToString()+"em");

                            
                            //RowVis = obj.id.split("ProjBox")[0] + "RowVis";
                            //currentheight = document.getElementById(RowVis).style.height;

                            //document.getElementById(RowVis).style.height = (ProjTotal * 2.5).toString() + "em";

                        }

                        //setting the last cell 
                        lastCell.ID = b.GetName() + "Cell";


                        //adding delete button
                        delete = new HyperLink();
                        delete.ID = "StratBox" + (count - 1).ToString() + "BusBox" + (valcount - 1).ToString() + "ProjBox" + projCount.ToString() + "Delete";
                        delete.Attributes.Add("class", "remove_proj");
                        delete.Text = " X";
                        lastCell.Controls.Add(delete);
                 
                        //increasing the index of projects for next project
                        projCount++;

                        //creating next project text box
                        newprojText = new HtmlInputText();
                        newprojText.Name = "DynmaicTextBox";
                        newprojText.ID = "StratBox" + (count - 1).ToString() + "BusBox" + (valcount - 1).ToString() + "ProjBox" + projCount.ToString();
                        newprojText.Attributes.Add("class", "txtProjDel");
                        newprojText.Attributes.Add("placeholder", "Add Project");
                        newprojText.Attributes.Add("runat", "server");
                        
                        newprojText.Attributes.Add("onkeyup", "addProj(event,this," + projCount.ToString() + ")");
                        lastCell.Controls.Add(newprojText);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), newprojText.ID + "Hide", "showProj(" + newprojText.ID + ");", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), proj.GetName()+"func", "enableDragbyId(" + proj.GetName() + ");", true);
                    }
                    #endregion

                    //creating default project textbox for a business value
                    nextText = new HtmlInputText();
                    nextText.Name = "DynmaicTextBox";
                    nextText.ID = "StratBox" + (count - 1).ToString() + "BusBox" + valcount.ToString() + "ProjBox0";
                    nextText.Attributes.Add("class", "txtProjDel");
                    nextText.Attributes.Add("placeholder", "Add Project");
                    nextText.Attributes.Add("runat", "server");
                    nextText.Attributes.Add("onkeyup", "addProj(event,this," + projCount.ToString() + ")");
                    NextInputCell.Controls.Add(nextText);
                    busVal = NextBox;
                  
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), nextText.ID + "Hide", "hideProj(" + nextText.ID + ");", true);
                  

                }



                #endregion

                //creating default business value for strategy point
                busVal = new HtmlInputText();
                busVal.Attributes.Add("class", "txtBus");
                busVal.Attributes.Add("ProjTotal", "1");
                busVal.ID = "StratBox" + count.ToString() + "BusBox0";
                busVal.Attributes.Add("placeholder", "Add Business Value");
                busVal.Attributes.Add("runat", "server");
                //busVal.Attributes.Add("firstadd", "1");
                busVal.Attributes.Add("onkeyup", "addBus(event,this," + count.ToString() + ")");
                stratCell.Controls.Add(busVal);

                //default delete
                delete = new HyperLink();
                delete.ID = "StratBox" + (count).ToString() + "BusBox0Delete";
                delete.Attributes.Add("class", "remove_bus");
                delete.Text = " X";         
                stratCell.Controls.Add(delete);

                //adding in default project for first business value
                projText = new HtmlInputText();
                projText.Name = "DynmaicTextBox";
                projText.ID = "StratBox" + count.ToString() + "BusBox0ProjBox0";
                projText.Attributes.Add("class", "txtProjDel");
                projText.Attributes.Add("placeholder", "Add Project");
                projText.Attributes.Add("runat", "server");
                projText.Attributes.Add("onkeyup", "addProj(event,this," + count.ToString() + ")");
                stratCell.Controls.Add(projText);

                lastRow.Cells.Add(mainTextCell);
                HtmlTable sideTable = FindControl("sidebarTable") as HtmlTable;
                sideTable.Rows.Add(lastRow);

                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), projText.ID + "Hide", "hideProj(" + projText.ID + ");", true);
                
                #endregion

            }

            //hiding the elements of the newest strategy point
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "StratBox" + (count).ToString() + "BusBox0Row", "hideStrat(StratBox" + (count).ToString() + "BusBox0Row);", true);
            

            /*
            * Loading in timeline
            */
            TimeLine TL = roadmap.GetTimeline();
            if(TL!= null)
            {
                int tickCount = 0;
                foreach (TickMark tick in TL.GetTicks())
                {
                    tickCount++;
                    double translateAmount = 0;
                    if (tick.GetName().Length < 2)
                    {
                        translateAmount = .2;
                    }
                    else
                    {
                        translateAmount = -1 * (tick.GetName().Length * .25 - .35);
                    }
                    containmentWrapper.InnerHtml += "<div  ondblclick = \"deleteTime(this)\" class=\"timeline\" id=\"" + tick.GetName()+ "\" style=\"left: " + (tick.GetXPlacement()).ToString() + "px; top: -3px; \">"
                        + "<p style='transform: translateX(" + translateAmount + "em)' class=\"timelineText\">" + tick.GetName() + "</p>"+
                        "</div>";
                }

            }

           //enabling drag
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script2", "enableDrag();", true);
            //set total height
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "setTotalHeight", "setTotalHeight(" + totalHeight.ToString() + ");", true);
        }

        #region Adding functions

        [WebMethod]
        public static void AddStrat(string id, string name, string color, string mapName)
        {

            RoadMap map = new RoadMap(mapName);

            int n = map.GetStrategyPoints().Count;

            StrategyPoint point = new StrategyPoint(id, name, mapName);

            map.AddStrategyPoint(point);
            point.EditColor(color);
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
            point.CreateBusinessValue(id, name, mapName);

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
        public static void SetProjPos(string id, string mapName, string stratID, string BusID, float startPos, float width)
        {
            int pointindex = id.IndexOf("Bus");
            int valindex = id.IndexOf("Proj");
            string point = id.Substring(0, pointindex);
            //string val = id.Substring(pointindex, valindex);
            //string pro = id.Substring(valindex, -1);
            RoadMap map = new RoadMap(mapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(BusID);
            Project newproj = newval.GetProject(id);

            //set width and start position

            newproj.SetWidth((int)width);
            startPos -= 47f;
            newproj.SetLeft((int)startPos);
            
        }

        [WebMethod]
        public static void AddTick(string mapName, int pos, string label)
        {
            RoadMap map = new RoadMap(mapName);
            TimeLine TL = map.GetTimeline();

            if(TL== null)
            {
                map.CreateTimeLine(mapName);
            }

            TL = map.GetTimeline();
            
            TickMark tickmark = new TickMark(label, pos);
            TL.NewTickMark(tickmark);
        }

        [WebMethod]
        public static void EditTickLocation(string mapName, float pos, string label)
        {
            TickMark tickmark = new TickMark(label, (int)pos);
            tickmark.EditTickLocation((int)pos, mapName);          
        }

        [WebMethod]
        public static string GetValue(string id, string mapName)
        {
            RoadMap map = new RoadMap(mapName);
            return map.GetPoint(id).GetDescription();

        }

        [WebMethod]
        public static void DeleteStrat(string id, string mapName)
        {
            RoadMap map = new RoadMap(mapName);
            map.DeleteStrategyPoint(id);

        }

        [WebMethod]
        public static void DeleteBus(string BusId, string StratId, string mapName)
        {
            RoadMap map = new RoadMap(mapName);
            map.GetPoint(StratId).DeleteBusinessValue(BusId);
        }

        [WebMethod]
        public static void DeleteProj(string ProjId, string BusId, string StratId, string mapName)
        {
            RoadMap map = new RoadMap(mapName);
            map.GetPoint(StratId).GetBusinessValue(BusId).DeleteProject(ProjId);
        }

        #endregion






        #region Modal Getters
        //Get Project Name (What is written on the button)
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


        //Get Project Modal Description
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

            return newproj.GetModalDescription();

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
            Dep_Names = newproj.GetDependantStrings();

            return Dep_Names;
        }

        //Get Project Depencies
        [WebMethod]
        public static string[][] GetProjectDependencyArr(string ProjectID, string RoadmapName)
        {
            List<string> Depon_Names = new List<string>();
            List<string> Projecton_List = new List<string>();
            List<string> Depof_Names = new List<string>();
            List<Project> Projectof_List = new List<Project>();
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);

            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);
            Projecton_List = newproj.GetDependencies();
            Projectof_List = newproj.GetDependants();

            
            //for each project just get the project names 
            foreach (string p in Projecton_List)
            {
                Depon_Names.Add(p);

            }
            //for each project just get the project names 
            foreach (Project p in Projectof_List)
            {
                Depof_Names.Add(p.GetName());

            }
            string[][] final_return = new string[2][];
            int x = 0;
            int y = 0;
            final_return[0] = new string[Depon_Names.Count];
            final_return[1] = new string[Depof_Names.Count];

            foreach (string s in Depon_Names)
            {
                final_return[0][x] = s;
                x+=1;
            }
            foreach (string s in Depof_Names)
            {
                final_return[1][y] = s;
                y++;
            }



            //Send name arrary

            return final_return;
        }
        //Get Project Depencies
        [WebMethod]
        public static List<string> GetProjectDependency(string ProjectID, string RoadmapName)
        {
            List<string> Project_Names = new List<string>();
            List<string> Project_List = new List<string>();
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);

            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);
            Project_List = newproj.GetDependencies();


            //for each project just get the project names 
            foreach (string p in Project_List)
            {
                Project_Names.Add(p);

            }

            //Send name arrary

            return Project_Names;
        }

        //Get Proejct Risks
        [WebMethod]
        public static string GetProjectRisk(string ProjectID, string RoadmapName)
        {
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);

            return newproj.GetProjectRisks();
        }

        //Get Project Links as a string
        [WebMethod]
        public static List<string> GetProjectLinksString(string ProjectID, string RoadmapName)
        {
            List<string> Project_Links = new List<string>();
            List<Link> link_list = new List<Link>();
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);
            link_list = newproj.GetLinks();


            //for each link just get the link text
            foreach (Link l in link_list)
            {
                Project_Links.Add(l.GetLink());
            }

            //Send link name array
            return Project_Links;
        }

        //Get Project Links as a string
        [WebMethod]
        public static List<Link> GetProjectLinks(string ProjectID, string RoadmapName)
        {
            List<string> Project_Links = new List<string>();
            List<Link> link_list = new List<Link>();
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);

            return newproj.GetLinks();
        }

        //Return all the projects 
        [WebMethod]
        public static List<Project> GetAllRoadmapProjects(string RoadmapName)
        {
            RoadMap map = new RoadMap(RoadmapName);
            return map.GetAllProjects();
        }

        //Return the names of the projects in the roadmap
        [WebMethod]
        public static List<string> GetAllRoadmapProjectDesc(string RoadmapName)
        {
            List<Project> All_proj = new List<Project>();
            List<string> return_string = new List<string>();
            RoadMap map = new RoadMap(RoadmapName);
            All_proj = map.GetAllProjects();

            foreach (Project p in All_proj)
            {
                return_string.Add(p.GetDescription());
            }

            //return all the porjects names 
            return return_string;
        }

        [WebMethod]
        public static string[][] GetAll(string ProjectID, string RoadmapName)
        {
            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);

            List<string> DepStr = new List<string>();
            List<string> DepProj = new List<string>();
            List<string> Link = new List<string>();
            List<string> AllProj = new List<string>();
            List<string> Proj = new List<string>();
            string Desc = newproj.GetModalDescription();
            string Risk = newproj.GetProjectRisks();
            string Name = newproj.GetDescription();

            DepStr = newproj.GetDependantStrings();
            DepProj = GetProjectDependency(ProjectID, RoadmapName);
            Link = GetProjectLinksString(ProjectID, RoadmapName);
            AllProj = GetAllRoadmapProjectDesc(RoadmapName);

            List<Project> all_projs = map.GetAllProjects();
            List<string> DepProjNames = new List<string>();
            foreach (string s in DepProj)
            {
                foreach (Project proj in all_projs)
                {
                    if(proj.GetName() == s)
                    {
                        DepProjNames.Add(proj.GetDescription());
                    }
                }
            }

            foreach(string rem in AllProj)
            {
                if (rem != Name) {
                    Proj.Add(rem);
                }
            }

            string[][] final_return = new string[7][];
            final_return[0] = new string[1];
            final_return[1] = new string[1];
            final_return[2] = new string[1];
            final_return[3] = new string[DepStr.Count];
            final_return[4] = new string[DepProj.Count];
            final_return[5] = new string[Link.Count];
            final_return[6] = new string[Proj.Count];

            final_return[0][0] = Desc;
            final_return[1][0] = Risk;
            final_return[2][0] = Name;
            int y = 0, x = 0, z = 0, a = 0;

            foreach (string ds in DepStr)
            {
                final_return[3][x] = ds;
                x++;
            }
            foreach (string dp in DepProjNames)
            {
                final_return[4][a] = dp;
                a++;
            }
            foreach (string l in Link)
            {
                final_return[5][z] = l;
                z++;
            }
            foreach (string ap in Proj)
            {
                final_return[6][y] = ap;
                y++;
            }


            return final_return;
        }

        #endregion

        #region Modal Setters

        //Set Project Dependency 
        [WebMethod]
        public static void SetAll(string ProjectID, string RoadmapName, string[] proj_dep, string[] link_arr, string[] string_dep, string desc, string risk)
        {
            List<Project> tot_list = new List<Project>(); // Total List of projects
            List<string> P_list = new List<string>(); // 
            List<Project> P_list2 = new List<Project>();
            List<Project> dep_list = new List<Project>();
            List<Link> Link_List = new List<Link>();
            List<string> Dep_Names = new List<string>();
            List<string> Dep_Names2 = new List<string>();

            int pointindex = ProjectID.IndexOf("Bus");
            int valindex = ProjectID.IndexOf("Proj");
            string point = ProjectID.Substring(0, pointindex);
            string val = ProjectID.Substring(0, valindex);
            RoadMap map = new RoadMap(RoadmapName);
            StrategyPoint newpoint = map.GetPoint(point);
            BusinessValue newval = newpoint.GetBusinessValue(val);
            Project newproj = newval.GetProject(ProjectID);

            //setting
            newproj.SetModalDescription(desc);
            newproj.SetProjectRisks(risk);

            P_list = newproj.GetDependencies();
            tot_list = map.GetAllProjects();
            Link_List = GetProjectLinks(ProjectID, RoadmapName);
            Dep_Names = newproj.GetDependantStrings();

            //DELETE ALL DEPENDENTS
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "DELETE FROM [dbo].[Dependents] WHERE ProjectName=@Pname AND RoadmapName=@Rname";
                    cmd.Parameters.AddWithValue("@Pname", ProjectID);
                    cmd.Parameters.AddWithValue("@Rname", RoadmapName);
                    cmd.Connection = conn;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            //Add in new dependents list
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                foreach (string s in proj_dep)
                {
                    Project proj=null;
                    foreach (Project pro in tot_list)
                    {
                        if (pro.GetDescription() == s)
                        {
                            proj = pro;
                        }
                    }
                    if (proj == null) break;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "INSERT INTO [dbo].[Dependents] (ProjectName, DependantName,Description, RoadmapName) VALUES (@Pname, @Dname,'this column is dumb', @Rname)";
                        cmd.Parameters.AddWithValue("@Pname", ProjectID);
                        cmd.Parameters.AddWithValue("@Dname", proj.GetName());
                        cmd.Parameters.AddWithValue("@Rname", RoadmapName);
                        cmd.Connection = conn;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }

            
          

            //SEPARATE THE LINK 
            //Check if link exists, if not create it
            bool lflag = false;

            foreach (string str_link in link_arr)
            {
                lflag = false;
                foreach (Link l_list in Link_List)
                {
                    if (str_link == l_list.GetLink())
                    {
                        lflag = true;
                    }
                }
                if (lflag == false)
                {
                    newproj.CreateLink(new Link("", ProjectID, str_link, RoadmapName));
                }
            }

            //Delete Links
            foreach (Link l in Link_List)
            {
                bool flag = false;
                foreach (string l_list in link_arr)
                {
                    if (l.GetLink() == l_list)
                    {
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    newproj.DeleteLink(l);
                }
            }


            //SEPARATE THER STRINGDEPENDENCY
            //Check if a string depenedency is already in the list
            foreach (string s in string_dep)
            {
                if (!Dep_Names.Contains(s))
                {
                    Dep_Names.Add(s);
                }
            }

            //remove any that are in the list but not in the send in array
            foreach (string s in Dep_Names)
            {
                if (!string_dep.Contains(s))
                {
                    Dep_Names2.Add(s);
                }
            }
            foreach (string s in Dep_Names2)
            {
                Dep_Names.Remove(s);
            }

            newproj.UpdateDependantStrings(Dep_Names);


        }

        [WebMethod]
        public static void SetColor(string id, string color, string mapName)
        {
            RoadMap map = new RoadMap(mapName);

            StrategyPoint point = map.GetPoint(id);
            point.EditColor(color);



        }
        [WebMethod]
        public static void SetDependencyColor(string on, string of,string mapName)
        {
            RoadMap map = new RoadMap(mapName);

            
        }

        #endregion
    }
}




