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
                    <a href="index.aspx">Logout</a>
                </li>
                <li>
                    <a data-toggle="modal" href="#roadModal"> New Roadmap</a>
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
<asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>



                                                
<div id="wrapper">

    <!-- Sidebar -->
    <div id="sidebar-wrapper">
        <br />
        <br />
        <!-- Data Input -->
        <div id="mainDiv">
           <table id ="sidebarTable" runat ="server">
            <tr id="StratBox0Row">
             <td>
               
                 <input id='StratBox0' class ='txtStrat' BusTotal="1"  type ='text' placeholder='Add Strategy Point' runat='server'  onkeyup ='addStrat(event,this,1)'/><a href="#" id='StratDelete0' style="color:white; font-size:20px; vertical-align:-3px;" class="remove_strat"> X</a><br/>
                    <table id ="StratBox0Table" runat='server' >
                        <tr id="StratBox0BusBox0Row">
                         <td>
                           <input id ='StratBox0BusBox0'  class='txtBus'  ProjTotal="1"  type ='text' placeholder='Add Business Value' runat='server' onkeyup ='addBus(event, this,1)' /><a href="#" id='StratBox0BusBox0Delete' style="color:white; font-size:20px; vertical-align:-3px;" class="remove_bus"> X</a>
                                    <div id="StratBox0BusBox0projDiv" visible="false" >
                                         <input id ='StratBox0BusBox0ProjBox0' class ='txtProjDel' type='text'placeholder ='Add Project'   onkeyup ='addProj(event, this,1)' />
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
                    <h1>Roadmap</h1>
                    <a href="#menu-toggle" class="btn btn-default" id="menu-toggle">Toggle Menu</a>

                </div>
                <br />
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
                    <textarea id="descText" rows="4" cols="75"></textarea> 
                    <br />

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
                    <br />

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
                        <div></div>
                    </div> 

                <!--Select Input-->
                    <div class="disdepSelect">
                        <div></div>
                    </div>
                    <br />

                <!-- Risks Text Box -->
                <h2><u>Risks</u></h2>
                    <textarea id="disriskText" rows="4" cols="75"></textarea> 

                <!-- Links Input -->
                <h2><u>Links</u></h2>
                    <div class="dislinkText">     
                        <div></div>
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
    var panel_close = false;
    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        if (panel_close == false) {
            panel_close = true;
        } else if (panel_close == true) {
            panel_close = false;
        }
        $("#wrapper").toggleClass("toggled");
    });

    var button_id;
    function showModal(id) {
        button_id=id.substr(0,id.length-3)
        if (panel_close == false) {
            $("#inputModal").modal("show");
        } else if (panel_close == true) {
            $("#displayModal").modal("show");
        }
    }
    

    $(document).ready(function () {
        var max_fields = 10; //maximum input boxes allowed
        var dep_Text = $(".depText"); //Dependency input wrapper
        var add_Text = $("#addText"); //Add dependency input
        var dep_arr = []; // dependecy string array
        var dep_Select = $(".depSelect"); //Dependency Selection wrapper
        var add_Select = $("#addSelect"); //Add dependency select
        var select_arr = []; //select array
        var link_Text = $(".linkText"); //Link input wrapper
        var add_Link = $("#addLink"); //Add Link input
        var link_arr = []; //link array
        var save = $("#save"); //Save button
        var dep_count = 0; //initlal dependency input counter
        var select_count = 0; //initial select input counter
        var link_count = 0; //initial link count
        var test_count = 0;

        var options; // used for the string of options a select has
        var select_total = 0;

        //Getting roadmap name
        var roadmap_url = window.location.href;
        var map_Name = roadmap_url.substr(roadmap_url.indexOf('?') + 1);
        map_Name = map_Name.substr(2, map_Name.length);

        //Dependency addition Functions
        $(add_Text).on("click", function (e) { //on add input button click
            e.preventDefault();
            if (dep_count < max_fields) { //max input box allowed
                dep_count++; //text box increment
                $(dep_Text).append("<div class='new_dep'><input type='text' size=55 name='dep_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
            }
        });

        $(dep_Text).on("click", ".remove_field", function (e) { //user click on remove text
            e.preventDefault(); $(this).parent('div').remove();
            dep_count--;

        })

        $(wrapper).on("click", ".remove_strat", function (e) { //user click on remove text

            deleteStrat(this);

        })

        $(wrapper).on("click", ".remove_bus", function (e) { //user click on remove text

            deleteBus(this);

        })

        $(wrapper).on("click", ".remove_proj", function (e) { //user click on remove text

            deleteProj(this);

        })

        //Selection addition Functions

        $(add_Select).on("click", function (e) { //on add input button click
            e.preventDefault();

            //Grab the list of project name string
            nselect_arr = PageMethods.GetProjectDependency(button_id,map_name);
            select_total = nselect_arr.length;

            //Create the options list
            for (options_x = 0; options_x < select_total; options_x++) {
                options += "<option value='" + nselect_arr[options_x].val + ">" + nselect_arr[options_x].val + "</option>";
            }

            //Add a selection
            if (select_count < max_fields) { //max input box allowed
                select_count++; //text box increment
                $(dep_Select).append("<div class='new_sel'><select name='select_input'>"+options+"</select><a href='#' class='remove_field'>X</a></div>"); //add input box
            }
        });

        $(dep_Select).on("click", ".remove_field", function (e) { //user click on remove text
            e.preventDefault(); $(this).parent('div').remove();
            select_count--;
        })


        //Links Addition Functions
        $(add_Link).on("click", function (e) { //on add input button click
            e.preventDefault();
            if (link_count < max_fields) { //max input box allowed
                link_count++; //text box increment
                $(link_Text).append("<div class='new_link'><input type='text' size=60 name='link_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
            }
        });

        $(link_Text).on("click", ".remove_field", function (e) { //user click on remove text
            e.preventDefault(); $(this).parent('div').remove();
            link_count--;
        })


        $(save).click(function (e) {
                //Taking the value of the description
                var description_val = document.getElementById("descText").value.toString();
                PageMethods.SetProjectDescription(button_id, map_Name, description_val);


                //Taking the string dependecy- in a list of string??
                $('input[name=dep_input]').each(function () {
                    if ($(this).val() != null) {
                        dep_arr.push($(this).val());
                    }
                });
                PageMethods.SetProjectStrDependency(button_id, map_Name,dep_arr);


                //Select Dependecy value
                $('input[name=select_input]').each(function () {
                    if ($(this).val() != null) {
                        select_arr.push($(this).text());
                    }
                });
                PageMethods.SetProjectDependency(button_id, map_Name,select_arr);                                                                                                        

                //Taking the value of the risks
                var risk_val = document.getElementById("riskText").value.toString();
                PageMethods.SetProjectDescription(button_id, map_Name, risk_val);

                //Select Dependecy value
                $('input[name=link_input]').each(function () {
                    if ($(this).val() != null) {
                        link_arr.push($(this).val());
                    }
                });
                PageMethods.SetProjectLink(button_id, map_Name, link_arr);

            alert("Here is the button id:" + button_id + "Here is the roadmap name" + map_Name);

        });

        ////Inserting boxes when reloading the modal depending on the project it will be connected to 
        $('#inputModal').on('show.bs.modal', function (e) {
           // var load_dep_count = 0; // loading dep counter
           // var load_select_count = 0; // loading select counter
           // var load_link_count = 0; // loading link counter
           // var idep_arr = []; // dependecy string array
           // var iselect_arr = []; // dependecy select array
           // var ilink_arr = []; // link string array
           // var dep_x, select_x, link_x, options_x;// used in respective for loops
           // var dep_val, select_val, link_val;
           // var dep_total = 0, select_total = 0, link_total = 0; //used as list lengths

           // var title_Value = ""
           // var desc_Value = "";
           // var risk_Value = "";


           //// Add Modal Title
           // var data = '{"ProjectID":' + button_id + ',"RoadmapName":' + map_Name + '}';
           // var pr = { "ProjectID": button_id, "RoadmapName": map_Name };
           // $.ajax({
           //     type: "GET",
           //     async: false,
           //     url: "Roadmap.aspx/GetProjectName",

           //     data: pr,
           //     contentType: "application/json; charset=utf-8",
           //     dataType: "json",
           //     success: function (response) {
           //         $('#input_title').html(response);
           //     },
           //     error: function (xhr) {
           //         console.log("Nothing")
           //     }
           // });

           // //HERE IS HOW I TRIED IT BEFORE
           //     title_Value = PageMethods.GetDescription(button_id, map_Name);
           //     $('#input_title').html(title_Value);
           //     data: JSON.stringify({ProjectID: button_id, RoadmapName: map_Name}),
           //     contentType: "application/json; charset=utf-8",
           //     dataType: "json",
           //     success: function (title_Value) {
           //         $('#input_title').html(title_Value)
           //     },
           //     error: function (xhr) {
           //         console.log("Nothing")
           //     }
           // });

           //     title_Value = PageMethods.GetProjectName(button_id, map_Name);
           //     ;


           //     //adding the text to the description
           //     desc_Value = PageMethods.GetProjectDescription(button_id,map_Name);
           //     $('#descText').val(desc_Value);

           //     //Find the number of input boxes to load
           //     idep_arr = PageMethods.GetProjectDependencyText();
           //     dep_total = idep_arr.length;

           //     //Add all of thre input boxes
           //     for (dep_x = 0; dep_x < dep_total; dep_x++) {
           //         $(dep_Text).append("<div class='new_dep'><input type='text' size=55 name='dep_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
           //     };

           //     //fill the inputboxes with their values
           //     $('input[name=dep_input]').each(function () {
           //         $(this).val(idep_arr[load_dep_count]);
           //         load_dep_count++;
           //     });

           //     //Grab the list of project name string
           //     iselect_arr = PageMethods.GetProjectDependency(button_id,map_Name);
           //     select_total = nselect_arr.length;
           //     //Create the options list
           //     for (options_x = 0; options_x < select_total; options_x++) {
           //         options += "<option value='" + iselect_arr[options_x].val + ">" + iselect_arr[options_x].val + "</option>";
           //     };

           //     //Insert the correct number of selects
           //     for (select_x = 0; select_x < select_total; select_x++) {
           //         $(dep_Select).append("<div class='new_sel'><select name='select_input'>"+options+"</select><a href='#' class='remove_field'>X</a></div>");
           //     };

           //     //fill the inputboxes with their values
           //     $('input[name=select_input]').each(function () {
           //         $(this).val(iselect_arr[load_select_count]);
           //         load_select_count++;
           //     });

           //     //Fill in the Risks text area
           //     risk_Value = PageMethods.GetProjectRisk(button_id,map_Name);
           //     $('#riskText').val(risk_Value);

           //     //Get link list size
           //     ilink_arr = PageMethods.GetProjectLinks(button_id, map_Name);
           //     link_total = ilink_arr.length;

           //     //Add all of thre input boxes
           //     for (link_x = 0; link_x < link_total; link_x++) {
           //         $(link_Text).append("<div class='new_link'><input type='text' size=60 name='link_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
           //     };

           //     //fill the inputboxes with their values
           //     $('input[name=link_input]').each(function () {
           //         $(this).val(ilink_arr[load_link_count]);
           //         load_dep_count++;
           //     });


        });

        //When the modal hides again it will erase all divs and have the modal shell
        $('#inputModal').on('hidden.bs.modal', function (e) {
            //remove text from description
            $('#descText').val('');

            //removing all of the link lines
            $('.new_dep').each(function () {
                $(this).remove();
            });

            //removing all of the link lines
            $('.new_sel').each(function () {
                $(this).remove();
            });

            //remove risk text
            $('#riskText').val('');

            //removing all of the link lines
            $('.new_link').each(function () {
                $(this).remove();
            });
        });


        //Display Modal Show
        $('#displayModal').on('show.bs.modal', function (e) {
            var disdep_arr = [];
            var seldep_arr = [];
            var dislink_arr = [];
            var distitle_Value = '';
            var disdesc_Value = '';
            var disdep_total, dissel_total, dislink_total;
            var disdep_x, dissel_x,dislink_x;
            var disrisk_Value = '';


            //Add Modal Title
            //title_Value = PageMethods.GetProjectName(button_id, map_Name);
            //$('#display_title').html(title_Value);

            ////Add description
            //disdesc_Value = PageMethods.GetProjectDescription(button_id, map_Name);
            //$('#disdescText').val(disdesc_Value);

            ////Find the number of paragrpahs to display
            //disdep_arr = PageMethods.GetProjectDependencyText(button_id,map_Name);
            //disdep_total = disdep_arr.length;

            ////Add all of the paragrpahs
            //for (disdep_x = 0; disdep_x < disdep_total; disdep_x++) {
            //    $('.disdepText').append("<div class='dep_p'><p>"+disdep_arr[disdep_x]+"</p></div>"); //add input box
            //};


            ////Find the number paragraphs to load for select
            //dissel_arr = PageMethods.GetProjectDependency(button_id,map_Name);
            //dissel_total = dissel_arr.length;

            ////Add all of the paragrpahs for select
            //for (dissel_x = 0; dissel_x < dissel_total; dissel_x++) {
            //    $('.disdepSelect').append("<div class='sel_p'><p>" + dissel_arr[dissel_x] + "</p></div>"); //add input box
            //};

            ////Add description
            //disrisk_Value = PageMethods.GetProjectRisk(button_id, map_Name);
            //$('#disriskText').val(disrisk_Value);

            ////Find the number paragraphs to load for select
            //dislink_arr = PageMethods.GetProjectLinks(button_id, map_Name);
            //dislink_total = dislink_arr.length;

            ////Add all of the paragrpahs for links, this is when it does NOT HAVE QUOTATIONS IN THE STRING
            //for (dislink_x = 0; dislink_x < dislink_total; dislink_x++) {
            //    $('.dislinkText').append("<div class='link_p'><a href=\"" + dislink_arr[dislink_x] + "\">" + dislink_arr[dislink_x] + "</a></div>"); //add input box
            //};

        });

        //diplay modal hidden
        $('#displayModal').on('hidden.bs.modal', function (e) {
            //remove text from description
            $('#disdescText').val('');

            //remove dependecy texts
            $('.disdepText').each(function () {
                $(this).remove();
            });

            //remove selects text
            $('.disdepSelect').each(function () {
                $(this).remove();
            });

            //remove risk text
            $('#disriskText').val('');

            //remove links
            $('.dislinkText').each(function () {
                $(this).remove();
            });


        });



    });
    </script>





<!-- Data Input -->
        <style>
        .txtStrat {
            width: 220px;
            height: 30px;
            margin-left: 0px;
        }

        .btnDelete {
            height: 32px;
            width: 28px;
        }
        .txtBus {
            width: 200px;
            height: 30px;
            margin-left: 20px;
        }

        .txtProj {
            width: 210px;
            height: 30px;
            margin-left: 40px;
        }
        .txtProjDel {
            width: 180px;
            height: 30px;
            margin-left: 40px;
        }
        .btnStrat{
            height:200px;
            width:200px;
        }   
        .proj1 {
            position: relative;
              top: -50%;             
        }
        .proj2 {
            position: relative;
              top: 0%; 
              left: 150px;             
        }
        .proj3 {
            position: relative;
              top: 50%;           
              left: 300px;  
        }

        
   
    </style>

    <!-- /#Data Input-->
   
    </form>
</body>

</html>