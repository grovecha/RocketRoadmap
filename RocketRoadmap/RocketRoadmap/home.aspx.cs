using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RocketRoadmap.DB;

namespace RocketRoadmap
{
    public partial class home : System.Web.UI.Page
    {
        private User mUser;

        protected void Page_Load(object sender, EventArgs e)
        {

            mUser = new DB.User((string)Session["username"], (string)Session["password"]);
            //loginlabel.Text = "Logged in as: " + mUser.GetUserName();
            name.InnerText = mUser.GetUserName() + "'s ROADMAPS";

                if (Request.Form["username_ID"] != "" && Request.Form["password_ID"] != "") //FIX: Lets null login.  is useful though
                {
                    RocketRoadmap.DB.User user = new RocketRoadmap.DB.User(Request.Form["username_ID"], Request.Form["password_ID"]);
                    bool flag = user.Login();

                    if (flag)
                    {
                        Session["username"] = user.GetUserName();
                        Session["password"] = user.GetPassword();
                        mUser = new DB.User((string)Session["username"], (string)Session["password"]);
                        //loginlabel.Text = "Logged in as: " + user.GetUserName();
                        name.InnerText = user.GetUserName() + "'s Roadmaps";
                    }
                    else if (mUser.Login())
                    {
                    }

                    else
                    {
                        Response.Redirect("index.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("index.aspx", false);
                    return;
                }
                    if (mUser.Login())
                    {

                        RoadMaps umaps = new RoadMaps();

                        try
                        {
                            List<List<string>> uall = umaps.GetUserMapsInfo(mUser.GetUserName());
                            TableHeaderRow uhead = new TableHeaderRow();

                            TableHeaderCell u1 = new TableHeaderCell();
                            TableHeaderCell u2 = new TableHeaderCell();
                            TableHeaderCell u3 = new TableHeaderCell();
                            TableHeaderCell u4 = new TableHeaderCell();
                            TableHeaderCell u5 = new TableHeaderCell();
                            TableHeaderCell u6 = new TableHeaderCell();

                            u1.Text = "Name";
                            u2.Text = "Author";
                            u3.Text = "Description";
                            u4.Text = "Timestamp";

                            uhead.Cells.Add(u1);
                            uhead.Cells.Add(u2);
                            uhead.Cells.Add(u3);
                            uhead.Cells.Add(u4);
                            uhead.Cells.Add(u5);
                            //uhead.Cells.Add(u6);

                            userroadmaps.Rows.Add(uhead);

                            foreach (var umap in uall)
                            {
                                TableRow urow = new TableRow();

                                TableCell ucell_1 = new TableCell();
                                TableCell ucell_2 = new TableCell();
                                TableCell ucell_3 = new TableCell();
                                TableCell ucell_4 = new TableCell();
                                TableCell ucell_5 = new TableCell();
                                TableCell ucell_6 = new TableCell();

                                Button B1 = new Button();
                                B1.Text = "X";
                                B1.CommandArgument = umap[0];
                                B1.Click += new EventHandler(BtnHandler);

                                HyperLink link = new HyperLink();
                                link.NavigateUrl = "Roadmap.aspx?n=" + umap[0];
                                link.Text = umap[0];

                                TableCell tCell1 = new TableCell();
                                ucell_1.Controls.Add(link);

                                ucell_2.Text = umap[1];
                                ucell_3.Text = umap[2];
                                ucell_4.Text = umap[3];

                                ucell_5.Controls.Add(B1);
                                ucell_6.Text = "EDIT";

                                urow.Cells.Add(ucell_1);
                                urow.Cells.Add(ucell_2);
                                urow.Cells.Add(ucell_3);
                                urow.Cells.Add(ucell_4);
                                urow.Cells.Add(ucell_5);
                             //   urow.Cells.Add(ucell_6);

                                userroadmaps.Rows.Add(urow);
                            }
                        }
                        catch (NullReferenceException nre)
                        {

                        }
                    }

                    RoadMaps maps = new RoadMaps();

                    try
                    {
                        List<List<string>> all = maps.GetAllMapsInfo();

                        foreach (var map in all)
                        {
                            TableRow row = new TableRow();
                            TableCell cell_1 = new TableCell();
                            TableCell cell_2 = new TableCell();
                            TableCell cell_3 = new TableCell();
                            TableCell cell_4 = new TableCell();
                            TableCell cell_5 = new TableCell();
                            TableCell cell_6 = new TableCell();

                            Button B1 = new Button();
                            B1.Text = "X";
                            B1.CommandArgument = map[0];
                            B1.Click += new EventHandler(BtnHandler);

                            Button B2 = new Button();
                            B2.Text = "EDIT";
                            B2.CommandArgument = map[0];
                            B2.Click += new EventHandler(EditRoadmap);

                            HyperLink link = new HyperLink();
                            link.NavigateUrl = "Roadmap.aspx?n=" + map[0];
                            link.Text = map[0];

                           TableCell tCell1 = new TableCell();
                            cell_1.Controls.Add(link);
                            cell_1.Controls.Add(B2);

                            cell_2.Text = map[1];
                            cell_3.Text = map[2];
                            cell_4.Text = map[3];

                            cell_5.Controls.Add(B1);
                            cell_6.Controls.Add(B2);

                            row.Cells.Add(cell_1);
                            row.Cells.Add(cell_2);
                            row.Cells.Add(cell_3);
                            row.Cells.Add(cell_4);
                            row.Cells.Add(cell_5);
                           // row.Cells.Add(cell_6);

                        allroadmaps.Rows.Add(row);
                        }
                    }
                    catch (NullReferenceException nre)
                    {

                    }

                
           
        }


        public void newroadmap(object sender, EventArgs e)
        {
            RoadMaps nRoadmap = new RoadMaps();
            if(roadmap_Name.Value == null)
            {
                roadmap_Name.Value = "Allbriansfault";
            }

            nRoadmap.CreateRoadMap(roadmap_Name.Value.ToString(), roadmap_Desc.Value.ToString(), mUser.GetUserName());
            Response.Redirect("Roadmap.aspx?n="+roadmap_Name.Value, false);

        }


        protected void BtnHandler(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RoadMaps maps = new RoadMaps();

            maps.DeleteRoadMap(btn.CommandArgument);
            Response.Redirect(Request.RawUrl);
        }

        protected void EditRoadmap(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RoadMap map = new RoadMap(btn.CommandArgument);

            
            Response.Redirect(Request.RawUrl);
        }

    }
    }
