
var StratBoxCounter = 0;
//$("#StratBox0BusBox0Row").hide();

function deleteStrat(obj) {

    var mainDiv = document.getElementById('sidebarTable');
    var PreviousStratNum = parseInt(obj.id.split('StratDelete')[1].split("BusBox")[0]);
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


}
function deleteBus(e, obj) {
    var StratId = obj.id.split("BusBox")[0];
    var BusId = obj.id.split("Delete")[0];
    var StratTable = document.getElementById(StratId + "Table");
    var RowIndex = document.getElementById(BusId + "Row").rowIndex;
    StratTable.deleteRow(RowIndex);

    //delete visual row 
    var table = document.getElementById(StratId + "VisualTable");
    table.deleteRow(RowIndex);
    currentheight = document.getElementById("StratBut" + String(CurrentStratCount)).style.height;
    document.getElementById("StratBut" + String(CurrentStratCount)).style.height = String(parseInt(currentheight) - 100) + "px";

}
function addStrat(e, obj, i) {
    //if (e.keyCode == 9) {
    //    document.getElementById(obj.id+"BusBox0").select();
    //    return false;
    //}

    ///////
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
            row.setAttribute("id", 'StratVisual' + StratBoxCounter.toString() + "Row");
        }

        //row.id = "StratRow" + PreviousStratNum.toString();
        var element1 = document.createElement("button");

        var cell1 = row.insertCell(0);
        var element1 = document.createElement("input");
        element1.type = "button";
        element1.name = "Strat";
        element1.id = "StratBut" + PreviousStratNum.toString();

        var NewValue = obj.value;
        if (NewValue != "") {
            element1.value = NewValue;
        }


        element1.style.backgroundColor = "red";
        element1.style.height = "100px";
        element1.style.width = "200px";

        var table1 = document.createElement("table");
        cell1.appendChild(table1);
        row1 = table1.insertRow(0);
        cell2 = row1.insertCell(0);
        div = document.createElement("div");
        //cell1.appendChild(div);
        cell2.appendChild(element1);

        //row1.style.verticalAlign = "top";

    }


    //delete strategy point and associated business values and projects
    if (0) {
        var mainDiv = document.getElementById('sidebarTable');
        PreviousStratNum = parseInt(obj.id.split('StratBox')[1].split("BusBox")[0]);
        var mainDiv = document.getElementById('sidebarTable');
        var varr = document.getElementById('StratBox' + (PreviousStratNum).toString() + "Row").rowIndex;

        mainDiv.deleteRow(varr);
    }

        //add new strategy point, business value and project
    else if (e.keyCode == 13) {

        var url = window.location.href;
        var mapName = url.substr(url.indexOf('?') + 1);
        mapName = mapName.substr(2, mapName.length);
        
        PageMethods.AddStrat("StratBox"+PreviousStratNum.toString(),document.getElementById("StratBox" + PreviousStratNum.toString()).value,mapName)

        StratBoxCounter++;
        var table = document.getElementById("roadmapTable");
        var PreviousStratRow = document.getElementById('StratVisual' + (PreviousStratNum).toString() + "Row").rowIndex;
        var row = table.insertRow(PreviousStratRow + 1);
        row.setAttribute("id", 'StratVisual' + StratBoxCounter.toString() + "Row");

        PreviousStratNum = parseInt(obj.id.split('StratBox')[1].split("BusBox")[0]);
        //var table = document.getElementById("roadmapTable");
        //var row = table.insertRow(PreviousStratNum);

        var mainDiv = document.getElementById('sidebarTable');
        var varr = document.getElementById('StratBox' + (PreviousStratNum).toString() + "Row").rowIndex + 1;
        newrow = mainDiv.insertRow(varr);
        newrow.setAttribute("id", 'StratBox' + StratBoxCounter.toString() + "Row");



        newrow.innerHTML = "<td>" +
                        "<input class='txtStrat' BusTotal=1 id='StratBox" + StratBoxCounter.toString() + "' type='text' placeholder='Add Strategy Point' runat='server'  onkeyup='addStrat(event,this," + StratBoxCounter.toString() + ")'/><button class = 'btnDelete' type='button' id='StratDelete" + StratBoxCounter.toString() + "' onclick='deleteStrat(event,this)'>X</button> <br />" +
                        '<table id ="StratBox' + StratBoxCounter.toString() + 'Table"' + ' >' +
                        '<tr id="StratBox' + StratBoxCounter.toString() + 'BusBox0Row" > ' +
                            '<td>' +
                        "<input  class='txtBus' ProjTotal=1 id='StratBox" + StratBoxCounter.toString() + "BusBox0' type='text' placeholder='Add Business Value' runat='server' onkeyup='addBus(event, this," + StratBoxCounter.toString() + ")' /><button class = 'btnDelete' type='button' id='StratBox" + StratBoxCounter.toString() + "BusBox0Delete' onclick='deleteBus(event, this)'>X</button><br />" +
                        "<input name='DynamicTextBox' id='StratBox" + StratBoxCounter.toString() + "BusBox0ProjBox0' class='txtProj' type='text' placeholder='Add Project' runat='server' onkeyup='addProj(event, this," + StratBoxCounter.toString() + ")' /><br />" +
                            '</td>' +
                        '</tr>' +
                        '</table>' +
                        "</td>";
       // $("#StratBox" + StratBoxCounter.toString() + "BusBox0Row").hide();
        //place cursor in next strat point
        //document.getElementById("StratBox" + StratBoxCounter.toString()).focus();
        document.getElementById("StratBox" + StratBoxCounter.toString()).select();


    }

        //place cursor in buxiness value box
    else if (0) {

        PreviousStratNum = parseInt(obj.id.split('StratBox')[1]);

        //document.getElementById("StratBox" + PreviousStratNum.toString() + 'BusBox0').focus();
        document.getElementById("StratBox" + PreviousStratNum.toString() + 'BusBox0').select();
    }


    //document.getElementById("StratBox" + CurrentStratCount.toString() + 'BusBox1').focus();
    //document.getElementById("StratBox" + CurrentStratCount.toString() + 'BusBox1').select();

    //add 1 to BusTotal
    //document.getElementById("StratBox" + i.toString()).setAttribute("BusTotal", 1);

    //var row = mainDiv.insertRow(mainDiv.rows.length);
    //var cell = row.insertCell(0);
    ////cell.innerHTML = "<input class=\"txtStrat\"id=\"Strat" + StratBoxCounter.toString() +"Box\" type=\"text\" placeholder=\"Add Strategy Point\" onkeyup=\"addStrat(event)\" />"


    //="<div><input class=\"txtStrat\"id=\"Strat1Box\" type=\"text\" placeholder=\"Add Strategy Point\" onkeyup=\"addStrat(event)\" /> </div>"
    if (obj.value != "") {
        //row = document.getElementById(obj.id + "Row");

        $("#" + obj.id + "BusBox0Row").show();
        document.getElementById(obj.id + "BusBox0Row").setAttribute("shown", "1");

    }

    return false;
}

function addBus(e, obj, i) {

    // add Business Value / Change Business Value text
    //if (e.keyCode == 0) {
    //    StratId = obj.id.split("BusBox")[0];
    //    var mainDiv = document.getElementById(StratId + "Table");

    //    PreviousStratNum = parseInt(obj.id.split('BusBox')[1].split("ProjBox")[0]);
    //    //CurrentStratNum = PreviousStratNum + 1;
    //    //CurrentStratNum = StratBoxCounter;
    //    //StratBoxCounter++;

    //    var varr = document.getElementById(obj.id.split('ProjBox')[0] + "Row").rowIndex;

    //    mainDiv.deleteRow(varr);
    //}

    //if (e.keyCode == 9) {
    //    document.getElementById(obj.id + "ProjBox0").select();
    //    document.getElementById(obj.id + "ProjBox0").focus();
    //    return false;
    //}


    CurrentStratCount = parseInt(obj.id.split("StratBox")[1].split('BusBox')[0]);
    CurrentBusCount = parseInt(obj.id.split("BusBox")[1].split('ProjBox')[0]);
    StratId = obj.id.split("BusBox")[0];
    BusTotal = parseInt(document.getElementById(StratId).getAttribute("BusTotal"));
    CurrentBusId = obj.id.split("BusBox")[0] + "BusBox" + String(BusTotal);


    //delete business value and associated projects
    if (0) {
        StratId = obj.id.split("BusBox")[0];
        var mainDiv = document.getElementById(StratId + "Table");
        PreviousStratNum = parseInt(obj.id.split('BusBox')[1].split("ProjBox")[0]);
        var varr = document.getElementById(obj.id.split('ProjBox')[0] + "Row").rowIndex;
        mainDiv.deleteRow(varr);
    }


    if (e.keyCode === 13 && e.shiftKey && obj.id.indexOf("Proj") <= 0) {
        addStrat(e, obj, i)
    }
        //add business value and project
    else if (e.keyCode === 13) {

        CurrentBusCount = parseInt(obj.id.split("BusBox")[1].split('ProjBox')[0]);
        StratId = obj.id.split("BusBox")[0];
        BusTotal = parseInt(document.getElementById(StratId).getAttribute("BusTotal"));

        //add 1 to BusTotal
        document.getElementById(StratId).setAttribute("BusTotal", parseInt(BusTotal) + 1);

        //BusTotal = obj.id.split("BusBox")[1].split("BusTotal")[1];
        //if (CurrentBusCount >= BusTotal) {

        var url = window.location.href;
        var mapName = url.substr(url.indexOf('?') + 1);
        mapName = mapName.substr(2, mapName.length);

        //add 1 to current business count
        CurrentBusId = obj.id.split("BusBox")[0] + "BusBox" + String(BusTotal);
        var PrevBusID = obj.id.split("BusBox")[0] + "BusBox" + String(BusTotal-1);
        PageMethods.AddBusVal(PrevBusID,document.getElementById(PrevBusID).value,mapName,StratId);

        var mainDiv = document.getElementById(StratId + "Table");
        var RowIndex = document.getElementById(obj.id.split('ProjBox')[0] + "Row").rowIndex + 1;
        newrow = mainDiv.insertRow(RowIndex);
        newrow.setAttribute("id", CurrentBusId + "Row");
        newrow.innerHTML = "<td>" +
                            "<input class='txtBus' ProjTotal=1 id='" + CurrentBusId + "' type='text' placeholder='Add Business Value' onkeyup='addBus(event, this, 1)' /><button class = 'btnDelete' type='button' id='" + CurrentBusId + "Delete' onclick='deleteBus(event, this)'>X</button><br />" +
                            '<div id="projDiv">' +
                            "<input class='txtProj' id='" + CurrentBusId + "ProjBox0' type='text' placeholder='Add Project' onkeyup='addProj(event, this, 1)' /><br />" +
                            "</div>"
        "</td>";


        //place cursor in next Bus box            
        //document.getElementById(CurrentBusId).focus();
        document.getElementById(CurrentBusId).select();
    }
    else if (0) {
        CurrentBusCount = parseInt(obj.id.split("BusBox")[1]);
        CurrentBusId = obj.id.split("BusBox")[0] + "BusBox" + String(CurrentBusCount);
        //document.getElementById(CurrentBusId + 'ProjBox0').focus();
        document.getElementById(CurrentBusId + 'ProjBox0').select();
    }
    else {
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

            console.log(table.rows.count);
            tableid = StratId + "VisualTable";


            //newcell.id = "StratBut" + String(CurrentStratCount) + "BusVisual" + String(CurrentBusCount);

            if (document.getElementById(tableid)) {
                table = document.getElementById(tableid);
                var rowIndex = document.getElementById(obj.id.split('ProjBox')[0] + "Row").rowIndex;
                row = table.insertRow(rowIndex);
                row.style = "height:100px; border-bottom: 1pt solid black; "
                cell = row.insertCell(0);
                cell.id = obj.id + "td";

                row.insertCell(1);
                row.insertCell(2);
                cell1 = row.insertCell(3);
                cell1.style = "width: 1000px; text-align:right; background-color: yellow; padding:0";
                cell1.id = NextVisualId;
                cell1.innerHTML = obj.value;
                currentheight = document.getElementById("StratBut" + String(CurrentStratCount)).style.height;
                document.getElementById("StratBut" + String(CurrentStratCount)).style.height = String(parseInt(currentheight) + 100) + "px";

            }
            else {
                var newcell = row.insertCell(1);
                var w = screen.width;
                newcell.style.width = w.toString() + "px";
                newcell.style.backgroundColor = "yellow";
                tableid = StratId + "VisualTable";
                projtd = obj.id + "td";
                newcell.innerHTML = "<table id='" + tableid + "'>" +
                                    "<tr  style = 'height:100px; border-bottom: 1pt solid black;'>" +
                                        '<td id="' + projtd + '" style=" padding:0">' +
                                        '</td>' +
                                        '<td ></td>' +
                                        '<td ></td>' +
                                        '<td id="' + NextVisualId + '" style=" width: 1000px; text-align:right; background-color: yellow; padding:0">' + obj.value + '</td>' +
                                   '</tr>' +
                                '</table>';
            }
            //newcell.innerHTML = obj.value;
            //newcell.textAlign = "right";


        }
    }
    return false;
}

function addProj(e, obj, i) {


    //if (e.keyCode == 0) {
        //CurrentProjCount = parseInt(obj.id.split("ProjBox")[1]);
        //PreviousStratNum = parseInt(obj.id.split('BusBox')[1].split("ProjBox")[0]);
        //CurrentStratNum = PreviousStratNum + 1;
        //CurrentStratNum = StratBoxCounter;
        //StratBoxCounter++;

    CurrentProjCount = parseInt(obj.id.split("ProjBox")[1]);
    BusId = obj.id.split("ProjBox")[0];
    ProjTotal = document.getElementById(BusId).getAttribute("ProjTotal");
    if (0) {
        CurrentProjCount = parseInt(obj.id.split("ProjBox")[1]);


       // var varr = document.getElementById(obj.id.split('ProjBox')[0] + "Row").rowIndex;
       // var projroot = obj.parentElement;
       // BusId = obj.id.split("ProjBox")[0];
       // ProjTotal = document.getElementById(BusId).getAttribute("ProjTotal");
       // projroot.removeChild(projroot.childNodes[ProjTotal]);
       // subtract 1 from ProjTotal
       // document.getElementById(BusId).setAttribute("ProjTotal", parseInt(ProjTotal) - 1);

    }

    if (e.keyCode === 13 && e.shiftKey) {
        addBus(e, obj, i);
    }
    if (e.keyCode === 13 && e.ctrlKey) {
        addStrat(e, obj, i);
    }
        //add project input box
    else if (e.keyCode === 13) {



        if (CurrentProjCount + 1 >= ProjTotal) {


            var url = window.location.href;
            var mapName = url.substr(url.indexOf('?') + 1);
            mapName = mapName.substr(2, mapName.length);


            PageMethods.AddProject(BusId.toString() + "ProjBox" + CurrentProjCount.toString(),document.getElementById(BusId.toString() + "ProjBox" + CurrentProjCount.toString()).value, mapName.toString(),StratId.toString(),BusId.toString() );

            //add 1 to ProjTotal
            document.getElementById(BusId).setAttribute("ProjTotal", parseInt(ProjTotal) + 1);

            var newInput = document.createElement('input');
            //var br = document.createElement('br');

            newInput.type = 'text';
            newInput.className = 'txtProj';
            newInput.placeholder = 'Add Project';
            newInput.id = BusId + "ProjBox" + String(CurrentProjCount + 1);
            newInput.setAttribute('onkeyup', 'addProj(event, this, 1)');

            var projroot = obj.parentElement;
            projroot.appendChild(newInput);
            //projroot.appendChild(br);

            //Place cursor in next proj box
            //document.getElementById(BusId + "ProjBox" + String(CurrentProjCount + 1)).focus();
            document.getElementById(BusId + "ProjBox" + String(CurrentProjCount + 1)).select();
        }
    }
    else {
        //add visual project
        //if the project exists, change the value
        if (document.getElementById(obj.id + "But")) {
            document.getElementById(obj.id + "But").value = obj.value;
        }
        else {
            var element1 = document.createElement("input");
            element1.type = "button";

            element1.id = obj.id + "But";

            var NewValue = obj.value;
            if (NewValue != "") {
                element1.value = NewValue;
            }

            element1.style.backgroundColor = "green";
            element1.style.height = "33px";
            element1.style.width = "150px";
            element1.style.verticalAlign = "top";
            element1.setAttribute("onclick", "showModal()");


            element1.setAttribute("class", "proj" + String(CurrentProjCount + 1))

            var cell = document.getElementById(BusId + "td");
            cell.appendChild(element1);
        }
    }



    return false;
}

