var count = 0;

function StratClick()
{
    var table = document.getElementById("roadmap-table");
    var row = table.insertRow(table.rows.length);
    var button = document.createElement("button");

    var cell1 = row.insertCell(0);
    var element1 = document.createElement("input");
    element1.type = "button";
    element1.name = "Strat";
    element1.value = "Strategy Point " + table.rows.length.toString();
    element1.style.backgroundColor = "red";
    element1.style.height = "100px";
    element1.style.width = "200px";
    cell1.appendChild(element1);



    if (count < 1)
    {
        //var strat = document.getElementById("stratBut1");
        //strat.innerHTML = "<button type=\"button\"> click</button>";
        var sideTable = document.getElementById("sidebar");
        var sideRow = sideTable.insertRow(sideTable.rows.length);
        var sideCell = sideRow.insertCell(0);
        var sideButton = document.createElement("input");
        sideButton.type = "button";
        sideButton.name = "StratInput";
        sideButton.value = "Insert Business Value";


        sideCell.appendChild(sideButton)
        count++;
    }


}

function BusValClick()
{

}