/**
Created by Eric Nartker
Contains input modal Onclick
**/
var FullScreen = false;
var button_id;
function showModal(id) {
    button_id = id.substr(0, id.length - 3)
    if (FullScreen == false) {
        $("#inputModal").modal("show");
    } else if (FullScreen == true) {
        $("#displayModal").modal("show");
    }
}


$("#menu-toggle").click(function (e) {
    e.preventDefault();
    console.log(FullScreen);
    if (FullScreen == false) {
        // the sidebar is closed and we are in presentation mode (Display)
        document.getElementById("sidebar-wrapper").setAttribute("Present", "true");                    
        
    } else if (FullScreen == true) {
        // the side bar is open and we are in edit mode
        
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

        $(".proj1").css("cursor", "auto");
        $(".proj2").css("cursor", "auto");
        $(".proj3").css("cursor", "auto");
        $(".timeline").css("cursor", "auto");
        
        if (document.getElementById("v2")) {
            $(".RowVis2").hide();
            $(".stratLabel").hide();
            $(".NewCellVis2").hide();
            $(".stratLabelFiller").show();
            $(".proj1").hide();
        }
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

        $(".proj1").draggable("enable");
        $(".proj1").resizable("enable");
        $(".proj2").draggable("enable");
        $(".proj2").resizable("enable");
        $(".proj3").draggable("enable");
        $(".proj3").resizable("enable");
        $(".timeline").draggable("enable");



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
    var total_dep_count = 0; // total dep counter
    var total_select_count = 0; // total select counter
    var total_link_count = 0; // total link counter
   
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

    //Inserting boxes when reloading the modal depending on the project it will be connected to 
    $('#inputModal').on('show.bs.modal', function (e) {
        var idep_arr = []; // dependecy string array
        var iselect_arr = []; // dependecy select array
        var ilink_arr = []; // link string array
        var ioption_arr = [] // array for all of the project names options
        var dep_x, select_x, link_x, options_x;// used in respective for loops
        var dep_val, select_val, link_val;
        var dep_total = 0, select_total = 0, link_total = 0, option_total = 0; //used as list lengths
        var load_dep_count = 0; // loading dep counter
        var load_select_count = 0; // loading select counter
        var load_link_count = 0; // loading link counter

        var select_Value = ""
        var desc_Value = "";
        var risk_Value = "";
        var color = ["#0EA4B5", "#00274c", "#663399", "#ad235e", "#FF4500", "#bb0000"];


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
                 headercolor(button_id);

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
                $(dep_Text).append("<div class='new_dep'><input type='text' name='dep_input' class='iptext'/><a href='#' class='remove_field'>X</a></div>"); //add input box
            }
            //fill the inputboxes with their values
            $('input[name=dep_input]').each(function () {
                $(this).val(dep_array[load_dep_count]);
                load_dep_count++;
            });
            total_dep_count = load_dep_count;
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
            total_select_count = load_select_count;
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
                $(link_Text).append("<div class='new_link'><input type='text' name='link_input' class='iptext'/><a href='#' class='remove_field'>X</a></div>"); //add input box
            };

            //fill the inputboxes with their values
            $('input[name=link_input]').each(function () {
                $(this).val(link_array[load_link_count]);
                load_link_count++;
            });
            total_link_count = load_link_count;
        }
        function headercolor(id) {
                var temp = id.split("StratBox");
                temp = temp[1].split("BusBox")[0]
                var c = color[temp % 6];

                $("#headcolor1").css('background-color', c);
                $("#save").css('background-color', c);
            

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
                $('.disdescText').append("<div class='added_desc'><p>" + response.d[0][0] + "</p></div>");
                $('.disriskText').append("<div class='added_risk'><p>" + response.d[1][0] + "</p></div>");
                //Getting Dep String array   
                disdep_arr = response.d[3];
                disdep_total = disdep_arr.length;
                for (disdep_x = 0; disdep_x < disdep_total; disdep_x++) {
                    $('.disdepText').append("<div class='added_depstring'><p>" + disdep_arr[disdep_x] + "</p></div>");
                }
                //Getting Select Array
                disselect_arr = response.d[4];
                disselect_total = disselect_arr.length;
                
                for (dissel_x = 0; dissel_x < disselect_total; dissel_x++) {
                    $('.disdepSelect').append("<div class='added_select'><p>" + disselect_arr[dissel_x] + "</p></div>");
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
                headercolor2(button_id);
            },
            error: function (xhr) {
                console.log("error");
            },
        });

        function headercolor2(id) {
            var temp = id.split("StratBox");
            temp = temp[1].split("BusBox")[0]
            var c = color[temp % 6];

            $("#headcolor2").css('background-color', c);
            $("#close1").css('background-color', c);


        }
    });

    //diplay modal hidden
    $('#displayModal').on('hidden.bs.modal', function (e) {
        //remove text from description
        $('.added_desc').each(function () {
            $(this).remove();
        });

        //remove dependecy texts
        $('.added_depstring').each(function () {
            $(this).remove();
        });

        //remove selects text
        $('.added_select').each(function () {
            $(this).remove();
        });

        //remove risk text
        $('.added_risk').each(function () {
            $(this).remove();
        });

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

    //Dependency addition Functions
    $(add_Text).on("click", function (e) { //on add input button click
        e.preventDefault();
        if (total_dep_count < max_fields) { //max input box allowed
            $(dep_Text).append("<div class='new_dep'><input type='text' name='dep_input' class='iptext'/><a href='#' class='remove_field'>X</a></div>");
            total_dep_count++; //text box increment//add input box
        }
    });

    $(dep_Text).on("click", ".remove_field", function (e) { //user click on remove text
        e.preventDefault(); $(this).parent('div').remove();
        total_dep_count--;

    })

    //Selection addition Functions
    $(add_Select).on("click", function (e) { //on add input button click
        e.preventDefault();

        //Add a selection
        if (total_select_count < max_fields) { //max input box allowed
            var add_sel = "<div class='new_sel'><select id='select_input" + total_select_count + "'>" + load_options + "</select>" + "<a href='#' class='remove_field'>X</a></div>"
            $(dep_Select).append(add_sel); //add input box
            total_select_count++;
        }
    });
    //Removing the selection dependency box
    $(dep_Select).on("click", ".remove_field", function (e) { //user click on remove text
        e.preventDefault(); $(this).parent('div').remove();
        total_select_count--;
    })


    //Links Addition Functions
    $(add_Link).on("click", function (e) { //on add input button click
        e.preventDefault();
        if (total_link_count < max_fields) { //max input box allowed

            $(link_Text).append("<div class='new_link'><input type='text'name='link_input' class='iptext'/><a href='#' class='remove_field'>X</a></div>"); //add input box
            total_link_count++; //text box increment
        }
    });
    //Removing the link input box
    $(link_Text).on("click", ".remove_field", function (e) { //user click on remove text
        e.preventDefault(); $(this).parent('div').remove();
        total_link_count--;
    })

    //Running the Save on all pieces

    $(save).click(function (e) {
        //Taking the value of the description
        var description_val = document.getElementById("descText").value.toString();
        var risk_val = document.getElementById("riskText").value.toString();
        //Taking the string dependecy- in a list of string??
        $('input[name=dep_input]').each(function () {
            if ($(this).val() != null) {
                ndep_arr.push($(this).val());
            }
        });
        $('input[name=link_input]').each(function () {
            if ($(this).val() != null) {
                nlink_arr.push($(this).val());
            }
        });
        for (select_x = 0; select_x < total_select_count; select_x++) {
            select_Value = "#select_input" + select_x;
            if ($(select_Value).val() != "No Project") {
                if (nselect_arr.indexOf($(select_Value).val()) == -1) {
                    nselect_arr.push($(select_Value).val());
                }
            }
        }
        //Need Ajax Post Call here?
        PageMethods.SetAll(button_id, map_Name, nselect_arr, nlink_arr, ndep_arr, description_val, risk_val);

        total_dep_count = 0;
        total_select_count = 0;
        total_link_count = 0;
        ndep_arr = [];
        nselect_arr = [];
        nlink_arr = [];

        $('#inputModal').modal('hide');



    });
});



