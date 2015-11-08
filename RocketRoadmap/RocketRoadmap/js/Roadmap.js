var changing = false;
function showTime() {
    if ($(".timeline").is(':visible')) {
        $(".timeline").hide();
    }
    else {
        $(".timeline").show();
    }
    
}

function Drag()
{
    $(".proj" + String(CurrentProjCount + 1)).draggable({ axis: "x" });
    $(".proj" + String(CurrentProjCount + 1)).resizable({ handles: 'e, w' });
    $(".proj3").draggable({ axis: "x" });
    $(".proj3").resizable({ handles: 'e, w' });
}
function addTick(e, obj) {
    if (e.keyCode == 13) {
        var timeline = document.createElement("div");
        timeline.className = "timeline";
        timeline.innerHTML = '<p contenteditable="true" class="timelineText">' + obj.value + '</p>'
        var parent = document.getElementById("containment-wrapper");
        parent.appendChild(timeline);
        $(".timeline").draggable({ axis: "x", containment: "#containment-wrapper", });
    }
}

function hideExample() {
    $("#StratBox0BusBox0Row").hide();
};
 
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

    //delete from database
    var url = window.location.href;
    var mapName = decodeURIComponent(url.substr(url.indexOf('?') + 1));
    mapName = decodeURIComponent(mapName.substr(2, mapName.length).split('#')[0]);
    PageMethods.DeleteStrat('StratBox' + (PreviousStratNum).toString(), mapName);

}

function deleteBus(obj) {
    var StratId = obj.id.split("BusBox")[0];
    var BusId = obj.id.split("Delete")[0];
    var StratTable = document.getElementById(StratId + "Table");
    var RowIndex = document.getElementById(BusId + "Row").rowIndex;
    StratTable.deleteRow(RowIndex);

    try {
        //delete visual row 
        var table = document.getElementById(StratId + "VisualTable");
        table.deleteRow(RowIndex);
        currentheight = document.getElementById("StratBut" + String(CurrentStratCount)).style.height;
        document.getElementById("StratBut" + String(CurrentStratCount)).style.height = String(parseInt(currentheight) - 100) + "px";
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

            var NewValue = obj.value;
            if (NewValue != "") {
                element1.value = NewValue;
            }

           // element1.style.backgroundColor = "white";
           // element1.style.height = "100px";
           // element1.style.width = "150px";
           // element1.style.borderRightStyle = "dashed";
           // element1.style.borderBottomStyle = "solid";
      
           // element1.style.borderLeftStyle = "none";
          //  element1.style.borderTopStyle = "none";
            //  element1.style.borderColor = "#D3D3D3";
            element1.style.height = "100px";
            element1.className = "StratVis";
            


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

            console.log(table.rows.count);
            tableid = StratId + "VisualTable";
            
            if (document.getElementById(tableid)) {
                table = document.getElementById(tableid);
                var rowIndex = document.getElementById(obj.id.split('ProjBox')[0] + "Row").rowIndex;
                row = table.insertRow(rowIndex);
                //row.setAttribute("style", "height:100px; border-width: 1px; border-bottom-style: solid; border-color: #D3D3D3");
                row.className = "RowVis";
                cell = row.insertCell(0);
                cell.id = obj.id + "td";

                row.insertCell(1);
                row.insertCell(2);
                cell1 = row.insertCell(3);
                //cell1.setAttribute("style", "width: 100px; text-align:right; background-color: white; padding:0");
                cell1.className = "CellVis";
                cell1.id = NextVisualId;
                cell1.innerHTML = obj.value;
                currentheight = document.getElementById("StratBut" + String(CurrentStratCount)).style.height;
                document.getElementById("StratBut" + String(CurrentStratCount)).style.height = String(parseInt(currentheight) + 100) + "px";
                console.log(CurrentStratCount);
                console.log(currentheight.toString());
                console.log(String(parseInt(currentheight) + 100) + "px");

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
                                        "<tr class='RowVis' style = 'border-top: 2pt solid; border-top-color: #D3D3D3; '>" +
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
                                        "<tr class='RowVis'>" +
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
            newrow.innerHTML = "<td id='"+CurrentBusId+"inputtd'>" +
                                "<input class='txtBus' ProjTotal=1 id='" + CurrentBusId + "' type='text' placeholder='Add Business Value' onkeyup='addBus(event, this, 1)' /><a href='#' id='"+CurrentBusId + "Delete' class='remove_bus'> X</a><br />" +
                                '<div id="'+CurrentBusId+'projDiv">' +
                                "<input class='txtProjDel' id='" + CurrentBusId + "ProjBox0' type='text' placeholder='Add Project' onkeyup='addProj(event, this, 1)' />" +
                                "</div>"
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
            document.getElementById(obj.id + "But").value = obj.value;
        }
        else {

            var element1 = document.createElement("div");       
            
            element1.id = obj.id + "But";
            

            var NewValue = obj.value;
            if (NewValue != "") {
                element1.innerHTML = "<span>" + NewValue; +"</span>";
            }
            //element1.style.verticalAlign = "top";
            element1.setAttribute("onclick", "showModal(this.id)");
            

            ////set class, location of more than 3 projects will be the 3rd location
            if (CurrentProjCount > 2) {
                element1.setAttribute("class", "proj3");
            }
            else {
                element1.setAttribute("class", "proj" + String(CurrentProjCount + 1));
            }
            
            element1.value = "";

            var cell = document.getElementById(BusId + "td");
            cell.appendChild(element1);
            space = document.createElement('div');
            space.className = 'space';
            space.id = obj.id + 'space';
            cell.appendChild(space);
            

            //enable draggability and resizability
            $(".proj" + String(CurrentProjCount + 1)).draggable({ axis: "x" });
            $(".proj" + String(CurrentProjCount + 1)).resizable({ handles: 'e, w' });
            $(".proj3").draggable({ axis: "x" });
            $(".proj3").resizable({ handles: 'e, w' });

            var element2 = document.createElement('a');
            element2.innerHTML = " X";
            element2.id = obj.id + "Delete";
            element2.setAttribute("href", "#");
            //element2.setAttribute("style", "color:white; font-size:20px; vertical-align:-3px")
            element2.className = 'remove_proj';
            $("#" + BusId + "projDiv").append(element2);
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

