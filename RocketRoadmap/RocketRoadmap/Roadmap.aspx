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
<asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>



                                                
<div id="wrapper">

    <!-- Sidebar -->
    <div id="sidebar-wrapper">

        <!-- Data Input -->
        <div id="mainDiv">
           <table id ="sidebarTable" runat ="server">
            <tr id="StratBox0Row">
             <td>
               
                 <input id='StratBox0' class ='txtStrat' BusTotal="1"  type ='text' placeholder='Add Strategy Point' runat='server'  onkeyup ='addStrat(event,this,1)'/><a href="#" id='StratDelete0' style="color:white; font-size:20px; vertical-align:-3px;" class="remove_strat"> X</a><br/>
                    <table id ="StratBox0Table" runat='server' >
                        <tr id="StratBox0BusBox0Row">
                         <td>
                           <input id ='StratBox0BusBox0'  class='txtBus'  ProjTotal="1"  type ='text' placeholder='Add Business Value' runat='server' onkeyup ='addBus(event, this,1)' /><button class = 'btnDelete' type='button' id='StratBox0BusBox0Delete' onclick='deleteBus(event, this)'>X</button>
                                    <div id="projDiv" visible="false" >
                                         <input id ='StratBox0BusBox0ProjBox0' class ='txtProj' type='text'placeholder ='Add Project'   onkeyup ='addProj(event, this,1)' />
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
                    <button type="button" class="btn" id="test" data-toggle="modal" onclick="showModal(this.id)">Test Modal</button>

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

            <h3 class="modal-title" id="m_title"></h3>

        </div>
            <div class="modal-body">

                <!-- Description Text Box -->
                <h2><u>Description</u></h2>
                    <textarea id="descText" rows="4" cols="75"></textarea> 
                    <p></p>

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
                    <p></p>

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
<<<<<<< HEAD
    function showModal( ) {                        
=======
    function showModal() {                        
>>>>>>> d71d48ab238acb874f9263e88dd08fde9328fbd8
        $("#inputModal").modal("show");
    }
    var button_id;

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

        var roadmap_url = window.location.href;
        var mapName = url.substr(url.indexOf('?') + 1);
        mapName = mapName.substr(2, mapName.length);

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


        //Selection addition Functions

        $(add_Select).on("click", function (e) { //on add input button click
            e.preventDefault();

            //Grab the list of project name string
            //nselect_arr = PageMethods.GetProjectDependency();
            //select_total = nselect_arr.length;
            //Create the options list
            for (options_x = 0; options_x < select_total; options_x++) {
                options += "<option value='" + nselect_arr[options_x].val + ">" + nselect_arr[options_x].val + "</option>";
            }

<<<<<<< HEAD
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

=======
                if (select_count < max_fields) { //max input box allowed
                    select_count++; //text box increment
                    $(dep_Select).append("<div> <select id='depenSelect'"+select_count+">" + options + "</select><a href='#' class='remove_field'>X</a></div>"); //add input box
                }
            });

            $(dep_Select).on("click", ".remove_field", function (e) { //user click on remove text
                e.preventDefault(); $(this).parent('div').remove();
                select_count--;
            })
>>>>>>> d71d48ab238acb874f9263e88dd08fde9328fbd8

        $(save).click(function (e) {
            //Taking the value of the description
            //var description_val = document.getElementById("descText").value.toString();
            //PageMethods.SetProjectDescription("Need project name", "need roadmap name", description_val);

<<<<<<< HEAD
            //Taking the string dependecy- in a list of string??
            $('input[name=dep_input]').each(function () {
                if ($(this).val() != null) {
                    //test_count++;
                    dep_arr.push($(this).val());
=======
            //Links Addition Functions
            $(add_Link).click(function (e) { //on add input button click
                e.preventDefault();
                if (link_count < max_fields) { //max input box allowed
                    link_count++; //text box increment
                    $(link_Text).append("<div><input type='text' size=60 id='linkText'"+link_count+"/><a href='#' class='remove_field'>X</a></div>"); //add input box
>>>>>>> d71d48ab238acb874f9263e88dd08fde9328fbd8
                }

            });

<<<<<<< HEAD
            //PageMethods.SetProjectStrDependency("Need project name", "need roadmap name",dep_arr[]);
=======
            $(wrapper).on("click", ".remove_field", function (e) { //user click on remove text
                e.preventDefault(); $(this).parent('div').remove();
                link_count--;
            })
>>>>>>> d71d48ab238acb874f9263e88dd08fde9328fbd8

            //Select Dependecy value
            $('input[name=select_input]').each(function () {
                if ($(this).val() != null) {
                    //test_count++;
                    select_arr.push($(this).text());
                }
            });

            //PageMethods.SetProjectDependency("Need project name", "need roadmap name",select_arr[]);                                                                                                        

            //Taking the value of the risks
            // var description_val = document.getElementById("riskText").value.toString();
            //PageMethods.SetProjectDescription("Need project name", "need roadmap name", description_val);

            //Select Dependecy value
            $('input[name=link_input]').each(function () {
                if ($(this).val() != null) {
                    //test_count++;
                    link_arr.push($(this).val());
                }
            });

            alert("This is the count total " + test_count);

        })

        ////Inserting boxes when reloading the modal depending on the project it will be connected to 
        $('#inputModal').on('show.bs.modal', function (e) {
            //    var load_dep_count = 0; // loading dep counter
            //    var load_select_count = 0; // loading select counter
            //    var load_link_count = 0; // loading link counter
            //    var ndep_arr = []; // dependecy string array
            //    var nselect_arr = []; // dependecy select array
            //    var nlink_arr = []; // link string array
            //    var dep_x, select_x, link_x, options_x;// used in respective for loops
            //    var dep_val, select_val, link_val;
            //    var dep_total = 0, select_total = 0, link_total = 0; //used as list lengths

            //    var title_Value = "Awesome Modal"
            //    var desc_Value = "this is a test showing that we can insert the descrition";
            //    var risk_Value = "this is a test showing that we can insert the descrition";



            //    //Add Modal Title
            //    $('#m_title').html(title_Value);

            //    //adding the text to the description
            //    //desc_Value = PageMethods.GetProjectDescription(,);
            //    $('#descText').val(desc_Value);

            //    //Find the number of input boxes to load
            //    //ndep_arr = PageMethods.GetProjectDependencyText();
            //    //dep_total = ndep_arr.length;

            //    //Add all of thre input boxes
            //    for (dep_x = 0; dep_x < dep_total; dep_x++) {
            //        $(dep_Text).append("<div class='new_dep'><input type='text' size=55 name='dep_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
            //    };

            //    //fill the inputboxes with their values
            //    $('input[name=dep_input]').each(function () {
            //        $(this).val(ndep_arr[load_dep_count]);
            //        load_dep_count++;
            //    });

            //    //Grab the list of project name string
            //    //nselect_arr = PageMethods.GetProjectDependency();
            //    //select_total = nselect_arr.length;
            //    //Create the options list
            //    for (options_x = 0; options_x < select_total; options_x++) {
            //        options += "<option value='" + nselect_arr[options_x].val + ">" + nselect_arr[options_x].val + "</option>";
            //    };

            //    //Insert the correct number of selects
            //    for (select_x = 0; select_x < select_total; select_x++) {
            //        //$(dep_Select).append("<div class='new_sel'><select name='select_input'>"+options+"</select><a href='#' class='remove_field'>X</a></div>");
            //    };

            //    //fill the inputboxes with their values
            //    $('input[name=select_input]').each(function () {
            //        $(this).val(nselect_arr[load_select_count]);
            //        load_select_count++;
            //    });

            //    //Fill in the Risks text area
            //    //risk_Value = PageMethods.GetProjectDescription(,);
            //    $('#riskText').val(risk_Value);

            //    //Get link list size
            //    //nlink_arr = PageMethods.GetProjectDependencyText();
            //    //link_total = link_arr.length;

            //    //Add all of thre input boxes
            //    for (link_x = 0; link_x < link_total; link_x++) {
            //        $(link_Text).append("<div class='new_link'><input type='text' size=60 name='link_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
            //    };

            //    //fill the inputboxes with their values
            //    $('input[name=link_input]').each(function () {
            //        $(this).val(nlink_arr[load_link_count]);
            //        load_dep_count++;
            //    });


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

<<<<<<< HEAD
            //remove risk text
            $('#riskText').val('');

            //removing all of the link lines
            $('.new_link').each(function () {
                $(this).remove();
            });
        });
=======
            ////Inserting boxes when reloading the modal depending on the project it will be connected to 
            $('#inputModal').on('show.bs.modal', function (e) {
                
           //    var i, max = 5;
           //    for (i = 1; i < max; i++) {
           //         $(dep_Text).append('<div><input type="text" size=60/><a href="#" class="remove_field">X</a></div>'); //add input box
           //     }

           //     for (i = 2; i < max; i++) {
           //          $(dep_Select).append('<div> <select id="depenSelect"></select><a href="#" class="remove_field">X</a></div>'); //add input box
           //     }
            })
>>>>>>> d71d48ab238acb874f9263e88dd08fde9328fbd8


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