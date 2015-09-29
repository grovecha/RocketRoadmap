using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HelloWorldLabel.Text = "Hello, world!";
             
            

            // These values can come from anywhere, but right now, we just hardcode them
            //phUserInfoBox.Controls.Add(LoadControl("~/UserInfoBoxControl.ascx"));

            UserInfoBoxControl userInfoBoxControl = (UserInfoBoxControl)LoadControl("~/UserInfoBoxControl.ascx");
            userInfoBoxControl.UserName = "John Doe";
            userInfoBoxControl.UserAge = 78;
            userInfoBoxControl.UserCountry = "Spain";
            phUserInfoBox.Controls.Add(userInfoBoxControl);

        }

        protected void GreetButton_Click(object sender, EventArgs e)
        {
            HelloWorldLabel.Text = TextInput.Text;
        }

        protected void GreetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            HelloWorldLabel.Text = "Hello, " + GreetList.SelectedValue;
        }

        protected void MyEventUserControl_PageTitleUpdated(object sender, EventArgs e)
        {
            this.Title = MyEventUserControl.PageTitle;
        }
    }
}