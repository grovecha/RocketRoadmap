/**
Created by Eric Nartker
Contains input modal Onclick
**/

function depClick() {
    $(document).ready(function () {
        var max_fields = 10; //maximum input boxes allowed
        var wrapper = $(".dependencySelect"); //Fields wrapper


        var x = 1; //initlal text box count
        $(addButton).click(function (e) { //on add input button click
            e.preventDefault();
            if (x < max_fields) { //max input box allowed
                x++; //text box increment
                $(wrapper).append('<div><input type="text" id="depenText" size="55" /><a href="#" class="remove_field">Remove</a></div>'); //add input box
            }
        });

        $(wrapper).on("click", ".remove_field", function (e) { //user click on remove text
            e.preventDefault(); $(this).parent('div').remove(); x--;
        })
    });
}

function depClick2() {
    var count = 1;

    count++;


}
function linkClick() {
    var count = 1;
    count++;
}