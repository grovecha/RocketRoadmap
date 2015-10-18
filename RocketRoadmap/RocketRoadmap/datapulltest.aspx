<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="datapulltest.aspx.cs" Inherits="RocketRoadmap.datapulltest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Data Pull Test</div>

        <p><asp:Label runat="server" ID="RoadmapName">Roadmap Name:</asp:Label></p>
        <p><asp:Label runat="server" ID="SP">Strategy Point Name:</asp:Label></p>
        <p><asp:Label runat="server" ID="BV">Business Value Name:</asp:Label></p>
        <p><asp:Label runat="server" ID="BVDescrip">Business Value Description:</asp:Label></p>
        <p><asp:Label runat="server" ID="Proj">Project Name:</asp:Label></p>
        <p><asp:Label runat="server" ID="ProjDescrip">Project Description:</asp:Label></p>
        <p><asp:Label runat="server" ID="ProjStartDate">Project Start Date:</asp:Label></p>
        <p><asp:Label runat="server" ID="ProjEndDate">Project End Date:</asp:Label></p>
        <p><asp:Label runat="server" ID="IssueDescrip">Issue Description:</asp:Label></p>
        <p><asp:Label runat="server" ID="Link">Link Address:</asp:Label></p>
        <p><asp:Label runat="server" ID="Dependency">Dependency:</asp:Label></p>

    </form>
    <p>
        &nbsp;</p>
</body>
</html>
