$(document).ready(function () {
    DrawTable('#tblSelectedContactList');
    //filteronText('#FirstNameFilter', 1, '#tblSelectedContactList');
    //filteronText('#LastNameFilter', 2, '#tblSelectedContactList');
  

    $("#FirstNameFilter").on('keyup click', function () {
        var FirstNameFilter = $('#FirstNameFilter').val();

        $('#tblSelectedContactList').dataTable().fnFilter('^' + FirstNameFilter + '.*', 1, true);
    });

    $("#LastNameFilter").on('keyup click', function () {
        var LastNameFilter = $('#LastNameFilter').val();
        $('#tblSelectedContactList').dataTable().fnFilter('^' + LastNameFilter + '.*', 2, true);
    });

  
    var totalCount = $("#tblSelectedContactList > tbody > tr").find('input[type=checkbox]').length;

    $("#tblSelectedContactList > tbody > tr").find("input[type=checkbox]").click(function (e) {
        var count = $("#tblSelectedContactList > tbody > tr").find('input[type=checkbox]:checked').length;
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
        $('#tblSelectedContactList').dataTable().fnFilter('^' + FirstNameFilter + '.*', 1, true);
        $('#tblSelectedContactList').dataTable().fnFilter('^' + LastNameFilter + '.*', 2, true);

    });

    $("#selectall").click(function (e) {
        var isChecked = $("#selectall").is(":checked");
        $("#tblSelectedContactList > tbody > tr").each(function () {
            $(this).find("input[type=checkbox]").prop("checked", isChecked);
        });
    });

    function CloseDialogShareContacts() {
        $('.modal').modal('hide');
        ShowAjaxSuccessMessage($("#shareContactsSuccessMessage").val());
    }
});