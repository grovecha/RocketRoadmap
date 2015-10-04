using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Test.Components;
namespace Test.Components
{
    public class StrategyPoint : Button
    {

        public StrategyPoint ()
        {
            Width = 200;
            Height = 100;
        }


        public void OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "disModal", "$('#displayModal').modal('show');", true);
        }



    }
}