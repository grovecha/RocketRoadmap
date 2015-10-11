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
            if (Request.Form["username_ID"] != "" && Request.Form["password_ID"] != "")
            {
                RocketRoadmap.DB.User user = new RocketRoadmap.DB.User(Request.Form["username_ID"], Request.Form["password_ID"]);
                bool flag = user.Login();
                if (flag)
                {
                    mUser = user;
                    loginlabel.Text = "Logged in as: " + mUser.GetUserName();
                }
                else
                {
                    Response.Redirect("index.aspx", false);
                }
                
            }

        }
    }
}