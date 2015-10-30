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
            mUser = new DB.User((string)ViewState["username"], (string)ViewState["password"]);
            //loginlabel.Text = "Logged in as: " + mUser.GetUserName();
            name.Text = mUser.GetUserName() + "'s ROADMAPS";
            if (!IsPostBack)
            {
                if (Request.Form["username_ID"] != "" && Request.Form["password_ID"] != "") //FIX: Lets null login.  is useful though
                {
                    RocketRoadmap.DB.User user = new RocketRoadmap.DB.User(Request.Form["username_ID"], Request.Form["password_ID"]);
                    bool flag = user.Login();
                    ViewState["username"] = user.GetUserName();
                    ViewState["password"] = user.GetPassword();
                    if (flag)
                    {
                        mUser = new DB.User((string)ViewState["username"], (string)ViewState["password"]);
                        //loginlabel.Text = "Logged in as: " + user.GetUserName();
                        name.Text = user.GetUserName() + "'s ROADMAPS";
                    }
                    else
                    {
                        Response.Redirect("index.aspx", false);
                    }

                }
                else
                {
                    Response.Redirect("index.aspx", false);
                }

                if (mUser != null)
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

                        u1.Text = "Name";
                        u2.Text = "Author";
                        u3.Text = "Description";
                        u4.Text = "Timestamp";

                        uhead.Cells.Add(u1);
                        uhead.Cells.Add(u2);
                        uhead.Cells.Add(u3);
                        uhead.Cells.Add(u4);

                        userroadmaps.Rows.Add(uhead);
                        foreach (var umap in uall)
                        {
                            TableRow urow = new TableRow();

                            TableCell ucell_1 = new TableCell();
                            TableCell ucell_2 = new TableCell();
                            TableCell ucell_3 = new TableCell();
                            TableCell ucell_4 = new TableCell();

                            HyperLink link = new HyperLink();
                            link.NavigateUrl = "Roadmap.aspx?n=" + umap[0];
                            link.Text = umap[0];

                            TableCell tCell1 = new TableCell();
                            ucell_1.Controls.Add(link);

                            ucell_2.Text = umap[1];
                            ucell_3.Text = umap[2];
                            ucell_4.Text = umap[3];

                            urow.Cells.Add(ucell_1);
                            urow.Cells.Add(ucell_2);
                            urow.Cells.Add(ucell_3);
                            urow.Cells.Add(ucell_4);

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

                        HyperLink link = new HyperLink();
                        link.NavigateUrl = "Roadmap.aspx?n=" + map[0];
                        link.Text = map[0];

                        TableCell tCell1 = new TableCell();
                        cell_1.Controls.Add(link);

                        cell_2.Text = map[1];
                        cell_3.Text = map[2];
                        cell_4.Text = map[3];

                        row.Cells.Add(cell_1);
                        row.Cells.Add(cell_2);
                        row.Cells.Add(cell_3);
                        row.Cells.Add(cell_4);

                        allroadmaps.Rows.Add(row);
                    }
                }
                catch (NullReferenceException nre)
                {

                }

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
    }
    }
