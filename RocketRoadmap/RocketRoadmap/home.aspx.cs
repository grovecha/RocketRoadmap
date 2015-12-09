using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RocketRoadmap.DB;
using System.Data.SqlClient;
using System.Configuration;

namespace RocketRoadmap
{
    public partial class home : System.Web.UI.Page
    {
        private User mUser;

        protected void Page_Load(object sender, EventArgs e)
        {

            mUser = new DB.User((string)Session["username"], (string)Session["password"]);
            name.InnerText = mUser.GetUserName() + "'s Roadmaps";
            searchtable.Rows.Clear();
            searchtable.Visible = false;
            searchb.Enabled = false;

            if (Request.Form["username_ID"] != "" && Request.Form["password_ID"] != "") //FIX: Lets null login.  is useful though
                {
                    RocketRoadmap.DB.User user = new RocketRoadmap.DB.User(Request.Form["username_ID"], Request.Form["password_ID"]);
                    bool flag = user.Login();

                    if (flag)
                    {
                        Session["username"] = user.GetUserName();
                        Session["password"] = user.GetPassword();
                        mUser = new DB.User((string)Session["username"], (string)Session["password"]);
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
                            u1.Width = new Unit(20, UnitType.Percentage);
                            u2.Text = "Author";
                            u2.Width = new Unit(15, UnitType.Percentage);
                            u3.Text = "Description";
                            u3.Width = new Unit(40, UnitType.Percentage);
                            u4.Text = "Timestamp";
                            u4.Width = new Unit(20, UnitType.Percentage);
                            u5.Width = new Unit(5, UnitType.Percentage);

                            uhead.Cells.Add(u1);
                            uhead.Cells.Add(u2);
                            uhead.Cells.Add(u3);
                            uhead.Cells.Add(u4);
                            uhead.Cells.Add(u5);

                            userroadmaps.Rows.Add(uhead);

                            foreach (var umap in uall)
                            {
                                TableRow urow = new TableRow();

                                TableCell ucell_1 = new TableCell();
                                TableCell ucell_2 = new TableCell();
                                TableCell ucell_3 = new TableCell();
                                TableCell ucell_4 = new TableCell();
                                TableCell ucell_5 = new TableCell();

                                 HtmlInputButton deleteButton = new HtmlInputButton();
                                deleteButton.Value = "X";
                                deleteButton.Attributes.Add("onclick","AreYouSure(\""+umap[0]+"\");");


                                HyperLink link = new HyperLink();
                                link.NavigateUrl = "Roadmap.aspx?n=" + Uri.EscapeUriString(umap[0]);
                                link.Text = umap[0];

                                TableCell tCell1 = new TableCell();
                                ucell_1.Controls.Add(link);

                                ucell_2.Text = umap[1];
                                ucell_3.Text = umap[2];
                                ucell_4.Text = umap[3];

                                ucell_5.Controls.Add(deleteButton);


                                urow.Cells.Add(ucell_1);
                                urow.Cells.Add(ucell_2);
                                urow.Cells.Add(ucell_3);
                                urow.Cells.Add(ucell_4);
                                urow.Cells.Add(ucell_5);

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
                        TableHeaderRow uhead = new TableHeaderRow();

                        TableHeaderCell u1 = new TableHeaderCell();
                        TableHeaderCell u2 = new TableHeaderCell();
                        TableHeaderCell u3 = new TableHeaderCell();
                        TableHeaderCell u4 = new TableHeaderCell();
                        TableHeaderCell u5 = new TableHeaderCell();
                        TableHeaderCell u6 = new TableHeaderCell();

                        u1.Text = "Name";
                        u1.Width = new Unit(20, UnitType.Percentage);
                        u2.Text = "Author";
                        u2.Width = new Unit(15, UnitType.Percentage);
                        u3.Text = "Description";
                        u3.Width = new Unit(40, UnitType.Percentage);
                        u4.Text = "Timestamp";
                        u4.Width = new Unit(20, UnitType.Percentage);
                        u5.Width = new Unit(5, UnitType.Percentage);

                        uhead.Cells.Add(u1);
                        uhead.Cells.Add(u2);
                        uhead.Cells.Add(u3);
                        uhead.Cells.Add(u4);
                        uhead.Cells.Add(u5);

                        allroadmaps.Rows.Add(uhead);

                foreach (var map in all)
                        {
                            TableRow row = new TableRow();
                            TableCell cell_1 = new TableCell();
                            TableCell cell_2 = new TableCell();
                            TableCell cell_3 = new TableCell();
                            TableCell cell_4 = new TableCell();
                            TableCell cell_5 = new TableCell();
                            TableCell cell_6 = new TableCell();

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
                            row.Cells.Add(cell_5);


                        allroadmaps.Rows.Add(row);
                        }
                    }
                    catch (NullReferenceException nre)
                    {

                    }
            
                
           
        }


        public void newroadmap(object sender, EventArgs e)
        {

            try {
                RoadMaps nRoadmap = new RoadMaps();
                if (roadmap_Name.Value == null)
                {
                    roadmap_Name.Value = "Allbriansfault";
                }

                nRoadmap.CreateRoadMap(roadmap_Name.Value.ToString(), roadmap_Desc.Value.ToString(), mUser.GetUserName());
                Response.Redirect("Roadmap.aspx?n=" + roadmap_Name.Value, false);
            } catch {
                System.Windows.Forms.MessageBox.Show("ERROR: Roadmap with name already exists! Please rename and try again");
            }


        }

        [WebMethod]
        public static void DeleteRoadmap(string name)
        {
            RoadMaps maps = new RoadMaps();

            maps.DeleteRoadMap(name);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "DELETE FROM [dbo].[SP_BV_Crosswalk] WHERE RoadmapName=@Rname";
                    cmd.Parameters.AddWithValue("@Rname", name);
                    cmd.Connection = conn;


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }

        protected void BtnHandler(Object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            RoadMaps maps = new RoadMaps();

            //ADD YES NO MODAL

            maps.DeleteRoadMap(btn.CommandArgument);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "DELETE FROM [dbo].[SP_BV_Crosswalk] WHERE RoadmapName=@Rname";
                    cmd.Parameters.AddWithValue("@Rname", btn.CommandArgument);
                    cmd.Connection = conn;


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            //Response.Redirect(Request.RawUrl);
        }

        protected void EditRoadmap(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RoadMap map = new RoadMap(btn.CommandArgument);

            
            Response.Redirect(Request.RawUrl);
        }
        protected void searchRoadmaps(Object sender, EventArgs e)
        {
            string search_String;
            Button btn = (Button)sender;
            RoadMaps maps = new RoadMaps();
            List<List<string>> L_map = new List<List<String>>();
            search_String = search_text.Text.ToString();
            L_map = maps.Search(search_String);
            search_name.InnerText = "Search Results:";

            try
            {
                TableHeaderRow uhead = new TableHeaderRow();

                TableHeaderCell u1 = new TableHeaderCell();
                TableHeaderCell u2 = new TableHeaderCell();
                TableHeaderCell u3 = new TableHeaderCell();
                TableHeaderCell u4 = new TableHeaderCell();
                TableHeaderCell u5 = new TableHeaderCell();
                TableHeaderCell u6 = new TableHeaderCell();

                u1.Text = "Name";
                u1.Width = new Unit(20, UnitType.Percentage);
                u2.Text = "Author";
                u2.Width = new Unit(15, UnitType.Percentage);
                u3.Text = "Description";
                u3.Width = new Unit(40, UnitType.Percentage);
                u4.Text = "Timestamp";
                u4.Width = new Unit(20, UnitType.Percentage);
                u5.Width = new Unit(5, UnitType.Percentage);

                uhead.Cells.Add(u1);
                uhead.Cells.Add(u2);
                uhead.Cells.Add(u3);
                uhead.Cells.Add(u4);
                uhead.Cells.Add(u5);

                searchtable.Rows.Add(uhead);
                searchtable.Visible = true;

                foreach (var map in L_map)
                {
                    TableRow row = new TableRow();
                    TableCell cell_1 = new TableCell();
                    TableCell cell_2 = new TableCell();
                    TableCell cell_3 = new TableCell();
                    TableCell cell_4 = new TableCell();
                    TableCell cell_5 = new TableCell();
                    TableCell cell_6 = new TableCell();

                    HtmlInputButton deleteButton = new HtmlInputButton();
                    deleteButton.Value = "X";
                    deleteButton.Attributes.Add("onclick", "AreYouSure(\"" + map[0] + "\");");


                    Button B2 = new Button();
                    B2.Text = "EDIT";
                    B2.CommandArgument = map[0];
                    B2.Click += new EventHandler(EditRoadmap);
                    B2.UseSubmitBehavior = false;

                    HyperLink link = new HyperLink();
                    link.NavigateUrl = "Roadmap.aspx?n=" + map[0];
                    link.Text = map[0];

                    TableCell tCell1 = new TableCell();
                    cell_1.Controls.Add(link);
                    cell_1.Controls.Add(B2);

                    cell_2.Text = map[1];
                    cell_3.Text = map[2];
                    cell_4.Text = map[3];

                    cell_5.Controls.Add(deleteButton);
                    cell_6.Controls.Add(B2);

                    row.Cells.Add(cell_1);
                    row.Cells.Add(cell_2);
                    row.Cells.Add(cell_3);
                    row.Cells.Add(cell_4);
                    row.Cells.Add(cell_5);

                    searchtable.Rows.Add(row);
                }
            }
            catch (NullReferenceException nre)
            {

            }


        }

    }
    }
