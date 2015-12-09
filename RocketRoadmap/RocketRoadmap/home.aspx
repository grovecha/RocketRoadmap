<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="RocketRoadmap.home" %>

<!DOCTYPE html>

<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Rocket Roadmap</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
       <link href="css/Home.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <style>
        body {
            padding-top: 70px;
            /* Required padding for .navbar-fixed-top. Remove if using .navbar-static-top. Change if height of navigation changes. */
            /*cursor: url(/RocketRoadmap/images/Rocket.cur), auto;*/
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
<form id="form1" runat="server">
<input type =hidden name ="__EVENTTARGET" value ="">
<input type =hidden name ="__EVENTARGUMENT" value ="">
<asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>

   <div class="modal fade" id="roadModal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header" id="roadmapmodal">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="roadmapmodal2">New Roadmap</h4>
                    </div>
                    <div class="modal-body">
                        <h2><u>Roadmap Name</u></h2>
                        <input type="text" id="roadmap_Name" style="width:90%" runat="server" />
                        <p></p>
                        <h2><u>Roadmap Description</u></h2>
                        <textarea id="roadmap_Desc" style="width:90%" runat="server"></textarea>
                        <p></p>
                       
                    </div>
                    <div class="modal-footer">
                        <asp:button type="button" id="createbutton" class="btn btn-default" onclick="newroadmap" runat="server" Text="Create"></asp:button>
                    </div>
                </div>
            </div>
        </div>

    <div class="modal fade" id="DeleteModal" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header" id="areyousure">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="deleteModalTitle" modalName="">Delete?</h4>
                    </div>
                     <div class="modal-body">
                        <h4>Are you sure you want to delete this?</h4>
                     </div>
                    <div class="modal-footer">
                        <button type="button" id="Button2" class="btn btn-default" onclick="deleteModal();" >Delete</button>
                    </div>
                </div>
            </div>
        </div>

<!-- Navigation -->
<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
    <!-- Brand and toggle get grouped for better mobile display-->
    <div class="navbar-header">
        <a class="navbar-brand" href="#">Enterprise Architecture Roadmap</a>
    </div>
       
        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav pull-right">
                <li><a data-toggle="modal" href="#roadModal" class="pull-left">New Roadmap</a></li>
                <li><a href="index.aspx" class="pull-right">Logout</a></li>
                <li><asp:Textbox  id = "search_text" runat="server" placeholder ="Search..."  /><asp:button type="button" ID="searchb" class="btn btn-default" runat="server" text ="Search" onclick="searchRoadmaps"/></li>

            </ul>
        </div>
        <!-- /.navbar-collapse -->

   
    <!-- /.container -->
</nav>

<!-- Page Content -->
<h1 id="search_name" runat="server"></h1>
    <asp:Table ID="searchtable" runat="server" class="table table-striped table-bordered" cellspacing="0" width="100%"> 
    </asp:Table>
<h1 ID="name" runat="server"></h1>  
    <asp:Table ID="userroadmaps" runat="server" class="table table-striped table-bordered" cellspacing="0" width="100%">   
    </asp:Table>
<h1 id="all">All Roadmaps</h1>
<asp:Table ID="allroadmaps" runat="server" class="table table-striped table-bordered" cellspacing="0" width="100%">   
</asp:Table> 
 
</form>
    
<!-- jQuery Version 1.11.1 -->
<script src="js/jquery.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="js/bootstrap.min.js"></script>
<script id="HomeJS" src="js/Home.js"></script>


</body>

</html>
