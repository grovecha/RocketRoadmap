using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            count += 1;
            Button test = new Button();
            newbutton.Controls.Add(test);
            test.Text = "This Worked!"+count.ToString();
            
            
        }
    }
}