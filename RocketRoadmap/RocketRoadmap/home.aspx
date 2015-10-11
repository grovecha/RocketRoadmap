<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="RocketRoadmap.home" %>

<!DOCTYPE html>

<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Bare - Start Bootstrap Template</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <style>
        body {
            padding-top: 70px;
            /* Required padding for .navbar-fixed-top. Remove if using .navbar-static-top. Change if height of navigation changes. */
        }
    </style>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>

<body>

<!-- Navigation -->
<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
    <div class="container">

        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="#">Enterprise Architecture Roadmap</a>
        </div>
        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav">
                <li>
                    <a href="index.html">Login</a>
                </li>
                <li>
                    <a href="SidebarTest.html"> New Roadmap</a>
                </li>
                <li>
                    <a><asp:Label ID="loginlabel" runat="server"></asp:Label></a>
                </li>

            </ul>
        </div>
        <!-- /.navbar-collapse -->

    </div>
    <!-- /.container -->
</nav>

<!-- Page Content -->

<div class="row">
    <div class="col-lg-12 text-center">
        <h1>Current Roadmaps</h1>

    </div>
    </div>
<table id="example" class="table table-striped table-bordered" cellspacing="0" width="100%">
    <thead>
    <tr>
        <th>Name</th>
        <th>Author</th>
        <th>Description</th>
        <th>Last Edited</th>

    </tr>
    </thead>

    <tfoot>
    <tr>
        <th>Name</th>
        <th>Author</th>
        <th>Description</th>
        <th>Last Edited</th>

    </tr>
    </tfoot>

    <tbody>
    <tr>
        <td><a href="Roadmap.html">Proj1</a></td>
        <td>Chase Grove</td>
        <td>Huge project involving stuff</td>
        <td>1/1/1</td>
    </tr>
    <tr>
        <td><a href="Roadmap.html">Proj2</a></td>
        <td>Brian Chivers</td>
        <td>Huge project involving stuff</td>
        <td>1/1/1</td>
    </tr>
    <tr>
        <td><a href="Roadmap.html">Proj3</a></td>
        <td>Emily Klopfer</td>
        <td>Huge project involving stuff</td>
        <td>1/1/1</td>
    </tr>
    <tr>
        <td><a href="Roadmap.html">Proj4</a></td>
        <td>Eric Nartker</td>
        <td>Huge project involving stuff</td>
        <td>1/1/1</td>
    </tr>
    <tr>
        <td><a href="Roadmap.html">Proj5</a></td>
        <td>Ben Toth</td>
        <td>Huge project involving stuff</td>
        <td>1/1/1</td>
    </tr>




    </tbody>
</table>
<!-- /.container -->

<!-- jQuery Version 1.11.1 -->
<script src="js/jquery.js"></script>

<!-- Bootstrap Core JavaScript -->
<script src="js/bootstrap.min.js"></script>

</body>

</html>
