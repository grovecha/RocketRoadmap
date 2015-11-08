/*
Functionality to highlight dependants
Emily Klopfer
*/

var panel_close = false;
var dep_arr = [];

function Highlight(id) {
    var cheche = "#" + id;
    button_id = id.substr(0, id.length - 3)
    var dep_x;
    var dep_char;

    var roadmap_url = window.location.href;
    var map_Name = roadmap_url.substr(roadmap_url.indexOf('?') + 1);
    map_Name = decodeURIComponent(map_Name.substr(2, map_Name.length));

    $(cheche).css('border-style', 'solid');
    $(cheche).css('border-color', 'red');
    $(cheche).css('border-width', '.2em');

    var disvalue = { 'ProjectID': button_id, 'RoadmapName': map_Name };
    $.ajax({
        type: "POST",
        async: false,
        url: "Roadmap.aspx/GetProjectDependencyArr",
        data: JSON.stringify(disvalue),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            for (dep_x = 0; dep_x < dep_arr.length; dep_x++) {
                dep_char = "#" + response.d[dep_x];
                dep_arr.push(dep_arr);
            }
            cheche(dep_arr);
        },
        error: function (xhr) {
            console.log("error");
        },
    });

    function cheche(dep_arr) {

        $(cheche).css('border-style', 'solid');
        $(cheche).css('border-color', 'orange');
        $(cheche).css('border-width', '.2em');
    }

    function Uncheche(dep_arr) {

        $(cheche).css('border-style', 'none');
        $(cheche).css('border-color', 'none');
        $(cheche).css('border-width', '0');
    }

    }

function UnHighlight(id) {

    var cheche = "#" + id;

    $(cheche).css('border-style', 'none');
    $(cheche).css('border-color', 'none');
    $(cheche).css('border-width', '0');

    Uncheche(dep_arr);
}
