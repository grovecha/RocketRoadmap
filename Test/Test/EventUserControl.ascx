<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventUserControl.ascx.cs" Inherits="Test.EventUserControl" %>
Page title: 
<asp:TextBox runat="server" ID="txtPageTitle" />
<asp:Button runat="server" ID="btnUpdatePageTitle" OnClick="btnUpdatePageTitle_Click" Text="Update" />