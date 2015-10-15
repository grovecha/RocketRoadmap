/**
Created by Eric Nartker
Contains input modal Onclick
**/


    $(document).ready(function () {
        var max_fields = 15; //maximum input boxes allowed
        var dep_Text = $(".depText"); //Dependency input wrapper
        var add_Text = $("#addText"); //Add dependency input
        var dep_Select = $(".depSelect"); //Dependency Selection wrapper
        var add_Select = $("#addSelect"); //Add dependency select
        var link_Text = $(".linkText"); //Link input wrapper
        var add_Link = $("#addLink"); //Add Link input
        var dep_count = 1; //initlal dependency input counter
        var select_count = 1; //initial select input counter
        var link_count = 1; //initial link count

        //Dependency addition Functions
        $(add_Text).click(function (e) { //on add input button click
            e.preventDefault();
            if (dep_count < max_fields) { //max input box allowed
                dep_count++; //text box increment
                $(dep_Text).append('<div><input type="text" size=60/><a href="#" class="remove_field">X</a></div>'); //add input box
            }
        });

        $(dep_Text).on("click", ".remove_field", function (e) { //user click on remove text
            e.preventDefault(); $(this).parent('div').remove(); x--;
        })


        //Selection addition Functions
      
        $(add_Select).click(function (e) { //on add input button click
            e.preventDefault();

            var options = "";
            var i, max = 10;
            for (i = 0; i < max; i++) {
                options += "<option value='project" + i + ">Option" + i + "</option>";
            }

            if (select_count < max_fields) { //max input box allowed
                select_count++; //text box increment
                $(dep_Select).append('<div> <select id="depenSelect">' + options +'</select><a href="#" class="remove_field">X</a></div>'); //add input box
            }
        });

        $(dep_Select).on("click", ".remove_field", function (e) { //user click on remove text
            e.preventDefault(); $(this).parent('div').remove(); x--;
        })
  

        //Links Addition Functions
        $(add_Link).click(function (e) { //on add input button click
            e.preventDefault();
            if (link_count < max_fields) { //max input box allowed
                link_count++; //text box increment
                $(link_Text).append('<div><input type="text" size=60/><a href="#" class="remove_field">X</a></div>'); //add input box
            }
        });

        $(wrapper).on("click", ".remove_field", function (e) { //user click on remove text
            e.preventDefault(); $(this).parent('div').remove(); x--;
        })
    
    
    $(add_Link).click(function (e) {
    //make the ajax calls in here?

    })
});