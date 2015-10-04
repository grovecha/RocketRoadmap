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
        List<string> StrategyPoints = new List<string>();
        List<string> BusinessValueControls = new List<String>();
        
        int SPcounter = 0;
        int BVCcounter = 0;


        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            StrategyPoints = (List<string>)ViewState["StrategyPoints"];
            BusinessValueControls = (List<string>)ViewState["BusinessValueControls"];

            foreach (string id in StrategyPoints)
            {

                SPcounter++;
                StrategyPoint test = new StrategyPoint();
                test.ID = "Button" + SPcounter.ToString();
                test.Text = "Point " + SPcounter.ToString();

                test.Click += new EventHandler(test.OnClick);

                LiteralControl linebreak = new LiteralControl("<br />");
                //PlaceHolder1.Controls.Add(test);
                //PlaceHolder1.Controls.Add(linebreak);

                PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<td>"));
                PlaceHolder1.Controls.Add(test);
                PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));

                LiteralControl linebreak = new LiteralControl("<br />");
                //PlaceHolder1.Controls.Add(test);
                //PlaceHolder1.Controls.Add(linebreak);
                string rowid = "<tr id= \"Row" + SPcounter + "\">";
                PlaceHolder1.Controls.Add(new LiteralControl(rowid));

                PlaceHolder1.Controls.Add(new LiteralControl("<td id=\"Col\">"));
                PlaceHolder1.Controls.Add(test);
                PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
                PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));

                //    test.Click += new EventHandler(test.OnClick);

                //    LiteralControl linebreak = new LiteralControl("<br />");
                //    //PlaceHolder1.Controls.Add(test);
                //    //PlaceHolder1.Controls.Add(linebreak);

                //    PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
                //    PlaceHolder1.Controls.Add(new LiteralControl("<td>"));
                //    PlaceHolder1.Controls.Add(test);
                //    PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
                //    PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));

            }

            foreach (string id in BusinessValueControls)
            {

                BVCcounter++;
                Button control = new Button();
                control.ID = "BV" + BVCcounter.ToString();
                control.Text = "Add Business Value";

                LiteralControl linebreak = new LiteralControl("<br />");

                //BVal1.Controls.Add(control);
                //BVal1.Controls.Add(linebreak);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            SPcounter++;
            StrategyPoint test = new StrategyPoint();
            test.ID = "Button" + SPcounter.ToString();
            test.Text = "Point " + SPcounter.ToString();

            test.Click += new EventHandler(test.OnClick);



            string rowid = "<tr id= \"Row" + SPcounter + "\">";
            PlaceHolder1.Controls.Add(new LiteralControl(rowid));
            PlaceHolder1.Controls.Add(new LiteralControl("<td>"));
            PlaceHolder1.Controls.Add(test);
            PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));

            //test.Attributes.Add("OnClick", "Point_Button_Click");



            LiteralControl linebreak = new LiteralControl("<br />");
            //PlaceHolder1.Controls.Add(test);
            //PlaceHolder1.Controls.Add(linebreak);

            PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl("<td>"));
            PlaceHolder1.Controls.Add(test);
            PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));

            StrategyPoints.Add(test.ID);
            ViewState["StrategyPoints"] = StrategyPoints;

            if (BVCcounter == 0)
            {
                BVCcounter++;
                Button control = new Button();
                control.ID = "BV" + BVCcounter.ToString();
                control.Text = "Add Business Value";


                BVal1.Controls.Add(control);
                BVal1.Controls.Add(linebreak);

                BusinessValueControls.Add(control.ID);
                ViewState["BusinessValueControls"] = BusinessValueControls;

            }
        }
       

    }
}