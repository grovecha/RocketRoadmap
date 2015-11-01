//Created By Eric for the display modal loading functions

$(document).ready(function () {
    //Display Modal Show
    $('#displayModal').on('show.bs.modal', function (e) {
        var disdep_arr = [];
        var seldep_arr = [];
        var dislink_arr = [];
        var distitle_Value = '';
        var disdesc_Value = '';
        var disdep_total, dissel_total, dislink_total;
        var disdep_x, dissel_x, dislink_x;
        var disrisk_Value = '';


        //Add Modal Title
        //title_Value = PageMethods.GetProjectName(button_id, map_Name);
        //$('#display_title').html(title_Value);

        ////Add description
        //disdesc_Value = PageMethods.GetProjectDescription(button_id, map_Name);
        //$('#disdescText').val(disdesc_Value);

        ////Find the number of paragrpahs to display
        //disdep_arr = PageMethods.GetProjectDependencyText(button_id,map_Name);
        //disdep_total = disdep_arr.length;

        ////Add all of the paragrpahs
        //for (disdep_x = 0; disdep_x < disdep_total; disdep_x++) {
        //    $('.disdepText').append("<div class='dep_p'><p>"+disdep_arr[disdep_x]+"</p></div>"); //add input box
        //};


        ////Find the number paragraphs to load for select
        //dissel_arr = PageMethods.GetProjectDependency(button_id,map_Name);
        //dissel_total = dissel_arr.length;

        ////Add all of the paragrpahs for select
        //for (dissel_x = 0; dissel_x < dissel_total; dissel_x++) {
        //    $('.disdepSelect').append("<div class='sel_p'><p>" + dissel_arr[dissel_x] + "</p></div>"); //add input box
        //};

        ////Add description
        //disrisk_Value = PageMethods.GetProjectRisk(button_id, map_Name);
        //$('#disriskText').val(disrisk_Value);

        ////Find the number paragraphs to load for select
        //dislink_arr = PageMethods.GetProjectLinks(button_id, map_Name);
        //dislink_total = dislink_arr.length;

        ////Add all of the paragrpahs for links, this is when it does NOT HAVE QUOTATIONS IN THE STRING
        //for (dislink_x = 0; dislink_x < dislink_total; dislink_x++) {
        //    $('.dislinkText').append("<div class='link_p'><a href=\"" + dislink_arr[dislink_x] + "\">" + dislink_arr[dislink_x] + "</a></div>"); //add input box
        //};

    });

    //diplay modal hidden
    $('#displayModal').on('hidden.bs.modal', function (e) {
        //remove text from description
        $('#disdescText').val('');

        //remove dependecy texts
        $('.disdepText').each(function () {
            $(this).remove();
        });

        //remove selects text
        $('.disdepSelect').each(function () {
            $(this).remove();
        });

        //remove risk text
        $('#disriskText').val('');

        //remove links
        $('.dislinkText').each(function () {
            $(this).remove();
        });
    });
});