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

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/simple-sidebar.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/simple-sidebar.css" rel="stylesheet">


    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <![endif]-->



</head>

<body>
<form id="form1" runat="server">
                            <asp:ScriptManager runat="server"></asp:ScriptManager>



                                                
<div id="wrapper">

    <!-- Sidebar -->
    <div id="sidebar-wrapper">

        <!-- Data Input -->
        <div id="mainDiv">
            <table id ="sidebar-table">
            <tr>
                <td>
                    <input class="txtStrat"id="StratBox1" type="text" placeholder="Add Strategy Point" onkeypress="addStrat(event,1)" /> 
 
                </td>
            </tr>
            </table>


           
        </div>
        <!--
            <ul class="sidebar-nav">

            <li class="sidebar-brand">
                <br />      
                <table id="sidebar">
                    <tr>
                        <td id="stratBu1">
                        <button type="button" id="button1" onclick="StratClick()"> Strategy Point</button>

                        </td>
                    </tr>
                </table>


                
               
            </li>

        </ul>
            -->
    </div>
    <!-- /#sidebar-wrapper -->

    <!-- Page Content -->
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h1>Roadmap</h1>
                    <a href="#menu-toggle" class="btn btn-default" id="menu-toggle">Toggle Menu</a>
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#inputModal"> Input modal </button>
                </div>
                <br />
                <table id="roadmap-table">



  

                </table>



            </div>
        </div>
    </div>
    <!-- /#page-content-wrapper -->

</div>


<!-- /#wrapper -->

    <!-- Modal -->
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
             d       <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Modal title</h4>
                    </div>
                    <div class="modal-body">
                        <h2><u>Description</u></h2>
                        <p> Pull the description</p>
                        <p></p>
                        <h2><u>Dependencies</u></h2>
                        <p> Pul the dependecies list</p>
                        <p></p>
                        <h2><u>Risks</u></h2>
                        <p> List of risks</p>
                        <h2><u>Links</u></h2>
                       
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

     <!-- Modal input -->
    <div id="inputModal" class="modal fade" role ="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal Content -->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title">Pull the project Title into here</h3>
                </div>
                <div class="modal-body">
                    <h2><u>Description</u></h2>
                        <textarea id="descText" rows="4" cols="75">
                        </textarea> 
                        <p></p>
                        <h2><u>Dependencies</u></h2>
                        <div id="dependencyText">
                        <input type="text" id="depenText1" size="55" /> 
                        <button type="button" class="btn btn-default" id="addButton" onclick="addClick()">Add</button>
         
                        </div>
                        <div id="dependencySelect">
                        <select id="depenSelect">
                            <option value="project1">Project1</option>
                            <option value="project2">Project2</option>
                            <option value="project3">Project3</option>
                            <option value="project4">Project4</option>
                        </select>
                        <button type="button" id="add2Button" class="btn btn-default" onclick="addClick2()">Add</button>
                      
                       </div>
                        <p></p>
                        <h2><u>Risks</u></h2>
                        <textarea id="riskText" rows="4" cols="75">
                        </textarea> 

                        <h2><u>Links</u></h2>
                     <div id="link">
                        <input type="text" id="linkText1" size="55"/> <button type="button" class="btn btn-default" id="linkButton" onclick="linkClick()">New Link</button>
                     </div>
                <div class="modal-footer">
                     <button type="button" class="btn btn-default" onclick="saveClick()">Save</button>
                </div>
            </div>
        </div> 
    </div>
  </div>





 <!-- jQuery -->
<script src="js/InputModal.js"></script>

<!-- jQuery -->
<script src="js/jquery.js"></script>


<!-- Bootstrap Core JavaScript -->
<script src="js/bootstrap.min.js"></script>

    <script src="js/Roadmap.js"></script>
<!-- Menu Toggle Script -->
<script>
    $("#menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });
</script>
<script>
    $("#menu-modal").click(function (e) {
        e.preventDefault();
        $("#myModal").modal("show");
    });
</script>

<script>
    function showModal() {                        
        $("#myModal").modal("show");
    }                    
</script>
<script>
    function addClick() {
        var count = 2;
        var mainDiv = document.getElementById('dependencyText');
        var mainroot = document.createElement("input");
        mainroot.innerHTML = "<input type='text' id='depenText" + count.toString() + "/>"
        mainDiv.appendChild(mainroot);
        mainroot.style.width = "450px"
        count++;
    }
</script>
<script>
    function addClick2() {
      var count = 2;
      var mainDiv = document.getElementById('dependencySelect');
      var newSelect = document.createElement("select");
      var newbreak = document.createElement("br");

        //creating option
      var i, s;
      for (i = 0; i < 5; i++) {
          var newOption = document.createElement("option");
          var newText = document.createTextNode("Project" + i);
          newOption.appendChild(newText);
          newSelect.appendChild(newOption);
      }

      mainDiv.appendChild(newbreak);
      mainDiv.appendChild(newSelect);
      count++;
    }
</script>

<script>
    function linkClick(){
    var count=2
    var mainDiv = document.getElementById('link');
    var mainroot = document.createElement("input");
    //mainroot.innerHTML = "<input type='text' id='link" + count.toString() + "/>"
    mainDiv.appendChild(mainroot);
    mainroot.style.width = "450px";
    count++;
    }
</script>

<script> 
    function saveClick() {
        //make the ajax calls in here?
    }


</script>


<!-- Data Input -->
        <style>
        .txtStrat {
            width: 250px;
            height: 30px;
            margin-left: 0px;
        }

        .txtBus {
            width: 230px;
            height: 30px;
            margin-left: 20px;
        }

        .txtProj {
            width: 210px;
            height: 30px;
            margin-left: 40px;
        }
        .btnStrat{
            height:200px;
            width:200px;
        }
        
        .busroot {
           
        }
        .mainroot {
           
        }
        .projroot {
         
        }
        .mainDiv {

        }
        
   
    </style>

    <!-- /#Data Input-->
   
    </form>
</body>

</html>