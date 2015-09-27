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
        <p>
            <asp:PlaceHolder runat="server" ID="newbutton" />
        </p>
        <p>
                        <asp:PlaceHolder runat="server" ID="PlaceHolder1" />
                        </p>
        <p>
                        <asp:PlaceHolder runat="server" ID="PlaceHolder2" />
                        </p>
        <p>
                        <asp:PlaceHolder runat="server" ID="PlaceHolder3" />
                        </p>
        <p>
                        <asp:PlaceHolder runat="server" ID="PlaceHolder4" />
        </p>
    </form>
</body>
</html>
