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
                <li><asp:Textbox id = "search_text" runat="server" placeholder ="Search..."  /><asp:Button ID="searchb" runat="server" text ="Search" OnClick="searchRoadmaps"/></li>

            </ul>
        </div>
        <!-- /.navbar-collapse -->

   
    <!-- /.container -->
</nav>


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
                        <textarea id="roadmap_Desc" style="width:90%"  runat="server"></textarea>
                        <p></p>
                       
                    </div>
                    <div class="modal-footer">
                        <asp:button type="button" id="createbutton" class="btn btn-default" onclick="newroadmap" runat="server" Text="Create"></asp:button>
                    </div>
                </div>
            </div>
        </div>


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
 
<!-- /.container -->

<!-- jQuery Version 1.11.1 -->
<script src="js/jquery.js"></script>

<!-- Bootstrap Core JavaScript -->
<script src="js/bootstrap.min.js"></script>


<style>
    #search_text{
        width: 18.75em;
        height: 3.125em;
        background: #2b303b;
        border: none;
        font-size: 10pt;
        float: left;
        color: #63717f;
        padding-left: 2.813em;
        -webkit-border-radius: .313em;
        -moz-border-radius: .313em;
        border-radius: .313em;
    }
    #name{
        text-align: center;
    }
    #all{
        text-align: center;
    }
    #search_name{
        text-align: center;
    }
    #searchb {
	-moz-box-shadow:inset 0em 2.125em 0em -1em #b54b3a;
	-webkit-box-shadow:inset 0em 2.125em 0px -1em #b54b3a;
	box-shadow:inset 0em 2.125em 0em -1em #b54b3a;
	background-color:#b5121b;
	border:.063em solid #241d13;
	display:inline-block;
	cursor:pointer;
	color:#ffffff;
	font-family:Arial;
	font-size:1em;
	font-weight:bold;
	padding:.563em 1.5em;
	text-decoration:none;
	text-shadow:0em -.063em 0em #7a2a1d;
    height: 3.125em;
    }
    #roadmapmodal{
           padding:.563em 1em;
    border-bottom:.063em solid #eee;
    -webkit-border-top-left-radius: .313em;
    -webkit-border-top-right-radius: .313em;
    -moz-border-radius-topleft: .313em;
    -moz-border-radius-topright: .313em;
    border-top-left-radius: .313em;
    border-top-right-radius: .313em;
    background-color:#f24537;
    font-family:Arial;
    
    }
    #createbutton{
	-moz-box-shadow: 0em 0em 0em 0em #cf866c;
	-webkit-box-shadow: 0em 0em 0em 0em #cf866c;
	box-shadow: 0em 0em 0em 0em #cf866c;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #d0451b), color-stop(1, #bc3315));
	background:-moz-linear-gradient(top, #d0451b 5%, #bc3315 100%);
	background:-webkit-linear-gradient(top, #d0451b 5%, #bc3315 100%);
	background:-o-linear-gradient(top, #d0451b 5%, #bc3315 100%);
	background:-ms-linear-gradient(top, #d0451b 5%, #bc3315 100%);
	background:linear-gradient(to bottom, #d0451b 5%, #bc3315 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#d0451b', endColorstr='#bc3315',GradientType=0);
	background-color:#d0451b;
	-moz-border-radius:1em;
	-webkit-border-radius:1em;
	border-radius:1em;
	border:.063em solid #942911;
	display:inline-block;
	cursor:pointer;
	color:#ffffff;
	font-family:Arial;
	font-size:1.00em;
	padding:.375em 1.5em;
	text-decoration:none;
    }

    table, td, th {

    }
    th {
        background-color: #b5121b !important;
        color: white !important;
        font-family: Arial !important;
     }
    #norm {
        width: 15% !important;
    }
    #wide {
        width: 30% !important;
    }

a:link {
    color: black;
}

/* visited link */
a:visited {
    color: black;
}

/* mouse over link */
a:hover {
    color: #b5121b;
}

</style>
</form>
</body>

</html>
