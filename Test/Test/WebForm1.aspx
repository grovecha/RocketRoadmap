<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Test.WebForm1" %>
<%@ Reference Control="~/UserInfoBoxControl.ascx" %>

<%@ Register TagPrefix="My" TagName="EventUserControl" Src="~/EventUserControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <My:EventUserControl runat="server" ID="MyEventUserControl" OnPageTitleUpdated="MyEventUserControl_PageTitleUpdated" />
             <br /><br />
             <br /><br />

    <asp:PlaceHolder runat="server" ID="phUserInfoBox" />
     <br /><br />
    <div>
           <asp:Label runat="server" id="HelloWorldLabel"></asp:Label>
    <br /><br />
    <asp:TextBox runat="server" id="TextInput" /> 
    <asp:Button runat="server" id="GreetButton" text="Say Hello!" OnClick="GreetButton_Click" />

    <br /><br />
    <asp:DropDownList runat="server" id="GreetList" autopostback="true" onselectedindexchanged="GreetList_SelectedIndexChanged">
    <asp:ListItem value="no one">No one</asp:ListItem>
    <asp:ListItem value="world">World</asp:ListItem>
    <asp:ListItem value="universe">Universe</asp:ListItem>

                
</asp:DropDownList>
    </div>



    </form>


</body>
</html>
