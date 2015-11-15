
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


    <!-- Navigation -->
<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation"> 
         <!-- Brand and toggle get grouped for better mobile display --> 
         <div class="navbar-header"> 
             <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> 
                 <span class="sr-only">Toggle navigation</span> 
                 <span class="icon-bar"></span> 
                 <span class="icon-bar"></span> 
                 <span class="icon-bar"></span> 
             </button> 
             <a class="navbar-brand navbar-left">Enterprise Roadmap Tool</a> 
             <a class="navbar-brand navbar-left" href="home.aspx">Roadmap Home</a> 
         </div> 
     <div class="container"> 
         <!-- Collect the nav links, forms, and other content for toggling --> 
         <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1"> 
             <ul class="nav navbar-nav pull-right"> 
                 <li><a href="#helpModal" data-toggle="modal" class ="navbar-right">User Guide</a> </li> 
                 <li><a href="index.aspx" class ="navbar-right">Logout</a></li> 
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

    
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css">

    

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
    <div id="sidebar-wrapper" Present="false">
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
                            <div id="StratBox0BusBox0projDiv" runat="server">   
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
    <div id="page-content-wrapper" >
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h1 id="roadmapnamelabel" runat="server"></h1>
                    <a href="#menu-toggle" class="btn btn-default" id="menu-toggle">Toggle Menu</a>

                </div>
                <br />
                <div id="containmentWrapper" runat="server">
                  
                </div>
                <div class="block"></div>
          
           

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
<div id="helpModal" class="modal fade" role ="dialog"> 

     <div class="modal-dialog modal-lg">  
          <!-- Modal Content --> 
         <div class="modal-content"> 
 
             <div class="modal-header">  
 
               <button type="button" class="close" data-dismiss="modal">&times;</button>  
 
                    <h3 class="modal-title" id="help_title">User Guide</h3> 
 
             </div> 
 
                <div class="modal-body"> 
                    <p>Hello, welcome to the Enterprise Roadmap Tool's User Guide</p> 
             
                    <h4><b>Adding, Editing, and Removing Visual Components</b></h4> 
                    <p>In order to add Strategy Points, Business Values, or Projects; the user must enter the text into the left sidebar and press enter. *Note: Data does not save until enter is pressed</p> 
                    <p>In order to change the text of the entered component, the user must type in the input boxes and press the enter key. </p> 
                    <p>In order to to remove a visual component completely, the user must click the X next to the corresponding input box. This will erase the all lower tiered components as well. </p> 
                    <p>When a Project is added to the page, the user is able to click and drag the object horizontally or grab either end of the project to resize the arrow.</p> 
   
 
                    <h4><b>Adding and Removing Timeline</b></h4> 
                    <p>In order to add a timeline component, the user must enter the desired title into the top box of the left sidebar, upon clicking the add button a timeline will appear.</p> 
                    <p>Timeline pieces are all draggable at anytime. When the user stops dragging the timeline and releases the mouse click, the position will be saved.</p> 
                    <p>In order to remove a timeline tick, double-click the timeline title.</p> 
    
 
                    <h4><b>Modal</b></h4> 
                    <p>In order for a modal to appear in either the editing or presentation mode, double-click a project object.</p> 
                    <p>Use the buttons to add the desired amount of dependencies or links.</p> 
                    <p>In order to save any information on the modal, the save button must be clicked, clicking the X or clicking outside the modal will close it without saving.</p> 
   
 
                    <h4><b>Highlighting</b></h4> 
                    <p>In Presentation mode, a single click on any Project object will highlight the projects that are dependent on that project and the projects that that project depends on.</p> 
                    <p>Projects that are depending on the chosen project: ORANGE</p> 
                    <p>Projects that the chosen project depend on: YELLOW</p> 
    
 
                <div class="modal-footer"> 
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button> 
                </div> 
                </div> 
          </div>  
        </div> 
</div> 
     




<!-- jQuery -->


<script src="js/bootstrap.min.js"></script>
<script src="js/Highlight.js"></script> 
<script src="js/Roadmap.js"></script>
<script src="js/InputModal.js"></script>


<!-- Data Input -->
        <style>
        .txtStrat {
           
            width: 150px;
            height:30px;
            margin-left: 0px;
            font-family:Arial;
        }
        .btnDelete {
            height: 32px;
            width: 28px;
        }
        .txtBus {
            width: 180px;
            height: 30px;
            margin-left: 20px;
            font-family:Arial;

        }
        .txtProj {
            width: 160px;
            height: 30px;
            margin-left: 40px;
            font-family:Arial;

        }
        .txtProjDel {
            width: 160px;
            height: 30px;
            margin-left: 40px;
            font-family:Arial;

        }
        .btnStrat{
            height:200px;
            width:200px;
        }   
        
      
        .space {
          
              left: 200px;  
              width: 90px; height: 10px; background-color:#e6f2c6
        }
       span {
          display: inline-block;
          vertical-align: middle;
          line-height: normal;      
        }

       .StratVis{
              word-break: break-all;
            white-space: normal;
            /*-moz-box-shadow:inset 0px -3px 7px 0px #7a1c1c;
	        -webkit-box-shadow:inset 0px -3px 7px 0px #7a1c1c;
	        box-shadow:inset 0px -3px 7px 0px #7a1c1c;*/
         	/*background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #b5121b), color-stop(1, #b5121b));
        	background:-moz-linear-gradient(top, #b5121b 5%, #b5121b 100%);
        	background:-webkit-linear-gradient(top, #b5121b 5%, #b5121b 100%);
        	background:-o-linear-gradient(top, #b5121b 5%, #b5121b 100%);
	        background:-ms-linear-gradient(top, #b5121b 5%, #b5121b 100%);
	        background:linear-gradient(to bottom, #b5121b 5%, #b5121b 100%);
	        filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#b5121b', endColorstr='b5121b',GradientType=0);
	        background-color:#b5121b;*/
	        -moz-border-radius:3px;
	        -webkit-border-radius:3px;
	        border-radius:3px;
	        border:1px solid #3c521e;
	        display:inline-block;
	        cursor:pointer;
	        color:#f5f5f5;
	        font-family:Arial;
	        font-size:15px;
	        padding:9px 23px;
	        text-decoration:none;
	        text-shadow:0px 1px 0px #263666;
            width: 150px;

       }

       .RowVis{
           height:100px;
           border-width: 1px;
           border-bottom-style:solid;
           background-color: #e6f2c6;
       }
       .CellVis{
           width:100px;
           text-align:right;
           background-color:#e6f2c6;
           padding:0;

       }
       .NewCellVis {
           background-color: #e6f2c6;
       }

       .projtd {
           padding:0;
           width: 2500px;
           background-color:#e6f2c6;
       }

       .ProjVis{
       }

       .BusVis {
           width: 100px;
           text-align:right;
           background-color: #e6f2c6;
           padding:0
       }

       .remove_strat {
           color:white; 
           font-size:20px; 
           vertical-align:-3px;
       }
       .remove_bus {
           color:white;
           font-size:20px;
           vertical-align:-3px;
       }
       .remove_proj {
           color:white; font-size:20px; vertical-align:-3px
       }


       .timeline {
    border-left: solid lightgrey;
	border-left-style: dashed;
	height: 2500px;
	cursor: e-resize;
    position: absolute;
    top: -3px;
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

#containmentWrapper {position: absolute; left: 150px; width: 100%;   }

 .proj1:after, .proj2:after, .proj3:after {
  color: black;
  border-left: 10px solid;
  border-top: 10px solid transparent;
  border-bottom: 10px solid transparent;
  display: inline-block;
  content: '';
  position: absolute;
  right: -10px;
 
}
.proj1, .proj2, .proj3 {
  background-color:  black;
  width: 150px;
  height: 20px;
  display: block;
  position: relative;
  text-align: center;
    text-indent: 10px;
    font-size: 15px;
    
}
.proj1:before, .proj2:before, .proj3:before {
    content: "";
    position: absolute;
    display: block;
    border-top: 10px solid transparent;
    border-left: 10px solid #e6f2c6;
    border-bottom: 10px solid transparent;    
}
textarea{resize:vertical; max-height:300px}
   
    </style>

    <!-- /#Data Input-->
   
    </form>

<script src="js/ion.rangeSlider.js"></script>


</body>

</html>
