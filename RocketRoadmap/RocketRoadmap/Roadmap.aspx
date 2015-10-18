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
            <tr id="StratBox0Row">
             <td>
               
                 <input id='StratBox0' class ='txtStrat' BusTotal=1  type ='text' placeholder='Add Strategy Point' onkeypress ='addStrat(event,this,1)'/><br/>
                    <table id ="StratBox0Table" >
                        <tr id="StratBox0BusBox0Row">
                         <td>
                           <input  class ='txtBus' ProjTotal=1 id ='StratBox0BusBox0' type ='text'placeholder='Add Business Value' onkeypress ='addBus(event, this,1)' />
                                    <div id="projDiv">
                                         <input id ='StratBox0BusBox0ProjBox0' class ='txtProj' type='text'placeholder ='Add Project' onkeypress ='addProj(event, this,1)' />
                                         </div> 
                          
                        </td>
                      </tr>
                  </table>                                
            
 
            </td>
          </tr>
        </table>


          </div> 
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
    
    <!-- /#sidebar-wrapper -->

    <!-- Page Content -->
<div id="page-content-wrapper">

 <div class="container-fluid">

 <div class="row">

 <div class="col-lg-12">

 <h1>Roadmap</h1>

 <a href="#menu-toggle" class="btn btn-default" id="menu-toggle">Toggle Menu</a>

 <button type="button" class="btn btn-default" data-toggle="modal" data-target="#inputModal"> Modal</button>

 </div>

 <br />

 <table id="roadmap-table">
 
 </table>

 </div>

 </div>

 </div>

 <!-- /#page-content-wrapper -->




　

<!-- /#wrapper -->
 
 <!-- Modal -->

 <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">

    <div class="modal-dialog" role="document">

        <div class="modal-content">

            <div class="modal-header">

            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

            <h4 class="modal-title" id="myModalLabel">Modal title</h4>

            </div>

        <div class="modal-body">

            <h2><u>Description</u></h2>

            <p>It will be saying things about our object BLah BLah Blah</p> 

            <p></p>

            <h2><u>Dependencies</u></h2>

            <p>Becky totally depends on Johnny</p>

            <p></p>

            <h2><u>Risks</u></h2>

            <p>We are all risky</p>

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

        <h3 class="modal-title">Add Style</h3>

    </div>

        <div class="modal-body">

            <!-- Description Text Box -->

            <h2><u>Description</u></h2>

            <textarea id="descText" rows="4" cols="75">

            </textarea> 

            <p></p>

            <!--Depedency Input -->

            <h2><u>Dependencies</u></h2>

            <input type="text" id="depenText1" size="60" />

            <button type="button" class="btn btn-default" id="addText">Add Input</button>

            <div class="depText">

                <div></div>

            </div>

        <!-- Selection Dependency - Need to loop for options -->

            <select id="depenSelect">

            <option value="project1">Project1</option>

            <option value="project2">Project2</option>

            <option value="project3">Project3</option>

            <option value="project4">Project4</option>

            </select>

            <button type="button" id="addSelect" class="btn btn-default">Add Selection</button> 

            <div class="depSelect">

                <div></div>

            </div>

            <p></p>

            <!-- Risks Text Box -->

            <h2><u>Risks</u></h2>

            <textarea id="riskText" rows="4" cols="75">

            </textarea> 

            <!-- Links Input -->

            <h2><u>Links</u></h2>

            <input type="text" id="linkText1" size="60"/> 

            <button type="button" class="btn btn-default" id="addLink">Add Link</button>

            <div class="linkText">

            <div></div>

            </div>
 
            <div class="modal-footer">

        <button type="button" class="btn btn-default" id ="saveButton">Save</button>

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
</script>\
    <script>
        $(document).ready(function () {
            var max_fields = 15; //maximum input boxes allowed
            var dep_Text = $(".depText"); //Dependency input wrapper
            var add_Text = $("#addText"); //Add dependency input
            var dep_Select = $(".depSelect"); //Dependency Selection wrapper
            var add_Select = $("#addSelect"); //Add dependency select
            var link_Text = $(".linkText"); //Link input wrapper
            var add_Link = $("#addLink"); //Add Link input
            var dep_count = 1; //initlal dependency input counter
            var select_count = 1; //initial select input counter
            var link_count = 1; //initial link count

            //Dependency addition Functions
            $(add_Text).click(function (e) { //on add input button click
                e.preventDefault();
                if (dep_count < max_fields) { //max input box allowed
                    dep_count++; //text box increment
                    $(dep_Text).append('<div><input type="text" size=60/><a href="#" class="remove_field">X</a></div>'); //add input box
                }
            });

            $(dep_Text).on("click", ".remove_field", function (e) { //user click on remove text
                e.preventDefault(); $(this).parent('div').remove(); x--;
            })


            //Selection addition Functions

            $(add_Select).click(function (e) { //on add input button click
                e.preventDefault();

                var options = "";
                var i, max = 10;
                for (i = 0; i < max; i++) {
                    options += "<option value='project" + i + ">Option" + i + "</option>";
                }

                if (select_count < max_fields) { //max input box allowed
                    select_count++; //text box increment
                    $(dep_Select).append('<div> <select id="depenSelect">' + options + '</select><a href="#" class="remove_field">X</a></div>'); //add input box
                }
            });

            $(dep_Select).on("click", ".remove_field", function (e) { //user click on remove text
                e.preventDefault(); $(this).parent('div').remove(); x--;
            })


            //Links Addition Functions
            $(add_Link).click(function (e) { //on add input button click
                e.preventDefault();
                if (link_count < max_fields) { //max input box allowed
                    link_count++; //text box increment
                    $(link_Text).append('<div><input type="text" size=60/><a href="#" class="remove_field">X</a></div>'); //add input box
                }
            });

            $(wrapper).on("click", ".remove_field", function (e) { //user click on remove text
                e.preventDefault(); $(this).parent('div').remove(); x--;
            })


            $(add_Link).click(function (e) {
                //make the ajax calls in here?

            })
        });
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