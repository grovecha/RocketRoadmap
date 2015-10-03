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
        List<string> controlIDList = new List <string>();
        int counter = 0;


        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            controlIDList = (List<string>)ViewState["controlIDList"];
            foreach (string id in controlIDList)
            {

                counter++;
                Button test = new Button();
                test.ID = "Button" + counter.ToString();
                test.Text = "Button" + counter.ToString() ;

                LiteralControl linebreak = new LiteralControl("<br />");

                PlaceHolder1.Controls.Add(test);
                PlaceHolder1.Controls.Add(linebreak);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            counter++;
            Button test = new Button();
            test.ID = "Button" + counter.ToString();
            test.Text = "Button" + counter.ToString() ;

            LiteralControl linebreak = new LiteralControl("<br />");

            PlaceHolder1.Controls.Add(test);
            PlaceHolder1.Controls.Add(linebreak);

            controlIDList.Add(test.ID);
            ViewState["controlIDList"] = controlIDList;
        }
    }
}