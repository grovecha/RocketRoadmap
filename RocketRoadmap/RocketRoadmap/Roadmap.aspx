﻿
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Roadmap.aspx.cs" Inherits="RocketRoadmap.Roadmap" %>

<!DOCTYPE html>
<html lang="en">

<head>  

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Roadmap</title>
    
    <!-- Draggable -->
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css">

    <!-- RangeSlider -->
    <link rel="stylesheet" href="css/normalize.css" />
    <link rel="stylesheet" href="css/ion.rangeSlider.css" />
    <link rel="stylesheet" href="css/ion.rangeSlider.skinFlat.css" />

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
            <a class="navbar-brand" href="home.aspx">Enterprise Architecture Roadmap</a>
        </div>
        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav">
                <li>
                    <a data-toggle="modal" href="#roadModal"> New Roadmap</a>
                </li>
                <li>
                    <a href="index.aspx">Logout</a>
                </li>
           
            </ul>
        </div>
        <!-- /.navbar-collapse -->

    </div>
    <!-- /.container -->
</nav>

    <br />

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <%--<!-- Custom CSS -->--%>
    <link href="css/simple-sidebar.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/simple-sidebar.css" rel="stylesheet">

    
    

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->



</head>

<body>
<form id="form1" runat="server">
<asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>



                                                
<div id="wrapper">

    <!-- Sidebar -->
    <div id="sidebar-wrapper">
        <br />        
        <br />
        <input style="width: 100px;" id="addtimelinetick" type="text" onkeyup="addTick(event,this)" placeholder='Add Timeline'/><input style="width: 120px;"  type="button" value="Toggle Timeline" onclick="showTime()">
        <!-- Data Input -->
        <div id="mainDiv">
           <table id ="sidebarTable" runat ="server">
            <tr id="StratBox0Row">
             <td>  
                 <input id='StratBox0' class ='txtStrat' BusTotal="1"  type ='text' placeholder='Add Strategy Point' runat='server'  onkeyup ='addStrat(event,this,1)'/><a href="#" id='StratDelete0' style="color:white; font-size:20px; vertical-align:-3px;" class="remove_strat"> X</a><br/>
                    <table id ="StratBox0Table" runat='server' >
                        <tr id="StratBox0BusBox0Row">
                         <td id ="StratBox0BusBox0Cell" runat="server">
                           <input id ='StratBox0BusBox0'  class='txtBus'  ProjTotal="1"  type ='text' placeholder='Add Business Value' runat='server' onkeyup ='addBus(event, this,1)' /><a href="#" id='StratBox0BusBox0Delete' style="color:white; font-size:20px; vertical-align:-3px;" class="remove_bus"> X</a>
                            <div id="StratBox0BusBox0projDiv">   
                             <input id ='StratBox0BusBox0ProjBox0' class ='txtProjDel' type='text' placeholder ='Add Project' runat='server' onkeyup ='addProj(event, this,1)' />
                            </div>
                          
                        </td>
                      </tr>
                  </table>                                
            
 
            </td>
          </tr>
        </table>


          </div> 
        </div>
       
    
    <!-- /#sidebar-wrapper -->

    <!-- Page Content -->
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h1 id="roadmapnamelabel" runat="server"></h1>
                    <a href="#menu-toggle" class="btn btn-default" id="menu-toggle">Toggle Menu</a>

                </div>
                <br />
                <div id="containment-wrapper">
                  
                </div>

                <table id="roadmapTable" style="width:100%" runat="server">                
        
                </table>



            </div>
        </div>
    </div>
    <!-- /#page-content-wrapper -->



</div>
<!-- /#wrapper -->


 <!-- Modal input -->

 <div id="inputModal" class="modal fade" role ="dialog">

    <div class="modal-dialog modal-lg">

        <!-- Modal Content -->

        <div class="modal-content">

        <div class="modal-header">

            <button type="button" class="close" data-dismiss="modal">&times;</button>

            <h3 class="modal-title" id="input_title"></h3>

        </div>
            <div class="modal-body">

                <!-- Description Text Box -->
                <h2><u>Description</u></h2>
                    <textarea id="descText" rows="4" cols="75" ></textarea> 

                <!--Depedency Input -->
                <h2><u>Dependencies</u></h2>
                    <button type="button" class="btn btn-default" id="addText">Add Input</button>
                    <div class="depText">
                        <div></div>
                    </div> 

                <!--Select Input-->
                    <button type="button" id="addSelect" class="btn btn-default">Add Selection</button>
                    <div class="depSelect">
                        <div></div>
                    </div>


                <!-- Risks Text Box -->
                <h2><u>Risks</u></h2>
                    <textarea id="riskText" rows="4" cols="75"></textarea> 

                <!-- Links Input -->
                <h2><u>Links</u></h2>
                    <button type="button" class="btn btn-default" id="addLink">Add Link</button>
                    <div class="linkText">     
                        <div></div>
                    </div>
 
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id ="save">Save</button>
                </div>
            </div>
        </div> 
    </div>
 </div>




    <!-- Display Modal-->
     <div id="displayModal" class="modal fade" role ="dialog">

    <div class="modal-dialog modal-lg">

        <!-- Modal Content -->

        <div class="modal-content">

        <div class="modal-header">

            <button type="button" class="close" data-dismiss="modal">&times;</button>

            <h3 class="modal-title" id="display_title"></h3>

        </div>
            <div class="modal-body">

                <!-- Description Text Box -->
                <h2><u>Description</u></h2>
                    <textarea id="disdescText" rows="4" cols="75"></textarea> 
                    <br />

                <!--Depedency Input -->
                <h2><u>Dependencies</u></h2>
                    <div class="disdepText">                        
                    </div> 

                <!--Select Input-->
                    <div class="disdepSelect">
                    </div>

                <!-- Risks Text Box -->
                <h2><u>Risks</u></h2>
                    <textarea id="disriskText" rows="4" cols="75"></textarea> 

                <!-- Links Input -->
                <h2><u>Links</u></h2>
                    <div class="dislinkText">
                    </div>
            </div>
        </div> 
    </div>
 </div>



<!-- jQuery -->


<script src="js/bootstrap.min.js"></script>
<script src="js/Roadmap.js"></script>
<script src="js/InputModal.js"></script>


<!-- Data Input -->
        <style>
        .txtStrat {
            width: 200px;
            height:30px;
            margin-left: 0px;
        }
        .btnDelete {
            height: 32px;
            width: 28px;
        }
        .txtBus {
            width: 180px;
            height: 30px;
            margin-left: 20px;
        }
        .txtProj {
            width: 160px;
            height: 30px;
            margin-left: 40px;
        }
        .txtProjDel {
            width: 160px;
            height: 30px;
            margin-left: 40px;
        }
        .btnStrat{
            height:200px;
            width:200px;
        }   
        .proj1 {
           
              width: 90px; height: 20px;  cursor: e-resize; background-color:deepskyblue         
        }
        .proj2 {
           
              left: 100px;    
              width: 90px; height: 20px;  cursor: e-resize; background-color:deepskyblue         
        }
        .proj3 {
           
                     
              left: 200px;  
              width: 90px; height: 20px;  cursor: e-resize; background-color:deepskyblue
        }
        .space {
          
              left: 200px;  
              width: 90px; height: 10px;  cursor: e-resize; background-color:white
        }
       span {
          display: inline-block;
          vertical-align: middle;
          line-height: normal;      
        }

       .timeline {
    border-left: solid lightgrey;
	border-left-style: dashed;
	height: 2500px;
	cursor: e-resize;
    position: absolute;
    top: -5px;
}

  #timelineborder {
      
      left: 150px;
      
  }

.timelineText {
    left: -10px;
    top: -25px;
	position: relative;
    z-index: 5;
	
}

#containment-wrapper {position: absolute; left: 150px; width: 100%;   }
   
    </style>

    <!-- /#Data Input-->
   
    </form>

<script src="js/ion.rangeSlider.js"></script>


</body>

</html>
