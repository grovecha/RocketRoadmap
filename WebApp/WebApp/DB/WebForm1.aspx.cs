using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static WebApp.DB.Database;


namespace WebApp.DB
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DB.Database db = new Database();
            db.connect();
            name.InnerText = db.getusername();
        }
    }
}