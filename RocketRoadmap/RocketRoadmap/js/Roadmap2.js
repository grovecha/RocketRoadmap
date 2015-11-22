var changing = true;
var dragging = true;
var globalSmallestPos;
var projtoggle = false;
var color = ["#FF6600", "#FFBB00", "#00E038", "#4949CC", "#b5121b", "#3DBCFC", "#0EA4B5"];

$(".block").resizable({ handles: 'e, w' });
$(".block").draggable({ axis: "x" });


function showStrat(obj, event) {
    StratId = obj.id.split("LabelFiller")[0];
    $("#" + obj.id.split("LabelFiller")[0] + "NewCellVis").show();
    $("#" + StratId + "LabelFiller").hide();
    $("#" + StratId + "Label").show();
    

    BusTotal = parseInt(document.getElementById(StratId).getAttribute("BusTotal"));
    for (i = 0; i < BusTotal; i++) {
        try {
            element = '#' + StratId + "BusBox" + i.toString() + "RowVis";
            $(element).show();
        }
        catch (err) {
        }
    }
}
function toggleBus(obj, ctrlkey) {
    if (!ctrlkey) {
        return 0;
    }
    var StratId = obj.id.split("NewCellVis")[0].split("LabelFiller")[0];
    BusTotal = parseInt(document.getElementById(StratId).getAttribute("BusTotal"));
 
    element1 = '#' + StratId + "BusBox0RowVis";
 
    var isvisible = $(element1).is(":visible");
    var element = '';
    for (i = 0; i < BusTotal; i++) {
        try {
            element = '#' + StratId + "BusBox" + i.toString() + "RowVis";
            if (isvisible) {
                $(element).hide();
            }
            else {
                $(element).show();
            }
        }
        catch (err) {
        }
    }
    if (isvisible) {
        element = '#' + StratId + "NewCellVis";
        $(element).hide();
        
        $("#" + StratId + "LabelFiller").show();
        $("#" + StratId + "Label").hide();
        console.log(StratID);
    }
    else {
        console.log(StratID);
        element = '#' + StratId + "NewCellVis";
        $(element).show();
        $("#" + StratId + "LabelFiller").hide();
        $("#" + StratId + "Label").show();
        $("#" + StratId + "LabelFiller").hide();
    } 
}

function toggleProj(obj, shiftkey) {
    element1 = '#' + obj.id.split("ProjBox")[0] + "ProjBox0But";

    if (!shiftkey) {
        if ($(element1).is(":visible")) {
            return 0;
        }
    }
    BusId = obj.id.split("RowVis")[0];
    ProjTotal = parseInt(document.getElementById(BusId).getAttribute("ProjTotal"));

    var isvisible = $(element1).is(":visible");
    var element = '';
    console.log(obj.id.split('BusBox') + "BusVisual" + BusId.split("BusBox")[1]);
    if (isvisible) {
        document.getElementById(obj.id.split('BusBox')[0] + "BusVisual" + BusId.split("BusBox")[1]).className = "BusVis3";
    }
    else {
        document.getElementById(obj.id.split('BusBox')[0] + "BusVisual" + BusId.split("BusBox")[1]).className = "BusVis2";
    }

    for (i = 0; i < BusTotal; i++) {
        try {
            element = '#' + BusId + "ProjBox" + i.toString() + "But";
            if (isvisible) {
                $(element).hide();
            }
            else {
                $(element).show();
            }
        }
        catch (err) {

        }
    }

}

function resize(id) {
    var BusId = id.split("ProjBox")[0];
    var ProjTotal = document.getElementById(BusId).getAttribute("ProjTotal");

    var smallestPos = 2500;
    var largestPos = 0;
    var position = 2500;
    //get first and last position
    for (i = 0; i < ProjTotal; i++) {
        try {
            console.log("i" + i.toString());
            position = $('#' + BusId + "ProjBox" + i.toString() + "But").position();
            width = $('#' + BusId + "ProjBox" + i.toString() + "But").width();
            console.log("position:" + position.left.toString());
            console.log("width:" + width.toString());
            if (position.left < smallestPos) {
                smallestPos = position.left;
            }
            if (width + position.left > largestPos) {
                largestPos = width + position.left;
            }
        }
        catch (err) {

        }
    }

    StratId = id.split("BusBox")[0];

    var totalWidth = largestPos - smallestPos;
    console.log("largestPos:" + largestPos.toString());
    console.log("smallestPos:" + smallestPos.toString());
    //edit bus value position and width
    var currentBusWidth = $("#" + BusId + "RowVis").width();
    console.log("totalwidth + 90" + (totalWidth + 90).toString());
    console.log("currentbuswidth" + currentBusWidth.toString());
    //if ((totalWidth + 90) - currentBusWidth > 1) {
    document.getElementById(BusId + "RowVis").style.width = (totalWidth + 90).toString() + "px";

    //resize bus value
    try {
        currenttranslate = document.getElementById(BusId + "RowVis").style.transform.split("translateX(")[1].split("px)")[0];

    }
    catch (err) {
        currenttranslate = 0;
    }
    try {
        currentStratTranslate = document.getElementById(id.split("BusBox")[0] + "NewCellVis").style.transform.split("translateX(")[1].split("px)")[0];
    }
    catch (err) {
        currentStratTranslate = 0;
    }
    console.log(currenttranslate);
    console.log(currentStratTranslate);
    document.getElementById(BusId + "RowVis").style.transform = "translateX(" + (smallestPos - 16 - currentStratTranslate).toString() + "px)";
    currentleft = document.getElementById(id).style.left;
    document.getElementById(id).style.left = (parseInt(currentleft.split("px")[0]) - (-currenttranslate + smallestPos - 16 - currentStratTranslate)).toString() + "px";



    //get first and last bus position
    BusTotal = parseInt(document.getElementById(StratId).getAttribute("BusTotal"));

    var smallestPos = 2500;
    var largestPos = 0;
    var position = 2500;
    //get first and last position
    for (i = 0; i < BusTotal; i++) {
        try {
            console.log("i" + i.toString());
            position = $('#' + StratId + "BusBox" + i.toString() + "RowVis").position();
            width = $('#' + StratId + "BusBox" + i.toString() + "RowVis").width();
            console.log("Bus position:" + position.left.toString());
            console.log("Bus width:" + width.toString());
            if (position.left < smallestPos) {
                smallestPos = position.left;
            }
            if (width + position.left > largestPos) {
                largestPos = width + position.left;
            }
        }
        catch (err) {

        }
    }

    var totalWidth = largestPos - smallestPos;
    previoustranslate = currenttranslate;
    //edit bus value position and width    
    try {
        currenttranslate = document.getElementById(BusId + "RowVis").style.transform.split("translateX(")[1].split("px)")[0];

    }
    catch (err) {
        currenttranslate = 0;
    }

    try {
        previousStratTranslate = document.getElementById(id.split("BusBox")[0] + "NewCellVis").style.transform.split("translateX(")[1].split("px)")[0];
    }
    catch (err) {
        previousStratTranslate = 0;
    }

    document.getElementById(StratId + "NewCellVis").style.width = (totalWidth + 42).toString() + "px";
    stratNum = StratId.split("StratBox")[1];

    document.getElementById(StratId + "NewCellVis").style.transform = "translateX(" + (smallestPos - 10.3).toString() + "px)";
    document.getElementById(StratId + "Label").style.transform = "translateX(" + (smallestPos - 13).toString() + "px)";

    try {
        currentStratTranslate = document.getElementById(id.split("BusBox")[0] + "NewCellVis").style.transform.split("translateX(")[1].split("px)")[0];
    }
    catch (err) {
        currentStratTranslate = 0;
    }

    console.log("currentStratTranslate" + currentStratTranslate.toString());
    console.log("previousStratTranslate" + previousStratTranslate.toString());
    document.getElementById(BusId + "RowVis").style.transform = "translateX(" + (parseInt(currenttranslate) - parseInt(currentStratTranslate) + parseInt(previousStratTranslate)).toString() + "px)";

    NewCellVisid = id.split("BusBox")[0] + "NewCellVis";
    //document.getElementById(NewCellVisid).style.width = (largestPos - smallestPos + 5).toString() + "px";

    console.log(totalWidth + 39);
    console.log(smallestPos);

    //resize label
    stratvis = document.getElementById(StratId + "NewCellVis");
    height = stratvis.style.height;
    translate = stratvis.style.transform;
    width = stratvis.style.width;
    labelfiller = document.getElementById(StratId + "LabelFiller");
    labelfiller.style.height = height;
    labelfiller.style.transform = translate;
    labelfiller.style.width = (parseInt(width.split("px")[0]) + 80).toString() + "px";
    labelfiller.style.lineHeight = height;

}

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

function enableDrag() {

    var url = window.location.href;
    var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1))
    mapName = mapName.substr(2, mapName.length).split('#')[0];
    $(".proj1").draggable({
        axis: "x",
        //containment: "#" + this.id.split("ProjBox")[0] + "td",
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
        //containment: "#" + this.id.split("ProjBox")[0] + "td",

        stop: function (event, ui) {

            var pos = $("#" + this.id).position().left;
            var width = $("#" + this.id).width();

            setProjPos(this.id.split("But")[0], pos - 158, width);

            document.getElementById(this.id.split("But")[0] + "Label").style.width = ((width - 15).toString() + "px");
        }
    });
    $(".proj2").draggable({
        axis: "x"
    });
    $(".proj2").resizable({ handles: 'e, w' });
    $(".proj3").draggable({
        axis: "x"
    });
    $(".proj3").resizable({ handles: 'e, w' });
    $(".timeline").draggable({ axis: "x", containment: "#containmentWrapper", });



    $(".proj1").draggable("enable");
    $(".proj1").resizable("enable");
    $(".proj2").draggable("enable");
    $(".proj2").resizable("enable");
    $(".proj3").draggable("enable");
    $(".proj3").resizable("enable");
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
        timeline.setAttribute("ondblclick", "deleteTime(this)")
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

    //don't allow deletion of last bus box
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

    var ele = document.getElementById(id + "But");
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
        var label = document.getElementById(obj.id + "Label");
        var labelfiller = document.getElementById(obj.id + "LabelFiller");
        if (label) {
            var NewValue = document.getElementById("StratBox" + PreviousStratNum.toString()).value;
            if (NewValue != "") {
                label.innerHTML = "<div class='Labeldiv'>" + NewValue + "</div>";
                document.getElementById(obj.id + "LabelFiller").innerHTML = NewValue;
            }
        }
        else if (labelfiller) {
            labelfiller.innerHTML = NewValue;
        }
            //otherwise add strategy point button
        else {
            var table = document.getElementById("roadmapTable");
            try {
                var PreviousStratRow = document.getElementById('StratVisual' + (PreviousStratNum).toString() + "Row").rowIndex;
                var row = table.rows[PreviousStratRow];
                row.className = "StratVisRow";
            }
            catch (err) {
                var row = table.insertRow(PreviousStratNum);
                row.setAttribute("id", 'StratVisual' + PreviousStratNum.toString() + "Row");
                row.className = "StratVisRow";
            }
            //set border for timeline (may need to change so only bus value has timeline
            if (PreviousStratNum == 0) {
                row.setAttribute('style', 'border-top: 2pt solid; border-top-color: #D3D3D3; ');
            }
            var colorNum = PreviousStratNum % color.length;
            var element1 = document.createElement("button");

            //var cell2 = row.insertCell(0);
            var cell1 = document.createElement("div");
            cell1.setAttribute("style", "background-color:" + color[colorNum] + ";");
            cell1.setAttribute("id", obj.id + "LabelFiller");
            cell1.setAttribute('onclick', 'showStrat(this, event)');
            cell1.setAttribute("style", "background-color:" + color[colorNum]);
            cell1.className = "stratLabelFiller";
            if (obj.value != "") {
                cell1.innerHTML = obj.value;
            }
            //var NewValue = "<div style = ' background-color:" + color[colorNum] + ";' class='stratLabelFiller'>" + obj.value + "</div>";

            row.appendChild(cell1);
            //cell2.appendChild(cell1);
            var element1 = document.createElement("input");
            element1.type = "button";
            element1.name = "Strat";
            element1.id = "StratBut" + PreviousStratNum.toString();

            var NewValue = obj.value;
            if (NewValue != "") {
                element1.value = NewValue;
            }
            element1.className = "StratVis2";
            //strat label
            var td1 = document.createElement("td");
            var colorNum = PreviousStratNum % color.length;


            var td2 = document.createElement("td");
            td2.style.width = "240px";
            td2.id = obj.id + "filler";
            //cell1.appendChild(td2);
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
            PageMethods.AddStrat("StratBox" + PreviousStratNum.toString(), document.getElementById("StratBox" + PreviousStratNum.toString()).value, mapName);
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

            newrow.innerHTML = "<td>" +
                            "<input class='txtStrat' BusTotal=1 id='StratBox" + NewStratCount.toString() + "' type='text' placeholder='Add Strategy Point' runat='server'  onkeyup='addStrat(event,this," + NewStratCount.toString() + ")'/><a href='#' id='StratDelete" + NewStratCount.toString() + "'class='remove_strat'> X</a> <br />" +
                            '<table id ="StratBox' + NewStratCount.toString() + 'Table"' + ' >' +
                            '<tr id="StratBox' + NewStratCount.toString() + 'BusBox0Row" > ' +
                                '<td id="StratBox' + NewStratCount.toString() + 'BuxBox0inputtd">' +
                            "<input  class='txtBus' ProjTotal=1 id='StratBox" + NewStratCount.toString() + "BusBox0' type='text' placeholder='Add Business Value' runat='server' onkeyup='addBus(event, this," + NewStratCount.toString() + ")' /><a href='#' id='StratBox" + NewStratCount.toString() + "BusBox0Delete' class='remove_bus'> X</a><br />" +
                            '<div id="StratBox' + NewStratCount.toString() + 'BusBox0projDiv">' +
                            "<input name='DynamicTextBox' id='StratBox" + NewStratCount.toString() + "BusBox0ProjBox0' class='txtProjDel' type='text' placeholder='Add Project' runat='server' onkeyup='addProj(event, this," + NewStratCount.toString() + ")' />" +
                            "</div>"
            '</td>' +
          '</tr>' +
          '</table>' +
          "</td>";
            //  $("#StratBox" + NewStratCount.toString() + "BusBox0Row").hide();

            //place cursor in  bus value
            document.getElementById(obj.id + "BusBox0").select();

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

                row.className = "RowVis2";
                row.setAttribute('onclick', 'toggleProj(this, event.shiftKey)');
                row.id = obj.id + "RowVis";
                cell = row.insertCell(0);
                cell.id = obj.id + "td";
                cell.className = "projtd2";

                row.insertCell(1);
                row.insertCell(2);
                cell1 = row.insertCell(3);

                cell1.className = "BusVis2";
                cell1.id = NextVisualId;
                cell1.innerHTML = obj.value;

                //increase stratvis height
                NewCellVisid = obj.id.split("BusBox")[0] + "NewCellVis";
                currentheight = document.getElementById(NewCellVisid).style.height;
                document.getElementById(NewCellVisid).style.height = (parseInt(currentheight.split("px")[0]) + 72).toString() + "px";

                stratLabel = obj.id.split("BusBox")[0] + "Label";
                document.getElementById(stratLabel).style.height = (parseInt(currentheight.split("px")[0]) + 72).toString() + "px";

            }
            else {
                //var filler = document.getElementById(StratId + "filler");
                //filler.parentNode.removeChild(filler);

                var newcell = row.insertCell(0);
                newcell.setAttribute("style", "padding: 0;")
                //newcell.style.backgroundColor = "white";
                //newcell.className = "NewCellVis2";
                //newcell.id = obj.id.split("BusBox")[0] + "NewCellVis";
                NewCellVisid = obj.id.split("BusBox")[0] + "NewCellVis";
                tableid = StratId + "VisualTable";
                projtd = obj.id + "td";

                var colorNum = PreviousStratNum % color.length;
                NewCellVisstyle = "height: 74px; background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, " + color[colorNum] + "), color-stop(1, " + color[colorNum] + ")); background:-moz-linear-gradient(top, " + color[colorNum] + " 5%, " + color[colorNum] + " 100%); background:-webkit-linear-gradient(top, " + color[colorNum] + " 5%, " + color[colorNum] + " 100%); background:-o-linear-gradient(top, " + color[colorNum] + " 5%, " + color[colorNum] + " 100%); background:-ms-linear-gradient(top, " + color[colorNum] + " 5%, " + color[colorNum] + " 100%); background:linear-gradient(to bottom, " + color[colorNum] + " 5%, " + color[colorNum] + " 100%); filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='" + color[colorNum] + "', endColorstr='b5121b',GradientType=0); background-color:" + color[colorNum] + ";"
                ////set top border if it is the first bus value
                var stratlabel = document.getElementById(StratId + "LabelFiller");
                $("#" + StratId + "LabelFiller").hide();
                //stratlabel.parentNode.removeChild(stratlabel);

                labelValue = document.getElementById(StratId).value;

                var stratlabel = "<div style = 'background-color:" + color[colorNum] + ";' class='stratLabel' id='" + StratId + "Label'><div class='Labeldiv'>" + labelValue + "</div></div>";
                if (obj.id == "StratBox0BusBox0") {
                    newcell.innerHTML = "<div onclick='toggleBus(this, event.ctrlKey)' style='" + NewCellVisstyle + "' id = '" + NewCellVisid + "' class='NewCellVis2'><table id='" + tableid + "'>" +
                                        "<tr onclick='toggleProj(this, event.shiftKey)' id='" + obj.id + "RowVis' class='RowVis2' >" +
                                            '<td class = "projtd2" id="' + projtd + '" >' +
                                            '</td>' +
                                            '<td ></td>' +
                                            '<td ></td>' +
                                            '<td class="BusVis2" id="' + NextVisualId + '" >' + obj.value + '</td>' +
                                       '</tr>' +
                                    '</table></div>' + stratlabel;
                }
                else {
                    newcell.innerHTML = "<div onclick='toggleBus(this, event.ctrlKey)' style='" + NewCellVisstyle + "' id = '" + NewCellVisid + "' class='NewCellVis2'><table id='" + tableid + "'>" +
                                        "<tr onclick='toggleProj(this, event.shiftKey)' id='" + obj.id + "RowVis' class='RowVis2'>" +
                                            '<td class="projtd2" id="' + projtd + '" >' +
                                            '</td>' +
                                            '<td ></td>' +
                                            '<td ></td>' +
                                            '<td class="BusVis2" id="' + NextVisualId + '" >' + obj.value + '</td>' +
                                       '</tr>' +
                                    '</table></div>' + stratlabel;
                }


            }
            //resize label
            stratvis = document.getElementById(StratId + "NewCellVis");
            height = stratvis.style.height;
            translate = stratvis.style.transform;
            width = stratvis.style.width;
            labelfiller = document.getElementById(StratId + "LabelFiller");
            labelfiller.style.height = height;
            labelfiller.style.transform = translate;
            labelfiller.style.width = (parseInt(width.split("px")[0]) + 80).toString() + "px";
            labelfiller.style.lineHeight = height;
            
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
            newrow.innerHTML = "<td id='" + CurrentBusId + "inputtd'>" +
                                "<input class='txtBus' ProjTotal=1 id='" + CurrentBusId + "' type='text' placeholder='Add Business Value' onkeyup='addBus(event, this, 1)' /><a href='#' id='" + CurrentBusId + "Delete' class='remove_bus'> X</a><br />" +
                                '<div id="' + CurrentBusId + 'projDiv">' +
                                "<input class='txtProjDel' id='" + CurrentBusId + "ProjBox0' type='text' placeholder='Add Project' onkeyup='addProj(event, this, 1)' />" +
                                "</div>"
            "</td>";
            //place cursor in proj box         
            document.getElementById(obj.id + "ProjBox0").select();
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
                //increase RowVis height
                var RowVis = obj.id.split("ProjBox")[0] + "RowVis";
                currentheight = document.getElementById(RowVis).style.height;

                document.getElementById(RowVis).style.height = (ProjTotal * 31).toString() + "px";

                //increase stratvis height
                NewCellVisid = obj.id.split("BusBox")[0] + "NewCellVis";
                currentheight = document.getElementById(NewCellVisid).style.height;
                console.log(currentheight);
                document.getElementById(NewCellVisid).style.height = (parseInt(currentheight.split("px")[0]) + 31).toString() + "px";

                stratLabel = obj.id.split("BusBox")[0] + "Label";
                //currentheight = document.getElementById(stratLabel).style.height;
                document.getElementById(stratLabel).style.height = (parseInt(currentheight.split("px")[0]) + 31).toString() + "px";
            }

            var element1 = document.createElement("div");

            element1.id = obj.id + "But";


            var NewValue = obj.value;
            if (NewValue != "") {

                element1.innerHTML = "<span id = '" + obj.id + "Label' style='width: 115px; white-space: nowrap; overflow: hidden; display: inline-block; transform: translateY(-0px); vertical-align: middle; line-height: normal;'>" + NewValue + "</span>";
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
            var colorNum = sNum % color.length;

            element1.style.backgroundColor = color[colorNum];

            element1.value = "";

            var cell = document.getElementById(BusId + "td");
            cell.appendChild(element1);
            space = document.createElement('div');
            space.className = 'space2';
            space.id = obj.id + 'space';
            cell.appendChild(space);

            //get global smallest pos
            //globalSmallestPos = $("#" + obj.id).position().left;

            //enable draggability and resizability

            $(".proj1").draggable({
                axis: "x",
                //containment: "#" + BusId + "td",
                drag: function (event, ui) {

                },
                stop: function (event, ui) {

                    var pos = $("#" + this.id).position().left;
                    var width = $("#" + this.id).width();

                    setProjPos(this.id.split("But")[0], pos - 158, width);
                    resize(this.id);
                }

            });

            //$(".proj" + String(CurrentProjCount + 1)).resizable({
            $(".proj1").resizable({
                handles: 'e, w',
                //containment: "#" + BusId + "td",
                drag: function (event, ui) {
                    globalSmallestPos = $("#" + obj.id).position().left;
                    position = $('#' + BusId + "ProjBox" + i.toString() + "But").position();
                    console.log(position);
                },

                stop: function (event, ui) {

                    var pos = $("#" + this.id).position().left;
                    var width = $("#" + this.id).width();

                    setProjPos(this.id.split("But")[0], pos - 158, width);

                    document.getElementById(this.id.split("But")[0] + "Label").style.width = ((width - 15).toString() + "px");
                    resize(this.id);
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

                    setProjPos(this.id.split("But")[0], pos - 158, width);
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
            $("#" + BusId + "projDiv").append(element2);
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

