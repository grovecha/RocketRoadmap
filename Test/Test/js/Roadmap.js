var StratBoxCounter = 1;



    function addStrat(e,i) {
        if (e.keyCode === 13) {

            if (i >= StratBoxCounter)
            {

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

                var mainDiv = document.getElementById('sidebar-table');

                var mainroot = document.createElement('tr');
                //add 1 to each 
                StratBoxCounter++;
                mainroot.innerHTML = "<td><div class='mainroot'>" +
                                    "<div id='busroot'>" +
                                    "<input name='p1' class='txtBus' type='text' placeholder='Add Business Value' onkeypress='addBus(event, this," + StratBoxCounter.toString() + ")' /><br />" +
                                    "<div id='projroot'>" +
                                    "<input name='DynamicTextBox' class='txtProj' type='text' placeholder='Add Project' onkeypress='addProj(event, this," + StratBoxCounter.toString() + ")' /><br />" +
                                    "</div>" +
                                    "</div>" +
                                    "<input name='s' class='txtStrat' id='StratBox" + StratBoxCounter.toString() + "' type='text' placeholder='Add Strategy Point' onkeypress='addStrat(event," + StratBoxCounter.toString() + ")'/><br />" +
                                    "</div></td>";

                mainDiv.appendChild(mainroot);

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

        
            var table = document.getElementById("roadmap-table");
            var row = table.rows[i - 2];
        
            console.log(table.rows.count);
            var newcell = row.insertCell(1);
            var w = screen.width;
            newcell.style.width = w.toString()+"px";
            newcell.style.backgroundColor = "yellow";
            newcell.innerHTML = "Business Value";
            var busroot = document.createElement('div');
            //add 1 to current b value
            busroot.innerHTML = "<div id='busroot'>" +
                                "<input class='txtBus' type='text' placeholder='Add Business Value' onkeypress='addBus(event, this)' /><br />" +
                                "<div id='projroot'>" +
                                "<input class='txtProj' id='projBox' type='text' placeholder='Add Project' onkeypress='addProj(event, this)' /><br />" +
                                "</div>" +
                                "</div>";

            var projroot = obj.parentElement;
            projroot.appendChild(busroot);
        }

        return false;
    }

    function addProj(e, obj,i) {
        if (e.keyCode === 13) {

            var newInput = document.createElement('input');
            var br = document.createElement('br');

            newInput.type = 'text';
            newInput.className = 'txtProj';
            newInput.placeholder = 'Add Project';
            newInput.id = 'projBox';
            newInput.setAttribute('onkeypress', 'addProj(event, this)');


            var projroot = obj.parentElement;
            projroot.appendChild(newInput);
            projroot.appendChild(br);

            var newInput2 = document.createElement('input');
            newInput2.type = 'button';
            newInput2.className = 'btnStrat';
            //newInput2.setAttribute('value', "hello");

            newInput2.setAttribute('value', obj.value);
            newInput2.setAttribute('onclick', 'showModal()');



            var placeholder1 = document.getElementById("PlaceHolder1")
            placeholder1.appendChild(newInput2);


        }

        return false;
    }
    function on_click() {
        var newInput2 = document.createElement('input');
        newInput2.type = 'text';
        newInput2.setAttribute('onkeypress', 'addProj(event, this)');

        var placeholder1 = document.getElementById("PlaceHolder1")
        placeholder1.appendChild(newInput2);
    }