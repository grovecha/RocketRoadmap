/**
Created by Eric Nartker
Contains input modal Onclick
**/

function depClick() {
    var count = 1;
    var newinput = $(document.createElement('div')).attr("id", 'DepTextbox' + count);
    newinput.after.html('<input type="text" id="depenText' + count + '" size = 60 /> <button type="button" id="depButton" onclick="depClick">Add Dependecy</button>');
    newinput.appendTo("#dependencyText");
    count++;

}

function depClick2() {
    var count = 1;
    var newinput = $(document.createElement('div')).attr("id", 'DepSelect' + count);
    newinput.('<select id="depenSelect' + count + '"></select>');
    newinput.after.html('<button type="button" id="dep2Button" class="btn btn-default" onclick="depClick2()">Add Dependecy</button>')
    newinput.appendTo("#dependencySelect");
    count++;


}
function linkClick() {
    var count = 1;
    var newinput = $(document.createElement('div')).attr("id", 'link' + count);
    newinput.after.html('<input type="text" id="linkText" size="40"/>');
    newinput.appendTo("#link");
    count++;
}