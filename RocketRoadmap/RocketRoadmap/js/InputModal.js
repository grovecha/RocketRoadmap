/**
Created by Eric Nartker
Contains input modal Onclick
**/
$(document).ready(function () {
    var max_fields = 10; //maximum input boxes allowed
    var dep_Text = $(".depText"); //Dependency input wrapper
    var add_Text = $("#addText"); //Add dependency input
    var ndep_arr = []; // dependecy string array
    var dep_Select = $(".depSelect"); //Dependency Selection wrapper
    var add_Select = $("#addSelect"); //Add dependency select
    var nselect_arr = []; //select array
    var link_Text = $(".linkText"); //Link input wrapper
    var add_Link = $("#addLink"); //Add Link input
    var nlink_arr = []; //link array
    var save = $("#save"); //Save button
    var dep_count = 0; //initlal dependency input counter
    var select_count = 0; //initial select input counter
    var link_count = 0; //initial link count
    var test_count = 0;
    var options; // used for the string of options a select has
    var select_total = 0;
    var all_proj = [];

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

    //Selection addition Functions

    $(add_Select).on("click", function (e) { //on add input button click
        e.preventDefault();

        //Getting all projects made
        //all_proj = PageMethods.GetAllRoadmapProjects(map_Name);
        //select_total = all_proj.length;

        //Grab the list of project name string
        //nselect_arr = PageMethods.GetProjectDependency(button_id, map_name);
        //select_total = nselect_arr.length;

        //Create the options list
        options += "<option value='No Project'>Please Select a Project </option>";
        //for (options_x = 0; options_x < select_total; options_x++) {
        //        options += "<option value='" + nselect_arr[options_x].val + ">" + nselect_arr[options_x].val + "</option>";
        //}
        
        //Add a selection
        if (select_count < max_fields) { //max input box allowed
            select_count++; //text box increment
            $(dep_Select).append("<div class='new_sel'><select name='select_input'>" + options + "</select><a href='#' class='remove_field'>X</a></div>"); //add input box
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
        if (link_count < max_fields) { //max input box allowed
            link_count++; //text box increment
            $(link_Text).append("<div class='new_link'><input type='text' size=60 name='link_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
        }
    });
    //Removing the link input box
    $(link_Text).on("click", ".remove_field", function (e) { //user click on remove text
        e.preventDefault(); $(this).parent('div').remove();
        link_count--;
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
        PageMethods.SetProjectStrDependency(button_id, map_Name, ndep_arr);


        //Select Dependecy value
        $('input[name=select_input]').each(function () {
            if ($(this).val() != null) {
                nselect_arr.push($(this).val());
            }
        });
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
        PageMethods.SetProjectLink(button_id, map_Name, nlink_arr);

        alert("Here is the button id:" + button_id + "Here is the roadmap name" + map_Name);

    });

    ////Inserting boxes when reloading the modal depending on the project it will be connected to 
    $('#inputModal').on('show.bs.modal', function (e) {
        var load_dep_count = 0; // loading dep counter
        var load_select_count = 0; // loading select counter
        var load_link_count = 0; // loading link counter
        var idep_arr = []; // dependecy string array
        var iselect_arr = []; // dependecy select array
        var ilink_arr = []; // link string array
        var dep_x, select_x, link_x, options_x;// used in respective for loops
        var dep_val, select_val, link_val;
        var dep_total = 0, select_total = 0, link_total = 0; //used as list lengths

        var title_Value = ""
        var desc_Value = "";
        var risk_Value = "";


        // Add Modal Title
        var pr = { "ProjectID": button_id, "RoadmapName": map_Name };

        //alert("Show " + button_id +  " show " + map_Name);



        //Add Modal Title

        //$.ajax({
        //    type: "GET",
        //    async: false,
        //    url: "Roadmap.aspx/GetProjectName",

        //    data: pr,
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (response) {
        //        $('#input_title').html(response);
        //    },
        //    error: function (xhr) {
        //        console.log("Nothing")
        //    },
        //    always: function (e) {
        //        console.log("Always")
        //    }
        //});

        //title_Value = PageMethods.GetProjectName(button_id, map_Name);

        ////adding the text to the description
        //desc_Value = PageMethods.GetProjectDescription(button_id,map_Name);
        //$('#descText').val(desc_Value);

        ////Find the number of input boxes to load
        //idep_arr = PageMethods.GetProjectDependencyText();
        //dep_total = idep_arr.length;

        ////Add all of thre input boxes
        //for (dep_x = 0; dep_x < dep_total; dep_x++) {
        //    $(dep_Text).append("<div class='new_dep'><input type='text' size=55 name='dep_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
        //};

        ////fill the inputboxes with their values
        //$('input[name=dep_input]').each(function () {
        //    $(this).val(idep_arr[load_dep_count]);
        //    load_dep_count++;
        //});

        ////Grab the list of project name string
        //iselect_arr = PageMethods.GetProjectDependency(button_id,map_Name);
        //select_total = nselect_arr.length;
        ////Create the options list
        //for (options_x = 0; options_x < select_total; options_x++) {
        //    options += "<option value='" + iselect_arr[options_x].val + ">" + iselect_arr[options_x].val + "</option>";
        //};

        ////Insert the correct number of selects
        //for (select_x = 0; select_x < select_total; select_x++) {
        //    $(dep_Select).append("<div class='new_sel'><select name='select_input'>"+options+"</select><a href='#' class='remove_field'>X</a></div>");
        //};

        ////fill the inputboxes with their values
        //$('input[name=select_input]').each(function () {
        //    $(this).val(iselect_arr[load_select_count]);
        //    load_select_count++;
        //});

        ////Fill in the Risks text area
        //risk_Value = PageMethods.GetProjectRisk(button_id,map_Name);
        //$('#riskText').val(risk_Value);

        ////Get link list size
        //ilink_arr = PageMethods.GetProjectLinks(button_id, map_Name);
        //link_total = ilink_arr.length;

        ////Add all of thre input boxes
        //for (link_x = 0; link_x < link_total; link_x++) {
        //    $(link_Text).append("<div class='new_link'><input type='text' size=60 name='link_input'/><a href='#' class='remove_field'>X</a></div>"); //add input box
        //};

        ////fill the inputboxes with their values
        //$('input[name=link_input]').each(function () {
        //    $(this).val(ilink_arr[load_link_count]);
        //    load_dep_count++;
        //});



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