var StratBoxCounter = 0;

function deleteRow(e, obj) {

}


//var url = window.location.href;
//var mapName = url.substr(url.indexOf('?') + 1);
//mapName = mapName.substr(2, mapName.length);
//console.log(mapName);

//PageMethods.AddStrat("StratBut" + StratBoxCounter.toString(), "Chases button of doom", mapName);

function addStrat(e, obj, i) {
    if (e.keyCode == 32) {
        var mainDiv = document.getElementById('sidebar-table');

        PreviousStratNum = parseInt(obj.id.split('StratBox')[1].split("BuxBox")[0]);
        //CurrentStratNum = PreviousStratNum + 1;
        //CurrentStratNum = StratBoxCounter;
        StratBoxCounter++;
        var mainDiv = document.getElementById('sidebar-table');
        var varr = document.getElementById('StratBox' + (PreviousStratNum).toString() + "Row").rowIndex;

        mainDiv.deleteRow(varr);
    }

        //add new input boxes
    else if (e.keyCode == 13) {

        PreviousStratNum = parseInt(obj.id.split('StratBox')[1].split("BuxBox")[0]);
        console.log(PreviousStratNum);
        //CurrentStratNum = PreviousStratNum + 1;
        //CurrentStratNum = StratBoxCounter;
        //StratBoxCounter++;
        var mainDiv = document.getElementById('sidebar-table');
        var varr = document.getElementById('StratBox' + (PreviousStratNum).toString() + "Row").rowIndex + 1;
        newrow = mainDiv.insertRow(varr);
        newrow.setAttribute("id", 'StratBox' + StratBoxCounter.toString() + "Row");
        
        //adding to the database
        //var url = window.location.href;
        //var mapName = url.substr(url.indexOf('?') + 1);
        //mapName = mapName.substr(2, mapName.length);
        //var desc = document.getElementById("StratBox" + StratBoxCounter.toString()).value;
        //PageMethods.AddStrat("StratBox" + StratBoxCounter.toString(), desc, mapName);
        
        //StratBoxCounter++;

        newrow.innerHTML = "<td>" +
                        "<input name='s' class='txtStrat' BusTotal=1 id='StratBox" + StratBoxCounter.toString() + "' type='text' placeholder='Add Strategy Point' ontext onkeypress='addStrat(event,this," + StratBoxCounter.toString() + ")'/><br />" +
                        '<table id ="StratBox' + StratBoxCounter.toString() + 'Table"' + ' >' +
                        '<tr id="StratBox' + StratBoxCounter.toString() + 'BusBox0Row" > ' +
                         '<td>' +
                        "<input name='p1' class='txtBus' ProjTotal=1 id='StratBox" + StratBoxCounter.toString() + "BusBox0' type='text' placeholder='Add Business Value' onkeypress='addBus(event, this," + StratBoxCounter.toString() + ")' /><br />" +
                        "<input name='DynamicTextBox' id='StratBox" + StratBoxCounter.toString() + "BusBox0ProjBox0' class='txtProj' type='text' placeholder='Add Project' onkeypress='addProj(event, this," + StratBoxCounter.toString() + ")' /><br />" +
                           '</td>' +
                        '</tr>' +
                        '</table>' +
                        "</td>";

        //place cursor in next strat point
        document.getElementById("StratBox" + StratBoxCounter.toString()).focus();
        document.getElementById("StratBox" + StratBoxCounter.toString()).select();


    }
        //add project point / change project point text
    else if (0) {

        PreviousStratNum = parseInt(obj.id.split('StratBox')[1]);

        document.getElementById("StratBox" + PreviousStratNum.toString() + 'BusBox0').focus();
        document.getElementById("StratBox" + PreviousStratNum.toString() + 'BusBox0').select();
    }
    if (0) {
        PreviousStratNum = parseInt(obj.id.split('StratBox')[1]);
        CurrentStratNum = PreviousStratNum + 1;

        //var table = document.getElementById("roadmap-table");
        //var row = table.insertRow(table.rows.length);
        //row.id = "StratRow" + StratBoxCounter.toString();
        //var button = document.createElement("button");

        //var cell1 = row.insertCell(0);
        //var element1 = document.createElement("input");
        //element1.type = "button";
        //element1.name = "Strat";
        //element1.id = "StratBut" + StratBoxCounter.toString();

        //var NewValue = document.getElementById("StratBox" + i.toString()).value;
        //if (NewValue != "")
        //{
        //    element1.value = NewValue;
        // }

        //element1.style.backgroundColor = "red";
        //element1.style.height = "100px";
        //element1.style.width = "200px";
        //cell1.appendChild(element1);

        var mainDiv = document.getElementById('sidebar-table');

        var mainroot = document.createElement('tr');

        newrow = mainDiv.insertRow(PreviousStratNum);

        //add 1 to each
        CurrentStratCount = StratBoxCounter;
        StratBoxCounter++;
        mainroot.innerHTML = "<td><div class='mainroot'>" +
                            "<input name='s' class='txtStrat' BusTotal=0 id='StratBox" + CurrentStratNum.toString() + "' type='text' placeholder='Add Strategy Point' onkeypress='addStrat(event,this," + CurrentStratNum.toString() + ")'/><br />" +
                            "<div id='busroot'>" +
                            "<input name='p1' class='txtBus' ProjTotal=1 id='StratBox" + CurrentStratNum.toString() + "BusBox0' type='text' placeholder='Add Business Value' onkeypress='addBus(event, this," + CurrentStratNum.toString() + ")' /><br />" +
                            "<div id='projroot'>" +
                            "<input name='DynamicTextBox' id='StratBox" + CurrentStratNum.toString() + "BusBox0ProjBox0' class='txtProj' type='text' placeholder='Add Project' onkeypress='addProj(event, this," + CurrentStratNum.toString() + ")' /><br />" +
                            "</div>" +
                            "</div>" +
                            "</div></td>";

        newrow.appendChild(mainroot);

        //Place cursor in buxiness Value Box
        //document.getElementById("StratBox" + CurrentStratCount.toString() + 'BusBox1').focus();
        //document.getElementById("StratBox" + CurrentStratCount.toString() + 'BusBox1').select();

        //add 1 to BusTotal
        //document.getElementById("StratBox" + i.toString()).setAttribute("BusTotal", 1);

        //var row = mainDiv.insertRow(mainDiv.rows.length);
        //var cell = row.insertCell(0);
        ////cell.innerHTML = "<input class=\"txtStrat\"id=\"Strat" + StratBoxCounter.toString() +"Box\" type=\"text\" placeholder=\"Add Strategy Point\" onkeypress=\"addStrat(event)\" />"


        //="<div><input class=\"txtStrat\"id=\"Strat1Box\" type=\"text\" placeholder=\"Add Strategy Point\" onkeypress=\"addStrat(event)\" /> </div>"

        //else {
        //    var but = document.getElementById("StratBut" + i.toString());
        //    var NewValue = document.getElementById("StratBox" + i.toString()).value;
        //    if (NewValue != "") {
        //        but.value = NewValue;
        //    }
    }

    return false;
}

function addBus(e, obj, i) {
    // add Business Value / Change Business Value text
    if (e.keyCode == 32) {
        StratId = obj.id.split("BusBox")[0];
        var mainDiv = document.getElementById(StratId + "Table");

        PreviousStratNum = parseInt(obj.id.split('BusBox')[1].split("ProjBox")[0]);
        //CurrentStratNum = PreviousStratNum + 1;
        //CurrentStratNum = StratBoxCounter;
        //StratBoxCounter++;

        var varr = document.getElementById(obj.id.split('ProjBox')[0] + "Row").rowIndex;

        mainDiv.deleteRow(varr);
    }

    if (e.keyCode === 13 && e.shiftKey && obj.id.indexOf("Proj") <= 0) {
        addStrat(e, obj, i)
    }
        //add input boxes
    else if (e.keyCode === 13) {
        CurrentBusCount = parseInt(obj.id.split("BusBox")[1].split('ProjBox')[0]);
        StratId = obj.id.split("BusBox")[0];
        BusTotal = document.getElementById(StratId).getAttribute("BusTotal");

        //if (parseInt(CurrentBusCount) >= parseInt(BusTotal)) {

        //add 1 to BusTotal
        document.getElementById(StratId).setAttribute("BusTotal", parseInt(BusTotal) + 1);

        //BusTotal = obj.id.split("BusBox")[1].split("BusTotal")[1];
        //if (CurrentBusCount >= BusTotal) {

        //add 1 to current business count and total
        CurrentBusId = obj.id.split("BusBox")[0] + "BusBox" + String(BusTotal + 1);
        //document.getElementById("StratBox" + CurrentStratCount.toString() + 'BusBox1BusTotal1');

        //var table = document.getElementById("roadmap-table");
        //var row = table.rows[i - 2];

        //console.log(table.rows.count);
        //var newcell = row.insertCell(1);
        //var w = screen.width;
        //newcell.style.width = w.toString()+"px";
        //newcell.style.backgroundColor = "yellow";
        //newcell.innerHTML = "Business Value";

        //adding to the database
        var url = window.location.href;
        var mapName = url.substr(url.indexOf('?') + 1);
        mapName = mapName.substr(2, mapName.length);
        PageMethods.AddBusVal(CurrentBusId, "A new Business Value", mapName, StratId);

        PreviousBusId = parseInt(obj.id.split('StratBox')[1].split('ProjBox')[0]);

        //CurrentStratNum = PreviousStratNum + 1;
        //CurrentStratNum = StratBoxCounter;
        StratBoxCounter++;
        var mainDiv = document.getElementById(StratId + "Table");
        var row = document.getElementById(obj.id.split('ProjBox')[0] + "Row").rowIndex + 1;
        newrow = mainDiv.insertRow(row);
        newrow.setAttribute("id", CurrentBusId + "Row");


        newrow.innerHTML = "<td>" +
                            "<input class='txtBus' ProjTotal=1 id='" + CurrentBusId + "' type='text' placeholder='Add Business Value' onkeypress='addBus(event, this, 1)' /><br />" +
                            '<div id="projDiv">' +
                            "<input class='txtProj' id='" + CurrentBusId + "ProjBox0' type='text' placeholder='Add Project' onkeypress='addProj(event, this, 1)' /><br />" +
                            "</div>"
        "</td>";



        //Place cursor in next Bus box            
        document.getElementById(CurrentBusId).focus();
        document.getElementById(CurrentBusId).select();
    }
    else if (0) {
        CurrentBusCount = parseInt(obj.id.split("BusBox")[1]);
        CurrentBusId = obj.id.split("BusBox")[0] + "BusBox" + String(CurrentBusCount);
        document.getElementById(CurrentBusId + 'ProjBox0').focus();
        document.getElementById(CurrentBusId + 'ProjBox0').select();
    }
    //else {
    //change business value
    //}

    //}

    return false;
}

function addProj(e, obj, i) {

    if (e.keyCode == 32) {
        CurrentProjCount = parseInt(obj.id.split("ProjBox")[1]);
        //PreviousStratNum = parseInt(obj.id.split('BusBox')[1].split("ProjBox")[0]);
        //CurrentStratNum = PreviousStratNum + 1;
        //CurrentStratNum = StratBoxCounter;
        //StratBoxCounter++;

        // var varr = document.getElementById(obj.id.split('ProjBox')[0] + "Row").rowIndex;
        var projroot = obj.parentElement;
        BusId = obj.id.split("ProjBox")[0];
        ProjTotal = document.getElementById(BusId).getAttribute("ProjTotal");
        projroot.removeChild(projroot.childNodes[ProjTotal]);
        //subtract 1 from ProjTotal
        document.getElementById(BusId).setAttribute("ProjTotal", parseInt(ProjTotal) - 1);

    }

    if (e.keyCode === 13 && e.shiftKey) {
        addBus(e, obj, i);
    }

    else if (e.keyCode === 13) {

        CurrentProjCount = parseInt(obj.id.split("ProjBox")[1]);
        BusId = obj.id.split("ProjBox")[0];
        ProjTotal = document.getElementById(BusId).getAttribute("ProjTotal");

        if (CurrentProjCount + 1 >= ProjTotal) {

            //add 1 to ProjTotal
            document.getElementById(BusId).setAttribute("ProjTotal", parseInt(ProjTotal) + 1);

            var newInput = document.createElement('input');
            //var br = document.createElement('br');

            newInput.type = 'text';
            newInput.className = 'txtProj';
            newInput.placeholder = 'Add Project';
            newInput.id = BusId + "ProjBox" + String(CurrentProjCount + 1);
            newInput.setAttribute('onkeypress', 'addProj(event, this, 1)');

            //adding to the database
            var url = window.location.href;
            var mapName = url.substr(url.indexOf('?') + 1);
            mapName = mapName.substr(2, mapName.length);
            PageMethods.addProj(BusId + "ProjBox" + String(CurrentProjCount + 1), "A new Project", mapName, StratId,BusId);


            var projroot = obj.parentElement;
            projroot.appendChild(newInput);
            //projroot.appendChild(br);

            //Place cursor in next proj box
            document.getElementById(BusId + "ProjBox" + String(CurrentProjCount + 1)).focus();
            document.getElementById(BusId + "ProjBox" + String(CurrentProjCount + 1)).select();

        }
        //else {
        //change proj value
        //}
    }

    return false;
}

