/*
Functionality to highlight dependants
Emily Klopfer
*/


var depon_arr = [];
var depof_arr = [];
var dtemp1 = [];
var dtemp2 = [];

function Highlight(id) {
    var cheche = "#" + id;
    button_id = id.substr(0, id.length - 3)
    var dep_x;
    var dep_char;

    var roadmap_url = window.location.href;
    var map_Name = roadmap_url.substr(roadmap_url.indexOf('?') + 1);
    map_Name = decodeURIComponent(map_Name.substr(2, map_Name.length));
    var togglebool = document.getElementById("sidebar-wrapper").getAttribute("Present");

    if (togglebool == "true") {
        $(cheche).css('background-color', 'yellow');

        var disvalue = { 'ProjectID': button_id, 'RoadmapName': map_Name };
        $.ajax({
            type: "POST",
            async: false,
            url: "Roadmap.aspx/GetProjectDependencyArr",
            data: JSON.stringify(disvalue),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                dtemp1 = response.d[0];
                dtemp2 = response.d[1];
                for (dep_x = 0; dep_x < dtemp1.length; dep_x++) {
                    dep_char = "#" + dtemp1[dep_x] + "But";
                    depon_arr.push(dep_char);
                }
                for (dep_x = 0; dep_x < dtemp2.length; dep_x++) {
                    dep_char = "#" + dtemp2[dep_x] + "But";
                    depof_arr.push(dep_char);
                }
                dependoncolor(depon_arr);
                dependantofcolor(depof_arr);
            },
            error: function (xhr) {
                console.log("error");
            },
        });
    }
}

function dependoncolor(id) {
    var id_x;
    for (id_x = 0; id_x < id.length; id_x++) {
        $(id[id_x]).css('background-color', 'deepskyblue');
    }
}
function dependantofcolor(id) {
    var id_x;
    for (id_x = 0; id_x < id.length; id_x++) {
        $(id[id_x]).css('background-color', 'purple');
    }
}

function Uncolor(id) {
    var id_x;

    for (id_x = 0; id_x < id.length; id_x++) {

        var temp = id[id_x].split("StratBox");
        temp = temp[1].split("BusBox")[0]

        var return_c = document.getElementById("ColorPicker" + temp).value;

        $(id[id_x]).css('background-color', return_c);
    }
}

function UnHighlight(id) {

    var cheche = "#" + id;
    var temp = id.split("StratBox");
    temp = temp[1].split("BusBox")[0]
    var return_c = document.getElementById("ColorPicker" + temp).value;

    $(cheche).css({ "border-color": return_c });
    $(cheche).css({"border-style": "ridge"});

    Uncolor(depon_arr);
    Uncolor(depof_arr);
    depon_arr = [];
    depof_arr = [];
}
