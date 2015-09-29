using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Test.Components;

namespace Test
{
    public partial class Roadmap : System.Web.UI.Page
    {
        List<string> controlIDList = new List<string>();
        int counter = 0;


        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            controlIDList = (List<string>)ViewState["controlIDList"];
            foreach (string id in controlIDList)
            {

                counter++;
                StrategyPoint test = new StrategyPoint();
                test.ID = "Button" + counter.ToString();
                test.Text = "Point " + counter.ToString();

                test.Click += new EventHandler(this.Point_Button_Click);

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
            StrategyPoint test = new StrategyPoint();
            test.ID = "Button" + counter.ToString();
            test.Text = "Point " + counter.ToString();

            test.Click += new EventHandler(this.Point_Button_Click);

            //test.Attributes.Add("OnClick", "Point_Button_Click");


            LiteralControl linebreak = new LiteralControl("<br />");

            PlaceHolder1.Controls.Add(test);
            PlaceHolder1.Controls.Add(linebreak);

            controlIDList.Add(test.ID);
            ViewState["controlIDList"] = controlIDList;
        }

        protected void Point_Button_Click(object sender, EventArgs e)
        {
            

        }
    }
}