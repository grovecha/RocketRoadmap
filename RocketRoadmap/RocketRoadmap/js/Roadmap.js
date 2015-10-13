var StratBoxCounter = 1;



    function addStrat(e,i) {
        if (e.keyCode === 13) {

            if (i >= StratBoxCounter) {

                var table = document.getElementById("roadmap-table");
                var row = table.insertRow(table.rows.length);
                row.id = "StratRow" + StratBoxCounter.toString();
                var button = document.createElement("button");

                var cell1 = row.insertCell(0);
                var element1 = document.createElement("input");
                element1.type = "button";
                element1.name = "Strat";
                element1.id = "StratBut" + StratBoxCounter.toString();

                var NewValue = document.getElementById("StratBox" + i.toString()).value;
                if (NewValue != "")
                {
                    element1.value = NewValue;
                }
 
                element1.style.backgroundColor = "red";
                element1.style.height = "100px";
                element1.style.width = "200px";
                cell1.appendChild(element1);

                var RoadmapName = $_GET["n"];
                console.log(RoadmapName);
                PageMethods.AddStrat("test", "sadfsdf","Test");
     
                //var t = "test";
                //$.ajax({
                //    url: "/Roadmap.aspx/Test",
                //    type: "POST",
                //    contentType: "application/json; charset=utf-8",
                //    dataType:"json",
                //    data:JSON.stringify(t),
                //        success: function (response) {
                //            reponse ? alert("It worked!") : alert("Fuck");
                //        }

                //    });



                var mainDiv = document.getElementById('sidebar-table');

                var mainroot = document.createElement('tr');
                //add 1 to each 
                CurrentStratCount = StratBoxCounter;
                StratBoxCounter++;
                mainroot.innerHTML = "<td><div class='mainroot'>" +
                                    "<div id='busroot'>" +
                                    "<input name='p1' class='txtBus' ProjTotal=1 id='StratBox" + CurrentStratCount.toString() + "BusBox1' type='text' placeholder='Add Business Value' onkeypress='addBus(event, this," + CurrentStratCount.toString() + ")' /><br />" +
                                    "<div id='projroot'>" +
                                    "<input name='DynamicTextBox' id='StratBox" + CurrentStratCount.toString() + "BusBox1ProjBox1' class='txtProj' type='text' placeholder='Add Project' onkeypress='addProj(event, this," + CurrentStratCount.toString() + ")' /><br />" +
                                    "</div>" +
                                    "</div>" +
                                    "<input name='s' class='txtStrat' BusTotal=0 id='StratBox" + StratBoxCounter.toString() + "' type='text' placeholder='Add Strategy Point' onkeypress='addStrat(event," + StratBoxCounter.toString() + ")'/><br />" +
                                    "</div></td>";

                mainDiv.appendChild(mainroot);

                //Place cursor in buxiness Value Box
                document.getElementById("StratBox" + CurrentStratCount.toString() + 'BusBox1').focus();
                document.getElementById("StratBox" + CurrentStratCount.toString() + 'BusBox1').select();

                //add 1 to BusTotal
                document.getElementById("StratBox" + i.toString()).setAttribute("BusTotal", 1);

                //var row = mainDiv.insertRow(mainDiv.rows.length);
                //var cell = row.insertCell(0);
                ////cell.innerHTML = "<input class=\"txtStrat\"id=\"Strat" + StratBoxCounter.toString() +"Box\" type=\"text\" placeholder=\"Add Strategy Point\" onkeypress=\"addStrat(event)\" />"


                //="<div><input class=\"txtStrat\"id=\"Strat1Box\" type=\"text\" placeholder=\"Add Strategy Point\" onkeypress=\"addStrat(event)\" /> </div>"
            }
            else {
                var but = document.getElementById("StratBut" + i.toString());
                var NewValue = document.getElementById("StratBox" + i.toString()).value;
                if (NewValue != "") {
                    but.value = NewValue;
                }
            }

        }

    
    

        return false;
    }

    function addBus(e, obj,i) {
        if (e.keyCode === 13) {
            CurrentBusCount = parseInt(obj.id.split("BusBox")[1]);
            StratId = obj.id.split("BusBox")[0];
            BusTotal = document.getElementById(StratId).getAttribute("BusTotal");

            if (parseInt(CurrentBusCount) >= parseInt(BusTotal)) {

            //add 1 to BusTotal
                document.getElementById(StratId).setAttribute("BusTotal", parseInt(BusTotal) + 1);
            
            //BusTotal = obj.id.split("BusBox")[1].split("BusTotal")[1]; 
            //if (CurrentBusCount >= BusTotal) {
                
                //add 1 to current business count and total
                CurrentBusId = obj.id.split("BusBox")[0] + "BusBox" + String(CurrentBusCount+1);
                //document.getElementById("StratBox" + CurrentStratCount.toString() + 'BusBox1BusTotal1');

                //var table = document.getElementById("roadmap-table");
                //var row = table.rows[i - 2];

                //console.log(table.rows.count);
                //var newcell = row.insertCell(1);
                //var w = screen.width;
                //newcell.style.width = w.toString()+"px";
                //newcell.style.backgroundColor = "yellow";
                //newcell.innerHTML = "Business Value";
                var busroot = document.createElement('div');


                busroot.innerHTML = "<div id='busroot'>" +
                                    "<input class='txtBus' ProjTotal=1 id='" + CurrentBusId + "' type='text' placeholder='Add Business Value' onkeypress='addBus(event, this, 1)' /><br />" +
                                    "<div id='projroot'>" +
                                    "<input class='txtProj' id='" + CurrentBusId + "ProjBox1' type='text' placeholder='Add Project' onkeypress='addProj(event, this, 1)' /><br />" +
                                    "</div>" +
                                    "</div>";

                var projroot = obj.parentElement;
                projroot.appendChild(busroot);

                //Place cursor in Proj Box
                PreviousBusId = obj.id.split("BusBox")[0] + "BusBox" + String(CurrentBusCount);
                document.getElementById(PreviousBusId + 'ProjBox1').focus();
                document.getElementById(PreviousBusId + 'ProjBox1').select();
            }
            //else {
                //change business value
            //}

        }

        return false;
    }

    function addProj(e, obj,i) {
        if (e.keyCode === 13) {

            CurrentProjCount = parseInt(obj.id.split("ProjBox")[1]);
            BusId = obj.id.split("ProjBox")[0];
            ProjTotal = document.getElementById(BusId).getAttribute("ProjTotal");

            if (CurrentProjCount >= ProjTotal) {

                //add 1 to ProjTotal
                document.getElementById(BusId).setAttribute("ProjTotal", parseInt(ProjTotal) + 1);

                var newInput = document.createElement('input');
                var br = document.createElement('br');

                newInput.type = 'text';
                newInput.className = 'txtProj';
                newInput.placeholder = 'Add Project';
                newInput.id = BusId + "ProjBox" + String(CurrentProjCount + 1);
                newInput.setAttribute('onkeypress', 'addProj(event, this, 1)');


                var projroot = obj.parentElement;
                projroot.appendChild(newInput);
                projroot.appendChild(br);

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
 