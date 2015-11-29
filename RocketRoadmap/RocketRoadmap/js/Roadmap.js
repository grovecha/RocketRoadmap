var changing = true;
var dragging = true;

var color = ["#DC381F", "#33cccc", "#6CBB3C", "#A23BEC", "#157DEC", "#F87217"];


$(".block").resizable({ handles: 'e, w' });
$(".block").draggable({ axis: "x" });

function setProjPos(ProjId, pos, width) {
    
    StratId = ProjId.split("BusBox")[0];
    BusId = ProjId.split("ProjBox")[0];
    var url = window.location.href;
    var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1))
    mapName = mapName.substr(2, mapName.length).split('#')[0];
    PageMethods.SetProjPos(ProjId, mapName, StratId, BusId, pos, width);
}

function setTimePos(timeId, pos) {
    var url = window.location.href;
    var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1))
    mapName = mapName.substr(2, mapName.length).split('#')[0];
    PageMethods.SetProjPos(mapName, timeId, pos);
}

function showTime() {
    if ($(".timeline").is(':visible')) {
        $(".timeline").hide();
    }
    else {
        $(".timeline").show();
    }    
}

function disableModal() {
    dragging = true;
}
function showMode(id) {
    if (!dragging) {
        showModal(id);
    }
}

function enableDrag()
{
    var url = window.location.href;
    var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1))
    mapName = mapName.substr(2, mapName.length).split('#')[0];
    
    $(".proj1").draggable({
        axis: "x",
        containment: "#" + BusId + "td",
        drag: function (event, ui) {

        },
        stop: function (event, ui) {
            
            var pos = $("#" + this.id).position().left;
            var width = $("#" + this.id).width();
       
            setProjPos(this.id.split("But")[0], pos - 158, width);
        }

    });

    //$(".proj" + String(CurrentProjCount + 1)).resizable({
    $(".proj1").resizable({
        handles: 'e, w',
        containment: "#" + BusId + "td",

        stop: function (event, ui) {

            var pos = $("#" + this.id).position().left;
            var width = $("#" + this.id).width();

            setProjPos(this.id.split("But")[0], pos - 158, width);

            document.getElementById(this.id.split("But")[0] + "Label").style.width = ((width - 15).toString() + "px");
        }
    });
    $(".proj2").draggable({
        axis: "x"});
    $(".proj2").resizable({ handles: 'e, w' });
    $(".proj3").draggable({
        axis: "x"
    });
    $(".proj3").resizable({ handles: 'e, w' });
    $(".timeline").draggable({
        axis: "x", containment: "#containmentWrapper",
        stop: function (event, ui) {
            console.log("call me");
            var pos = $("#" + this.id).position().left;
            console.log(pos);
            PageMethods.EditTickLocation(mapName, pos, this.id);
        }
    });

    $(".proj1").draggable("enable");
    $(".proj1").resizable("enable");
    $(".proj2").draggable("enable");
    $(".proj2").resizable("enable");
    $(".proj3").draggable("enable");
    $(".proj3").resizable("enable");
    $(".timeline").draggable("enable");
}

function deleteTime(obj) {
    
    if (!FullScreen) {
        var timeline = document.getElementById(obj.id)
        timeline.parentNode.removeChild(timeline);
    }
}
function addTick(e, obj) {
    if (e.keyCode == 13) {
        //map name
        var url = window.location.href;
        var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1))
        mapName = mapName.substr(2, mapName.length).split('#')[0];

        //add timeline
        var timeline = document.createElement("div");
        timeline.setAttribute("ondblclick","deleteTime(this)")
        timeline.className = "timeline";
        timeline.id = obj.value;
        timeline.innerHTML = '<p  class="timelineText">' + obj.value + '</p>'
        var parent = document.getElementById("containmentWrapper");
        parent.appendChild(timeline);

        //add timeline to database
        PageMethods.AddTick(mapName, 0, obj.value);

        //draggable, edit location function
        $(".timeline").draggable({
            axis: "x", containment: "#containmentWrapper",
            stop: function (event, ui) {
                console.log("call me");
                var pos = $("#" + this.id).position().left;
                console.log(pos);
                PageMethods.EditTickLocation(mapName, pos, this.id);
            }
        });
    }
}

function hideStrat(StratString) {
    $(StratString).hide();
};
 
//$("#StratBox0BusBox0Row").hide();

function deleteStrat(obj) {
    
    var mainDiv = document.getElementById('sidebarTable');
    var PreviousStratNum = parseInt(obj.id.split('StratDelete')[1].split("BusBox")[0]);

    //don't allow deletion of last strat box
    if (!document.getElementById("StratBox" + PreviousStratNum.toString()).getAttribute("firstadd")) {
        return 0;
    }
    var mainDiv = document.getElementById('sidebarTable');
    var RowIndex = document.getElementById('StratBox' + (PreviousStratNum).toString() + "Row").rowIndex;
    mainDiv.deleteRow(RowIndex);

    //delete visual row 
    var table = document.getElementById("roadmapTable");
    //table.rows[PreviousStratNum].innerHTML = "";
    var PreviousStratRow = document.getElementById('StratVisual' + (PreviousStratNum).toString() + "Row").rowIndex;
    table.deleteRow(PreviousStratRow);
    
    try {
        //delete visual row 
        var table = document.getElementById("roadmapTable");
        //table.rows[PreviousStratNum].innerHTML = "";
        var PreviousStratRow = document.getElementById('StratVisual' + (PreviousStratNum).toString() + "Row").rowIndex;
        table.deleteRow(PreviousStratRow);
    }
    catch (err) {

    }

    //delete from database
    var url = window.location.href;
    var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1))
    mapName = mapName.substr(2, mapName.length).split('#')[0];
    PageMethods.DeleteStrat('StratBox' + (PreviousStratNum).toString(), mapName);

}

function deleteBus(obj) {
    var StratId = obj.id.split("BusBox")[0];
    var BusId = obj.id.split("Delete")[0];
    var StratTable = document.getElementById(StratId + "Table");
    var RowIndex = document.getElementById(BusId + "Row").rowIndex;

    //don't allow deletion of last strat box
    if (!document.getElementById(BusId).getAttribute("firstadd")) {
        return 0;
    }

    StratTable.deleteRow(RowIndex);

    try {
        //delete visual row 
        var table = document.getElementById(StratId + "VisualTable");
        table.deleteRow(RowIndex);
        currentheight = document.getElementById("StratBut" + String(CurrentStratCount)).style.height;
        document.getElementById("StratBut" + String(CurrentStratCount)).style.height = String(parseInt(currentheight) - 66) + "px";
    }
    catch (err) {

    }

    //delete from database
    var url = window.location.href;
    var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1));
    mapName = mapName.substr(2, mapName.length).split('#')[0];
    PageMethods.DeleteBus(BusId, StratId, mapName);
}

function deleteProj(obj) {

    var id = obj.id.split("Delete")[0];    

    //delete visual and input box
    var ele = document.getElementById(obj.id);
    ele.parentNode.removeChild(ele);
    
    var ele = document.getElementById(id);
    ele.parentNode.removeChild(ele);
    
    var ele = document.getElementById(id+"But");
    ele.parentNode.removeChild(ele);

    ////
    var ele = document.getElementById(id + "space");
    ele.parentNode.removeChild(ele);

    //delete from database
    var StratId = obj.id.split("BusBox")[0];
    var BusId = obj.id.split("ProjBox")[0];
    var ProjId = obj.id.split("Delete")[0];
    var url = window.location.href;
    var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1));
    mapName = mapName.substr(2, mapName.length).split('#')[0];
    PageMethods.DeleteProj(ProjId, BusId, StratId, mapName);
}


function addStrat(e, obj, i) {    
    
    if (e.keyCode == 13) {

        if (obj.value != "") {
            $("#" + obj.id + "BusBox0Row").show();
            document.getElementById(obj.id + "BusBox0Row").setAttribute("shown", "1");

        }

        //////Add visual 
        //if visual strat button already exists, change the value
        PreviousStratNum = parseInt(obj.id.split('StratBox')[1].split("BusBox")[0]);
        if (document.getElementById("StratBut" + PreviousStratNum.toString())) {

            var but = document.getElementById("StratBut" + PreviousStratNum.toString());
            var NewValue = document.getElementById("StratBox" + PreviousStratNum.toString()).value;
            if (NewValue != "") {
                but.value = NewValue;
            }

        }
        //otherwise add strategy point button
        else {

            var table = document.getElementById("roadmapTable");
            try {
                var PreviousStratRow = document.getElementById('StratVisual' + (PreviousStratNum).toString() + "Row").rowIndex;
                var row = table.rows[PreviousStratRow];

            }
            catch (err) {
                var row = table.insertRow(PreviousStratNum);
                row.setAttribute("id", 'StratVisual' + PreviousStratNum.toString() + "Row");
            }

            var element1 = document.createElement("button");

            var cell1 = row.insertCell(0);
            var element1 = document.createElement("input");
            element1.type = "button";
            element1.name = "Strat";
            element1.id = "StratBut" + PreviousStratNum.toString();

            var colorpick = document.createElement("colorPicker");
            colorpick.type = "color";


            var NewValue = obj.value;
            if (NewValue != "") {
                element1.value = NewValue;
            }


           // var colorNum = PreviousStratNum % color.length;

            var colorpicker = document.getElementById("ColorPicker" + PreviousStratNum.toString());
            var newColor= colorpicker.value;
            element1.className = "StratVis";
            element1.setAttribute("style", "background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, " + newColor + "), color-stop(1, " + newColor + ")); background:-moz-linear-gradient(top, " + newColor + " 5%, " + newColor + " 100%); background:-webkit-linear-gradient(top, " + newColor + " 5%, " + newColor + " 100%); background:-o-linear-gradient(top, " + newColor + " 5%, " + newColor + " 100%); background:-ms-linear-gradient(top, " + newColor + " 5%, " + newColor + " 100%); background:linear-gradient(to bottom, " + newColor + " 5%, " + newColor + " 100%); filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='" + newColor + "', endColorstr='b5121b',GradientType=0); background-color:" + newColor + ";")
            element1.style.height = "3.5em";

            var table1 = document.createElement("table");
            cell1.appendChild(table1);
            row1 = table1.insertRow(0);
            cell2 = row1.insertCell(0);
            div = document.createElement("div");
            cell2.appendChild(element1);



        }
        var url = window.location.href;
        var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1));
        mapName = mapName.substr(2, mapName.length).split('#')[0];
        //if new strat input already created, just edit database
        if (obj.getAttribute("firstadd") == "1") {

            PageMethods.EditStrat("StratBox" + PreviousStratNum.toString(), document.getElementById("StratBox" + PreviousStratNum.toString()).value, mapName);
        }
        //create new strat input and add entry to database
        else {
            PageMethods.AddStrat("StratBox" + PreviousStratNum.toString(), document.getElementById("StratBox" + PreviousStratNum.toString()).value,newColor, mapName);
            obj.setAttribute("firstadd", "1");
            PreviousStratNum = parseInt(obj.id.split('StratBox')[1].split("BusBox")[0]);
            NewStratCount = PreviousStratNum + 1;
            var table = document.getElementById("roadmapTable");
            var PreviousStratRow = document.getElementById('StratVisual' + (PreviousStratNum).toString() + "Row").rowIndex;
            var row = table.insertRow(PreviousStratRow + 1);
            row.setAttribute("id", 'StratVisual' + NewStratCount.toString() + "Row");

            var mainDiv = document.getElementById('sidebarTable');
            var varr = document.getElementById('StratBox' + (PreviousStratNum).toString() + "Row").rowIndex + 1;
            newrow = mainDiv.insertRow(varr);
            
            newrow.setAttribute("id", 'StratBox' + NewStratCount.toString() + "Row");
            //color input
            var colorNum = PreviousStratNum % color.length;

            newrow.innerHTML += "<td style='display:block;'>" +
                            "  <input type=\"color\" class=\"stratColor\" id=\"ColorPicker" + NewStratCount.toString() + "\" onchange=\"changeColor(" + NewStratCount.toString()+")\" value=\"" + color[colorNum + 1] + "\">" +
                            "<input class='txtStrat' BusTotal=1 id='StratBox" + NewStratCount.toString() + "' type='text' placeholder='Add Strategy Point' runat='server'  onkeyup='addStrat(event,this," + NewStratCount.toString() + ")'/><a href='#' id='StratDelete" + NewStratCount.toString() + "'class='remove_strat'> X</a> <br />" +
                            '<table style="display:block; height: 100%" id ="StratBox' + NewStratCount.toString() + 'Table"' + ' >' +
                            '<tr style="display:block;" id="StratBox' + NewStratCount.toString() + 'BusBox0Row" > ' +
                                '<td style="display:block;" id="StratBox' + NewStratCount.toString() + 'BusBox0Cell">' +
                            "<input  class='txtBus' ProjTotal=1 id='StratBox" + NewStratCount.toString() + "BusBox0' type='text' placeholder='Add Business Value' runat='server' onkeyup='addBus(event, this," + NewStratCount.toString() + ")' /><a href='#' id='StratBox" + NewStratCount.toString() + "BusBox0Delete' class='remove_bus'> X</a><br />" +
                            
                            "<input name='DynamicTextBox' id='StratBox" + NewStratCount.toString() + "BusBox0ProjBox0' class='txtProjDel' type='text' placeholder='Add Project' runat='server' onkeyup='addProj(event, this," + NewStratCount.toString() + ")' />" +
                            
                              '</td>' +
                            '</tr>' +
                            '</table>' +
                            "</td>";
                              $("#StratBox" + NewStratCount.toString() + "BusBox0Row").hide();

            //place cursor in  bus value
            document.getElementById(obj.id+"BusBox0").select();
           
        }

    }
    return false;
}

function addBus(e, obj, i) {

    CurrentStratCount = parseInt(obj.id.split("StratBox")[1].split('BusBox')[0]);
    CurrentBusCount = parseInt(obj.id.split("BusBox")[1].split('ProjBox')[0]);
    StratId = obj.id.split("BusBox")[0];
    BusTotal = parseInt(document.getElementById(StratId).getAttribute("BusTotal"));
    CurrentBusId = obj.id.split("BusBox")[0] + "BusBox" + String(BusTotal);

    //add strategy piont from business value
    if (e.keyCode === 13 && e.shiftKey && obj.id.indexOf("Proj") <= 0) {
        addStrat(e, obj, i)
    }

    else if (e.keyCode === 13) {

        ////add visual
        //if business value visual exists, change the text
        NextVisualId = StratId + "BusVisual" + String(CurrentBusCount);
        if (document.getElementById(NextVisualId)) {
            document.getElementById(NextVisualId).innerHTML = obj.value;
        }
            //otherwise add the business value visual
        else {
            //add visual business value
            var table = document.getElementById("roadmapTable");
            PreviousStratNum = parseInt(obj.id.split('StratBox')[1].split("BusBox")[0]);
            var PreviousStratRow = document.getElementById('StratVisual' + (PreviousStratNum).toString() + "Row").rowIndex;
            var row = table.rows[PreviousStratRow];

           
            tableid = StratId + "VisualTable";
            
            if (document.getElementById(tableid)) {
                table = document.getElementById(tableid);
                var rowIndex = document.getElementById(obj.id.split('ProjBox')[0] + "Row").rowIndex;
                row = table.insertRow(rowIndex);
                
                row.className = "RowVis";
                row.id = obj.id + "RowVis";
                cell = row.insertCell(0);
                cell.id = obj.id + "td";
                cell.className = "projtd";

                row.insertCell(1);
                row.insertCell(2);
                cell1 = row.insertCell(3);
                
                cell1.className = "BusVis";
                cell1.id = NextVisualId;
                cell1.innerHTML = obj.value;
                currentheight = document.getElementById("StratBut" + String(CurrentStratCount)).style.height.split('em')[0];
                console.log(currentheight);
                document.getElementById("StratBut" + String(CurrentStratCount)).style.height = String(parseFloat(currentheight) + 3.27) + "em";


            }
            else {
                var newcell = row.insertCell(1);
                var w = screen.width;
                newcell.style.width = w.toString() + "px";
                //newcell.style.backgroundColor = "white";
                newcell.className = "NewCellVis";
                tableid = StratId + "VisualTable";
                projtd = obj.id + "td";

                ////set top border if it is the first bus value
                if (obj.id == "StratBox0BusBox0") {
                    newcell.innerHTML = "<table id='" + tableid + "'>" +
                                        "<tr id='" + obj.id + "RowVis' class='RowVis' style = 'border-top: 2pt solid; border-top-color: #D3D3D3; '>" +
                                            '<td class = "projtd" id="' + projtd + '" >' +
                                            '</td>' +
                                            '<td ></td>' +
                                            '<td ></td>' +
                                            '<td class="BusVis" id="' + NextVisualId + '" >' + obj.value + '</td>' +
                                       '</tr>' +
                                    '</table>';
                }
                else {
                    newcell.innerHTML = "<table id='" + tableid + "'>" +
                                        "<tr id='" + obj.id + "RowVis' class='RowVis'>" +
                                            '<td class="projtd" id="' + projtd + '" >' +
                                            '</td>' +
                                            '<td ></td>' +
                                            '<td ></td>' +
                                            '<td class="BusVis" id="' + NextVisualId + '" >' + obj.value + '</td>' +
                                       '</tr>' +
                                    '</table>';
                }
            }
        }
        ////////Add input boxes
        CurrentBusCount = parseInt(obj.id.split("BusBox")[1].split('ProjBox')[0]);
        StratId = obj.id.split("BusBox")[0];
        BusTotal = parseInt(document.getElementById(StratId).getAttribute("BusTotal"));

        //add 1 to BusTotal
        document.getElementById(StratId).setAttribute("BusTotal", parseInt(BusTotal) + 1);

        var url = window.location.href;
        var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1));
        mapName = mapName.substr(2, mapName.length).split('#')[0];
        var PrevBusID = obj.id.split("BusBox")[0] + "BusBox" + String(BusTotal - 1);
        if (obj.getAttribute("firstadd") == "1") {
            PageMethods.EditBusVal(obj.id, obj.value, mapName, StratId);
        }
        else {
            PageMethods.AddBusVal(obj.id, obj.value, mapName, StratId);
            obj.setAttribute("firstadd", "1");

            //add 1 to current business count
            CurrentBusId = obj.id.split("BusBox")[0] + "BusBox" + String(BusTotal);
            var mainDiv = document.getElementById(StratId + "Table");
            var RowIndex = document.getElementById(obj.id.split('ProjBox')[0] + "Row").rowIndex + 1;
            newrow = mainDiv.insertRow(RowIndex);
            newrow.setAttribute("id", CurrentBusId + "Row");
            newrow.innerHTML = "<td style='display:block;' id='"+CurrentBusId+"Cell'>" +
                                "<input class='txtBus' ProjTotal=1 id='" + CurrentBusId + "' type='text' placeholder='Add Business Value' onkeyup='addBus(event, this, 1)' /><a href='#' id='"+CurrentBusId + "Delete' class='remove_bus'> X</a><br />" +
                                
                                "<input class='txtProjDel' id='" + CurrentBusId + "ProjBox0' type='text' placeholder='Add Project' onkeyup='addProj(event, this, 1)' />" +
                                
            "</td>";
            //place cursor in proj box         
            document.getElementById(obj.id +"ProjBox0").select();
        }
    }
 
    else {
        
    }
    return false;
}

function addProj(e, obj, i) {
    CurrentProjCount = parseInt(obj.id.split("ProjBox")[1]);
    BusId = obj.id.split("ProjBox")[0];
    ProjTotal = document.getElementById(BusId).getAttribute("ProjTotal");
    StratId = obj.id.substr(0, obj.id.indexOf('Bus'));
    if (e.keyCode === 13 && e.shiftKey) {
        addBus(e, obj, i);
    }
    if (e.keyCode === 13 && e.ctrlKey) {
        addStrat(e, obj, i);
    }
        //add project input box
    else if (e.keyCode === 13) {
        /////add visual project        
        //if the project exists, change the value
       
        if (document.getElementById(obj.id + "But")) {
            document.getElementById(obj.id + "Label").innerHTML = obj.value;
        }
        else {

            if (ProjTotal > 2) {
                //increase stratbut height
                var currentheight = document.getElementById("StratBut" + String(CurrentStratCount)).style.height.split("em");
                document.getElementById("StratBut" + String(CurrentStratCount)).style.height = String(parseInt(currentheight) + 1.95) + "em";

                //increase RowVis height
                var RowVis = obj.id.split("ProjBox")[0] + "RowVis";
                currentheight = document.getElementById(RowVis).style.height;
        
                document.getElementById(RowVis).style.height = (ProjTotal*2.5).toString() + "em";
            }

            var element1 = document.createElement("div");      
            element1.id = obj.id + "But";
            var NewValue = obj.value;
            if (NewValue != "") {
                
                element1.innerHTML = "<span id = '" + obj.id + "Label' class = 'projLabel' >" + NewValue + "</span>";
            }
            //element1.style.verticalAlign = "top";
            element1.setAttribute("ondblclick", "showModal(this.id)");
            element1.setAttribute("onclick", "Highlight(this.id)");
            //element1.setAttribute("onresize", "disableModal()");
            //element1.setAttribute("ondrag", "disableModal()");
            element1.setAttribute("onmouseleave", "UnHighlight(this.id)");

            ////set class, location of more than 3 projects will be the 3rd location
            if (CurrentProjCount > 2) {
                element1.setAttribute("class", "proj1");
            }
            else {
                element1.setAttribute("class", "proj1");
            }


            var sNum = parseInt(StratId.split("StratBox")[1]);

            var colorpicker = document.getElementById("ColorPicker" + PreviousStratNum.toString());
            var newColor = colorpicker.value;

            element1.style.backgroundColor = newColor;


            
            element1.value = "";

            var cell = document.getElementById(BusId + "td");
            cell.appendChild(element1);
            space = document.createElement('div');
            space.className = 'space';
            space.id = obj.id + 'space';
            cell.appendChild(space);
            

            //enable draggability and resizability
           
            $(".proj1").draggable({
                axis: "x",
                containment: "#"+BusId+"td",
                drag: function (event, ui) {
                    
                },            
                stop: function (event, ui) {
              
                    var pos = $("#" + this.id).position().left;
                    var width = $("#" + this.id).width();
               
                    setProjPos(this.id.split("But")[0], pos - 158, width);
                }
            
            });
            
            //$(".proj" + String(CurrentProjCount + 1)).resizable({
            $(".proj1").resizable({
                handles: 'e, w',
                containment: "#"+BusId+"td",
                       
                stop: function (event, ui) {
                
                    var pos = $("#" + this.id).position().left;
                    var width = $("#" + this.id).width();
             
                    setProjPos(this.id.split("But")[0], pos - 158, width);
          
                    document.getElementById(this.id.split("But")[0] + "Label").style.width = ((width - 15).toString() + "px");
                }
            });
            $(".proj3").draggable({
                axis: "x",
                containment: "#" + BusId + "td",
                drag: function (event, ui) {

                },
                stop: function (event, ui) {
                
                    var pos = $("#" + this.id).position().left;
                    var width = $("#" + this.id).width();
            
                    setProjPos(this.id.split("But")[0], pos-158, width);
                }
            });
            $(".proj3").resizable({
                handles: 'e, w',
                containment: "#" + BusId + "td",
          
                stop: function (event, ui) {
             
                    var pos = $("#" + this.id).position().left;
                    var width = $("#" + this.id).width();
                
                    setProjPos(this.id.split("But")[0], pos - 158, width);
                }
            });

            var element2 = document.createElement('a');
            element2.innerHTML = " X";
            element2.id = obj.id + "Delete";
            element2.setAttribute("href", "#");
            //element2.setAttribute("style", "color:white; font-size:20px; vertical-align:-3px")
            element2.className = 'remove_proj';
            success = $("#" + BusId + "Cell").append(element2);
            console.log("#" + BusId + "Cell" + success.toString());
        }


        if (obj.value == "") {

        }


        var url = window.location.href;
        var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1));
        mapName = mapName.substr(2, mapName.length).split('#')[0];
        if (obj.getAttribute("firstadd") == "1") {
            PageMethods.EditProject(obj.id, obj.value, mapName.toString(), StratId.toString(), BusId.toString());
        }
        else {
            PageMethods.AddProject(obj.id, obj.value, mapName.toString(), StratId.toString(), BusId.toString());
            obj.setAttribute("firstadd", "1");
            //add 1 to ProjTotal
            document.getElementById(BusId).setAttribute("ProjTotal", parseInt(ProjTotal) + 1);

            var newInput = document.createElement('input');
            newInput.type = 'text';
            newInput.className = 'txtProjDel';
            newInput.placeholder = 'Add Project';
            newInput.id = BusId + "ProjBox" + String(CurrentProjCount + 1);
            newInput.setAttribute('onkeyup', 'addProj(event, this, 1)');
            
            var projroot = obj.parentElement;
            projroot.appendChild(newInput);

            //Place cursor in next proj box
            document.getElementById(BusId + "ProjBox" + String(CurrentProjCount + 1)).select();
        }
    }
    return false;
}


function changeColor(index)
{
    var picker = document.getElementById("ColorPicker" + index.toString());

    var newColor = picker.value;

    

    var element1 = document.getElementById("StratBut" + index.toString());

    if (element1 != null)
    {
        element1.setAttribute("style", "background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, " + newColor + "), color-stop(1, " + newColor + ")); background:-moz-linear-gradient(top, " + newColor + " 5%, " + newColor + " 100%); background:-webkit-linear-gradient(top, " + newColor + " 5%, " + newColor + " 100%); background:-o-linear-gradient(top, " + newColor + " 5%, " + newColor + " 100%); background:-ms-linear-gradient(top, " + newColor + " 5%, " + newColor + " 100%); background:linear-gradient(to bottom, " + newColor + " 5%, " + newColor + " 100%); filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='" + newColor + "', endColorstr='b5121b',GradientType=0); background-color:" + newColor + ";")
        element1.style.height = "3.5em";

        var url = window.location.href;
        var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1));
        mapName = mapName.substr(2, mapName.length).split('#')[0];

        PageMethods.SetColor("StratBox" + index.toString(),newColor, mapName);


        var bustotal = parseInt(document.getElementById("StratBox" + index.toString()).getAttribute("BusTotal"));

        for (i = 0; i < bustotal - 1; ++i) {
            var projTotal = parseInt(document.getElementById("StratBox" + index.toString() + "BusBox" + i.toString()).getAttribute("ProjTotal"));
            for (j = 0; j < projTotal - 1; ++j) {
                //<div id="StratBox0BusBox0ProjBox0But" ondblclick="showModal(this.id)" onclick="Highlight(this.id)" onmouseleave="UnHighlight(this.id)" class="proj1 ui-draggable ui-draggable-handle ui-resizable" style="background-color: rgb(220, 56, 31);"><span id="StratBox0BusBox0ProjBox0Label" class="projLabel">project</span><div class="ui-resizable-handle ui-resizable-e" style="z-index: 90;"></div><div class="ui-resizable-handle ui-resizable-w" style="z-index: 90;"></div></div>
                var p = document.getElementById("StratBox" + index.toString() + "BusBox" + i.toString() + "ProjBox" + j.toString() + "But");
                p.style.backgroundColor = picker.value;
            }
        }
    }

}

