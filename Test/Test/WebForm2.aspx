<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Test.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>

            <asp:Button runat="server" id="testbutton" text="Test" OnClick="Button_Click" />
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>

    </form>
</body>
</html>
