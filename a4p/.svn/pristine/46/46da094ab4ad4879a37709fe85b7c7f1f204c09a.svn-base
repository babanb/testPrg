$(document).ready(function () {

    DrawTable('#tblS_ContactList');
    //filteronText('#FirstNameFilter', 1, '#tblS_ContactList');
    //filteronText('#LastNameFilter', 2, '#tblS_ContactList');


    $("#FirstNameFilter").on('keyup click', function () {
        var FirstNameFilter = $('#FirstNameFilter').val();
        $('#tblS_ContactList').dataTable().fnFilter('^' + FirstNameFilter + '.*', 1, true);
    });

    $("#LastNameFilter").on('keyup click', function () {
        var LastNameFilter = $('#LastNameFilter').val();
        $('#tblS_ContactList').dataTable().fnFilter('^' + LastNameFilter + '.*', 2, true);
    });



    //Created by Gokul
    var totalCount = $("#tblS_ContactList > tbody > tr").find('input[type=checkbox]').length;

    $("#tblS_ContactList > tbody > tr").find("input[type=checkbox]").click(function (e) {
        var count = $("#tblS_ContactList > tbody > tr").find('input[type=checkbox]:checked').length;
        count = count == undefined ? 0 : count;
        if (count < totalCount) {
            $("#selectall").prop("checked", false);
        }
        else {
            $("#selectall").prop("checked", true);
        }
        $("#FirstNameFilter").val('');
        $("#LastNameFilter").val('');
        var FirstNameFilter = $('#FirstNameFilter').val();
        var LastNameFilter = $('#LastNameFilter').val();
        $('#tblS_ContactList').dataTable().fnFilter('^' + FirstNameFilter + '.*', 1, true);
        $('#tblS_ContactList').dataTable().fnFilter('^' + LastNameFilter + '.*', 2, true);
    });

    $("#selectall").click(function (e) {
        var isChecked = $("#selectall").is(":checked");
        $("#tblS_ContactList > tbody > tr").each(function () {
            $(this).find("input[type=checkbox]").prop("checked", isChecked);
        });
    });

    function CloseDialogShareContacts() {
        $('.modal').modal('hide');
        ShowAjaxSuccessMessage($("#shareContactsSuccessMessage").val());
    }

});