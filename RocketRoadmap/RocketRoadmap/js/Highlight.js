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

    $(cheche).css('background-color', 'yellow');

    var disvalue = { 'ProjectID': button_id, 'RoadmapName': map_Name };
    $.ajax({
        type: "POST",
        async: false,
        url: "Roadmap.aspx/GetProjectDependencyArr",
        data: JSON.stringify(disvalue),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            for (dep_x = 0; dep_x < response.d.length; dep_x++) {
                dep_char = "#" + response.d[dep_x] + "But";
                dep_arr.push(dep_char);
            }
            color(dep_arr);
        },
        error: function (xhr) {
            console.log("error");
        },
    });
}

    function color(id) {
        var id_x;
        for (id_x = 0; id_x < id.length; id_x++) {
            $(id[id_x]).css('background-color', 'orange');
        }
    }

    function Uncolor(id) {
        var id_x;
        for (id_x = 0; id_x < id.length; id_x++) {
            $(id[id_x]).css('background-color', 'deepskyblue');
        }
    }

    function UnHighlight(id) {

        var cheche = "#" + id;

        $(cheche).css('background-color', 'deepskyblue');

        Uncolor(dep_arr);
        dep_arr = [];
    }
