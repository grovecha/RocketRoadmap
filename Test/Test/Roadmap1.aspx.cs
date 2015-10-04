using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Test.Components;

namespace Test
{
    public partial class Roadmap1 : System.Web.UI.Page
    {
        List<string> StrategyPoints = new List<string>();
        List<string> BusinessValueControls = new List<String>();
        Table table;
        int SPcounter = 0;
        int BVCcounter = 0;


        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            //StrategyPoints = (List<string>)ViewState["StrategyPoints"];
            //BusinessValueControls = (List<string>)ViewState["BusinessValueControls"];

            //foreach (string id in StrategyPoints)
            //{
            //    SPcounter++;
            //    TableRow row1 = new TableRow();
            //    TableCell cell = new TableCell();
            //    Table1.Rows.Add(row1);
            //    row1.Cells.Add(cell);

            //    StrategyPoint test = new StrategyPoint();
            //    test.ID = "Button" + SPcounter.ToString();
            //    test.Text = "Point " + SPcounter.ToString();

            //    test.Click += new EventHandler(test.OnClick);
            //    cell.Controls.Add(test);


            //}

            //foreach (string id in BusinessValueControls)
            //{

            //    BVCcounter++;
            //    Button control = new Button();
            //    control.ID = "BV" + BVCcounter.ToString();
            //    control.Text = "Add Business Value";

            //    LiteralControl linebreak = new LiteralControl("<br />");

            //    BVal1.Controls.Add(control);
            //    BVal1.Controls.Add(linebreak);
            //}
        }


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            //SPcounter++;
            //TableRow row = new TableRow();
            //row.ID = "StratRow" + SPcounter.ToString();
            //TableCell cell = new TableCell();

            //Table1.Rows.Add(row);
            //row.Cells.Add(cell);


            //StrategyPoint stratpt = new StrategyPoint();
            //stratpt.ID = "Button" + SPcounter.ToString();
            //stratpt.Text = "Point " + SPcounter.ToString();

            ////stratpt.Click += new EventHandler(stratpt.OnClick);

            ////StrategyPoints.Add(stratpt.ID);
            ////ViewState["StrategyPoints"] = StrategyPoints;
            //cell.Controls.Add(stratpt);




            //if (BVCcounter == 0)
            //{
            //    BVCcounter++;
            //    Button control = new Button();
            //    control.ID = "BV" + BVCcounter.ToString();
            //    control.Text = "Add Business Value";


            //    BVal1.Controls.Add(control);
            //    BVal1.Controls.Add(linebreak);

            //    BusinessValueControls.Add(control.ID);
            //    ViewState["BusinessValueControls"] = BusinessValueControls;

            //}
        }

    }
}