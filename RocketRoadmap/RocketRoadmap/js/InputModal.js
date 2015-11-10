/**
Created by Eric Nartker
Contains input modal Onclick
**/


var button_id;
function showModal(id) {
    button_id = id.substr(0, id.length - 3)
    if (panel_close == false) {
        $("#inputModal").modal("show");
    } else if (panel_close == true) {
        $("#displayModal").modal("show");
    }
}
var FullScreen = false;
var panel_close = false;
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    if (panel_close == false) {
        // the sidebar is closed and we are in presentation mode (Display)
        document.getElementById("sidebar-wrapper").setAttribute("Present", "true");                     //how to get that bool
                                                                                                        // document.getElementById("sidebar-wrapper").GetAttribute("Present);
        panel_close = true;
    } else if (panel_close == true) {
        // the side bar is open and we are in edit mode
        panel_close = false;
        document.getElementById("sidebar-wrapper").setAttribute("Present", "false");
    }
        
    $("#wrapper").toggleClass("toggled");
    if (!FullScreen) {
        //disable editing
        FullScreen = true;
        $(".proj1").draggable("disable");
        $(".proj1").resizable("disable");
        $(".proj2").draggable("disable");
        $(".proj2").resizable("disable");
        $(".proj3").draggable("disable");
        $(".proj3").resizable("disable");
        $(".timeline").draggable("disable");
        $(".proj1").bind("mouseover");
        $(".proj1").bind("mouseout");
        //$(".proj2").bind("mouseover");
        //$(".proj2").bind("mouseout");
        //$(".proj3").bind("mouseover");
        //$(".proj3").bind("mouseout");

        $(".proj1").css("cursor", "auto");
        $(".proj2").css("cursor", "auto");
        $(".proj3").css("cursor", "auto");
        $(".timeline").css("cursor", "auto");
    }
    else {
        FullScreen = false;
        $(".proj1").draggable({ axis: "x" });
        $(".proj1").resizable({ handles: 'e, w' });
        $(".proj2").draggable({ axis: "x" });
        $(".proj2").resizable({ handles: 'e, w' });
        $(".proj3").draggable({ axis: "x" });
        $(".proj3").resizable({ handles: 'e, w' });
        $(".timeline").draggable({ axis: "x", containment: "#containment-wrapper", });
        $(".proj1").unbind("mouseover");
        $(".proj1").unbind("mouseout");
        //$(".proj2").unbind("mouseover");
        //$(".proj2").unbind("mouseout");
        //$(".proj3").unbind("mouseover");
        //$(".proj3").unbind("mouseout");

        $(".proj1").css("cursor", "e-resize");
        $(".proj2").css("cursor", "e-resize");
        $(".proj3").css("cursor", "e-resize");
        $(".timeline").css("cursor", "e-resize");
    }


});




$(document).ready(function () {
    var max_fields = 10; //maximum input boxes allowed
    var max_select; // maximum number of selects allowed
    var dep_Text = $(".depText"); //Dependency input wrapper
    var add_Text = $("#addText"); //Add dependency input
    var dep_Select = $(".depSelect"); //Dependency Selection wrapper
    var add_Select = $("#addSelect"); //Add dependency select
    var link_Text = $(".linkText"); //Link input wrapper
    var add_Link = $("#addLink"); //Add Link input
    var save = $("#save"); //Save button
    var load_dep_count = 0; // loading dep counter
    var load_select_count = 0; // loading select counter
    var load_link_count = 0; // loading link counter
    var options = ""; // used for the string of options a select has
    var load_options = "";
    var select_total = 0;
    var all_proj = [];
    var ndep_arr=[];
    var nselect_arr=[];
    var nlink_arr=[];

    //Getting roadmap name
    var roadmap_url = window.location.href;
    var map_Name = roadmap_url.substr(roadmap_url.indexOf('?') + 1);
    map_Name = decodeURIComponent(map_Name.substr(2, map_Name.length));

    //Dependency addition Functions
    $(add_Text).on("click", function (e) { //on add input button click
        e.preventDefault();
        if (load_dep_count < max_fields) { //max input box allowed
            load_dep_count++; //text box increment
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
        
        //Add a selection
        if (load_select_count < max_fields ) { //max input box allowed
            var add_sel = "<div class='new_sel'><select id='select_input" + load_select_count + "'>" + load_options + "</select>" + "<a href='#' class='remove_field'>X</a></div>"
            $(dep_Select).append(add_sel); //add input box
            load_select_count++;
        }
    });
    //Removing the selection dependency box
    $(dep_Select).on("click", ".remove_field", function (e) { //user click on remove text
        e.preventDefault(); $(this).parent('div').remove();
        load_select_count--;
    })


    //Links Addition Functions
    $(add_Link).on("click", function (e) { //on add input button click
        e.preventDefault();
        if (load_link_count < max_fields) { //max input box allowed
            
            $(link_Text).append("<div class='new_link'><input type='text' size=60 name='link_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
            load_link_count++; //text box increment
        }
    });
    //Removing the link input box
    $(link_Text).on("click", ".remove_field", function (e) { //user click on remove text
        e.preventDefault(); $(this).parent('div').remove();
        load_link_count--;
    })

    //Running the Save on all pieces

    $(save).click(function (e) {
        //Taking the value of the description
        var description_val = document.getElementById("descText").value.toString();
        PageMethods.SetProjectDescription(button_id, map_Name, description_val);
        var risk_val = document.getElementById("riskText").value.toString();
        PageMethods.SetProjectRisk(button_id, map_Name, risk_val);


        //Taking the string dependecy- in a list of string??
        $('input[name=dep_input]').each(function () {
            if ($(this).val() != null) {
                ndep_arr.push($(this).val());
            }
        });
        //Need Ajax Post Call here?
        PageMethods.SetProjectStrDependency(button_id, map_Name, ndep_arr);
        //Select Dependecy value
        $('input[name=link_input]').each(function () {
            if ($(this).val() != null) {
                nlink_arr.push($(this).val());
            }
        });
        //Need Ajax Call here
        PageMethods.SetProjectLink(button_id, map_Name, nlink_arr);
        


        //Select Dependecy value
        for (select_x = 0; select_x < load_select_count; select_x++) {
            select_Value = "#select_input" + select_x;
            if ( $(select_Value).val() != "No Project") {
                nselect_arr.push($(select_Value).val());
            }
        }
        //Need Ajax Call here 
        PageMethods.SetProjectDependency(button_id, map_Name, nselect_arr);
        

        //Taking the value of the risks



        ndep_arr = [];
        nselect_arr = [];
        nlink_arr = [];

        $('#inputModal').modal('hide');

      

    });

    //Inserting boxes when reloading the modal depending on the project it will be connected to 
    $('#inputModal').on('show.bs.modal', function (e) {
        var idep_arr = []; // dependecy string array
        var iselect_arr = []; // dependecy select array
        var ilink_arr = []; // link string array
        var ioption_arr = [] // array for all of the project names options
        var dep_x, select_x, link_x, options_x;// used in respective for loops
        var dep_val, select_val, link_val;
        var dep_total = 0, select_total = 0, link_total = 0, option_total = 0; //used as list lengths

        var select_Value = ""
        var desc_Value = "";
        var risk_Value = "";

        //FILLING THE TITLE
        var pr = { 'ProjectID': button_id, 'RoadmapName': map_Name };
        var br = { 'RoadmapName': map_Name };

         $.ajax({
             type: "POST",
             async: false,
             url: "Roadmap.aspx/GetAll",
             data: JSON.stringify(pr),
             contentType: "application/json; charset=utf-8",
             success: function (response) {
                 $('#input_title').html(response.d[2][0]);//fill in title
                 $('#descText').val(response.d[0][0]);//fill in description
                 $('#riskText').val(response.d[1][0]);//fill in risk
                 //Getting Dep String array   
                 idep_arr = response.d[3];
                 dep_total = idep_arr.length;
                 //Getting Select Array
                 iselect_arr = response.d[4];
                 select_total = iselect_arr.length;
                 //Geting Options
                 ioption_arr = response.d[6];
                 option_total = ioption_arr.length;
                 //Get Link Array
                 ilink_arr = response.d[5];
                 link_total = ilink_arr.length;

                 fill_dep(idep_arr);
                 fill_options(ioption_arr);
                 fill_select(iselect_arr); 
                 fill_link(ilink_arr);

                 idep_arr = [];
                 ioptions_arr = [];
                 iselect_arr = [];
                 ilink_arr = [];
             },
             error: function (xhr) {
                 console.log("error");
             },
         });


        function fill_dep(dep_array) {
            //Add all of the input boxes
            for (dep_x = 0; dep_x < dep_total; dep_x++) {
                $(dep_Text).append("<div class='new_dep'><input type='text' size=55 name='dep_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
            }
            //fill the inputboxes with their values
            $('input[name=dep_input]').each(function () {
                $(this).val(dep_array[load_dep_count]);
                load_dep_count++;
            });

        }

        function fill_options(option_array) {
            //Create the options list
            options += "<option value='No Project'>Please Select a Project </option>";
            for (var options_x = 0; options_x < option_array.length; options_x++) {
                options += "<option value='" + option_array[options_x] + "'>" + option_array[options_x] + "</option>";
            }
            load_options = options;
            //Insert the correct number of selects
            for (select_x = 0; select_x < select_total; select_x++) {
                $(dep_Select).append("<div class='new_sel'><select id='select_input" + load_select_count + "'>" + options + "</select><a href='#' class='remove_field'>X</a></div>");
                load_select_count++;
            };
            
        }
        
        function fill_select(select_array) {
            //fill the inputboxes with their values
            for (select_x = 0; select_x < select_total; select_x++)
            {
                select_Value = "#select_input" + select_x;
                $(select_Value).val(select_array[select_x]);
            }
        }
        function fill_link(link_array){
            for (link_x = 0; link_x < link_total; link_x++) {
                $(link_Text).append("<div class='new_link'><input type='text' size=60 name='link_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
            };

            //fill the inputboxes with their values
            $('input[name=link_input]').each(function () {
                $(this).val(link_array[load_link_count]);
                load_link_count++;
            });
        }
      
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
        options = "";
    });

    //Display Modal Show
    $('#displayModal').on('show.bs.modal', function (e) {
        var disdep_arr = [];
        var dissel_arr = [];
        var dislink_arr = [];
        var disdep_total, dissel_total, dislink_total, disoption_total;
        var disdep_x, dissel_x, dislink_x;
        var disrisk_Value = '';


        var disvalue = { 'ProjectID': button_id, 'RoadmapName': map_Name };
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetAll",
            data: JSON.stringify(disvalue),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $('#display_title').html(response.d[2][0]);
                $('#disdescText').val(response.d[0][0]);
                $('#disriskText').val(response.d[1][0]);
                //Getting Dep String array   
                disdep_arr = response.d[3];
                disdep_total = disdep_arr.length;
                for (disdep_x = 0; disdep_x < disdep_total; disdep_x++) {
                    $('.disdepText').append("<div class='added_depstring'><p>" + disdep_arr[disdep_x] + "</p></div>");
                }
                //Getting Select Array
                disselect_arr = response.d[4];
                disselect_total = disselect_arr.length;
                
                for (dissel_x = 0; dissel_x < dissel_total; dissel_x++) {
                    $('.disdepSelect').append("<div class='added_select'><p>" + dissel_arr[dissel_x] + "</p></div>");
                }
                //Get Link Array
                dislink_arr = response.d[5];
                dislink_total = dislink_arr.length;
                
                for (dislink_x = 0; dislink_x < dislink_total; dislink_x++) {
                    $('.dislinkText').append('<div class="added_link"><a href="' + dislink_arr[dislink_x] + '">' + dislink_arr[dislink_x] + ' </a></div>');
                }
                //Geting Options
                disoption_arr = response.d[6];
                disoption_total = disoption_arr.length;

                disdep_arr = [];
                dissel_arr = [];
                dislink_arr = [];
                disoption_arr = [];
            },
            error: function (xhr) {
                console.log("error");
            },
        });


    });

    //diplay modal hidden
    $('#displayModal').on('hidden.bs.modal', function (e) {
        //remove text from description
        $('#disdescText').val('');

        //remove dependecy texts
        $('.added_depstring').each(function () {
            $(this).remove();
        });

        //remove selects text
        $('.added_select').each(function () {
            $(this).remove();
        });

        //remove risk text
        $('#disriskText').val('');

        //remove links
        $('.added_link').each(function () {
            $(this).remove();
        });
    });

    //Delete the Strategy Point 
    $(wrapper).on("click", ".remove_strat", function (e) { //user click on remove text

        deleteStrat(this);

    });
    //Delete the Business Value
    $(wrapper).on("click", ".remove_bus", function (e) { //user click on remove text

        deleteBus(this);

    });
    //Delete the Project
    $(wrapper).on("click", ".remove_proj", function (e) { //user click on remove text

        deleteProj(this);

    });
});