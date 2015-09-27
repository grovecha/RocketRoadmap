<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfoBoxControl.ascx.cs" Inherits="Test.UserInfoBoxControl" %>
<b>Information about <%= this.UserName %></b>
<br /><br />
<%= this.UserName %> is <%= this.UserAge %> years old and lives in <%= this.UserCountry %>