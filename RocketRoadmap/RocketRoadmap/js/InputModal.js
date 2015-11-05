/**
Created by Eric Nartker
Contains input modal Onclick
**/
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
    button_id = id.substr(0, id.length - 3)
    if (panel_close == false) {
        $("#inputModal").modal("show");
    } else if (panel_close == true) {
        $("#displayModal").modal("show");
    }
}


$(document).ready(function () {
    var max_fields = 10; //maximum input boxes allowed
    var max_select; // maximum number oif selects allowed
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

    //Getting roadmap name
    var roadmap_url = window.location.href;
    var map_Name = roadmap_url.substr(roadmap_url.indexOf('?') + 1);
    map_Name = map_Name.substr(2, map_Name.length);


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
        var br = { 'RoadmapName': map_Name };
        var option_count = 0;
        var option_arr = [];
        
        //NEED AJAX CALL HERE TO GET THE LIST of project Names
      
        //Add a selection
        if (load_select_count < max_select ) { //max input box allowed
            load_select_count++; //text box increment
            var add_sel = "<div class='new_sel'><select name='select_input'>" + load_options + "</select>" + "<a href='#' class='remove_field'>X</a></div>"
            $(dep_Select).append(add_sel); //add input box
            load_options = "";
        }
    });
    //Removing the selection dependency box
    $(dep_Select).on("click", ".remove_field", function (e) { //user click on remove text
        e.preventDefault(); $(this).parent('div').remove();
        select_count--;
    })


    //Links Addition Functions
    $(add_Link).on("click", function (e) { //on add input button click
        e.preventDefault();
        if (load_link_count < max_fields) { //max input box allowed
            load_link_count++; //text box increment
            $(link_Text).append("<div class='new_link'><input type='text' size=60 name='link_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
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


        //Taking the string dependecy- in a list of string??
        $('input[name=dep_input]').each(function () {
            if ($(this).val() != null) {
                ndep_arr.push($(this).val());
            }
        });
        //Need Ajax Post Call here?
        PageMethods.SetProjectStrDependency(button_id, map_Name, ndep_arr);


        //Select Dependecy value
        $('select[name=select_input]').each(function () {
            if ($(this).val() != "No Project") {
                nselect_arr.push($(this).val());
            }
        });
        //Need Ajax Call here 
        PageMethods.SetProjectDependency(button_id, map_Name, nselect_arr);

        //Taking the value of the risks
        var risk_val = document.getElementById("riskText").value.toString();
        PageMethods.SetProjectRisk(button_id, map_Name, risk_val);

        //Select Dependecy value
        $('input[name=link_input]').each(function () {
            if ($(this).val() != null) {
                nlink_arr.push($(this).val());
            }
        });
        //Need Ajax Call here
        PageMethods.SetProjectLink(button_id, map_Name, nlink_arr);

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

        var title_Value = ""
        var desc_Value = "";
        var risk_Value = "";

        //FILLING THE TITLE
        var pr = { 'ProjectID': button_id, 'RoadmapName': map_Name };
        var br = { 'RoadmapName': map_Name };
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectName",
            data: JSON.stringify(pr),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $('#input_title').html(response.d);
            },
            error: function (xhr) {
                console.log("error");
            },
        });

        //FILLING THE DESCRIPTION
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectDescription",
            data: JSON.stringify(pr),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('#descText').val(response.d);
                console.log(response.d);
            },
            error: function (xhr) {
                console.log("error");
            },
        });

       //FILLING THE STRING DEPENDENCIES
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectDependencyText",
            data: JSON.stringify(pr),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {   
                idep_arr = response.d;
                dep_total = idep_arr.length;
                fill_dep(idep_arr);
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

        //FILLING THE SELECT DEPENDENCIES
        //Ajax call to get the array of project name strings 
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectDependency",
            data: JSON.stringify(pr),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {   
                iselect_arr = response.d;
                select_total = iselect_arr.length;
                
                fill_select(iselect_arr);
            },
            error: function (xhr) {
                console.log("error");
            },
        });

        function fill_options(option_array) {
            //Create the options list
            options += "<option value='No Project'>Please Select a Project </option>";
            for (var options_x = 0; options_x < option_array.length; options_x++) {
                options += "<option value='" + option_array[options_x] + "'>" + option_array[options_x] + "</option>";
            }
            load_options = options;
            //Insert the correct number of selects
            for (select_x = 0; select_x < select_total; select_x++) {
                $(dep_Select).append("<div class='new_sel'><select name='select_input'>" + options + "</select><a href='#' class='remove_field'>X</a></div>");
                
            };
        }
        
        function fill_select(select_array) {
            //Ajax call to get all of the project names 
            $.ajax({
                type: "POST",
                async: false,
                url: "Roadmap.aspx/GetAllRoadmapProjectDesc",
                data: JSON.stringify(pr),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    ioption_arr = response.d;
                    option_total = ioption_arr.length;
                    fill_options(ioption_arr);

                },
                error: function (xhr) {
                    console.log("error");
                },
            });
            
            //fill the inputboxes with their values
            $('input[name=select_input]').each(function () {
                $(this).val(select_array[load_select_count]);
                load_select_count++;
            });
        }
      

        //Fill in the Risks text area
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectRisk",
            data: JSON.stringify(pr),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('#riskText').val(response.d);
            },
            error: function (xhr) {
                console.log("error");
            },
        });

        //FILLING THE LINKS
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectLinksString",
            data: JSON.stringify(pr),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                ilink_arr = response.d;
                link_total = ilink_arr.length;          
                fill_link(ilink_arr);
            },
            error: function (xhr) {
                console.log("error");
            },
        });
        

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
        var distitle_Value = '';
        var disdesc_Value = '';
        var disdep_total, dissel_total, dislink_total;
        var disdep_x, dissel_x, dislink_x;
        var disrisk_Value = '';


        //Add Modal Title
        var disvalue = { 'ProjectID': button_id, 'RoadmapName': map_Name };
        var getvalue = { 'RoadmapName': map_Name };
        var pr = { 'ProjectID': button_id, 'RoadmapName': map_Name };
        var br = { 'RoadmapName': map_Name };
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectName",
            data: JSON.stringify(disvalue),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $('#display_title').html(response.d);
            },
            error: function (xhr) {
                console.log("error");
            },
        });

        //FILLING THE DESCRIPTION
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectDescription",
            data: JSON.stringify(disvalue),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('#disdescText').val(response.d);
                console.log(response.d);
            },
            error: function (xhr) {
                console.log("error");
            },
        });

        //FILLING THE STRING DEPENDENCIES 
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectDependencyText",
            data: JSON.stringify(pr),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                disdep_arr = response.d;
                disdep_total = disdep_arr.length;
                for (disdep_x = 0; disdep_x < disdep_total; disdep_x++) {
                    $('.disdepText').append("<div class='added_depstring'><p>" + disdep_arr[disdep_x] + "</p></div>");
                }
            },
            error: function (xhr) {
                console.log("error");
            },
        });


        //FILLING THE SELECT DEPENDENCIES
        //Ajax call to get the array of project name strings 
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectDependency",
            data: JSON.stringify(pr),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                dissel_arr = response.d;
                dissel_total = dissel_arr.length;
                for (dissel_x = 0; dissel_x < dissel_total; dissel_x++) {
                    $('.disdepSelect').append("<div class='added_select'><p>" + dissel_arr[dissel_x] + "</p></div>");
                }
            },
            error: function (xhr) {
                console.log("error");
            },
        });


        //Fill in the Risks text area
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectRisk",
            data: JSON.stringify(pr),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('#disriskText').val(response.d);
            },
            error: function (xhr) {
                console.log("error");
            },
        });

        //FILLING THE LINKS
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectLinksString",
            data: JSON.stringify(pr),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                ilink_arr = response.d;
                link_total = ilink_arr.length;
                for (dislink_x = 0; dislink_x < dislink_total; dislink_x++) {
                    $('.dislinkText').append('<div class="added_link"><a href="' + dislink_arr[dislink_x] + '">' +dislink_arr[dislink_x]+ ' </a></div>');
                }
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