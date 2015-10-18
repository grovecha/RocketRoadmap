using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RocketRoadmap.DB;

namespace RocketRoadmap
{
    public partial class home : System.Web.UI.Page
    {
        User mUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["username_ID"] != "" && Request.Form["password_ID"] != "") //FIX: Lets null login.  is useful though
            {
                RocketRoadmap.DB.User user = new RocketRoadmap.DB.User(Request.Form["username_ID"], Request.Form["password_ID"]);
                bool flag = user.Login();
                if (flag)
                {
                    mUser = user;
                    loginlabel.Text = "Logged in as: " + mUser.GetUserName();
                    name.Text = mUser.GetUserName() + "'s ROADMAPS";
                }
                else
                {
                    Response.Redirect("index.aspx", false);
                }

            }

            if (mUser != null)
            {

                RoadMaps umaps = new RoadMaps();
                List<RoadMap> uall = new List<RoadMap>();

                try
                {
                    uall = umaps.GetUserMaps(mUser.GetUserName());

                    foreach (var umap in uall)
                    {
                        TableRow urow = new TableRow();
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

                        TableCell ucell_1 = new TableCell();
                        TableCell ucell_2 = new TableCell();
                        TableCell ucell_3 = new TableCell();
                        TableCell ucell_4 = new TableCell();

                        HyperLink link = new HyperLink();
                        link.NavigateUrl = "Roadmap.aspx?n=" + umap.GetName();
                        link.Text = umap.GetName();

                        TableCell tCell1 = new TableCell();
                        ucell_1.Controls.Add(link);

                        ucell_2.Text = umap.GetUser().GetName();
                        ucell_3.Text = umap.GetDecription();
                        ucell_4.Text = umap.GetTimeStamp().ToString();

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
            List<RoadMap> all = new List<RoadMap>();

            try
            {
                all = maps.GetAllMaps();

                foreach (var map in all)
                {
                    TableRow row = new TableRow();
                    TableCell cell_1 = new TableCell();
                    TableCell cell_2 = new TableCell();
                    TableCell cell_3 = new TableCell();
                    TableCell cell_4 = new TableCell();

                    HyperLink link = new HyperLink();
                    link.NavigateUrl = "Roadmap.aspx?n=" + map.GetName(); 
                    link.Text = map.GetName();

                    TableCell tCell1 = new TableCell();
                    cell_1.Controls.Add(link);

                    cell_2.Text = map.GetUser().GetName();
                    cell_3.Text = map.GetDecription();
                    cell_4.Text = map.GetTimeStamp().ToString();

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
    }
