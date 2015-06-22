$(document).ready(function () {
    DrawTable('#tblCommunityContactList');
    //filteronText('#FirstNameFilter', 1, '#tblCommunityContactList');
    //filteronText('#LastNameFilter', 2, '#tblCommunityContactList');
  

    $("#FirstNameFilter").on('keyup click', function () {
        var FirstNameFilter = $('#FirstNameFilter').val();

        $('#tblCommunityContactList').dataTable().fnFilter('^' + FirstNameFilter + '.*', 1, true);
    });

    $("#LastNameFilter").on('keyup click', function () {
        var LastNameFilter = $('#LastNameFilter').val();
        $('#tblCommunityContactList').dataTable().fnFilter('^' + LastNameFilter + '.*', 2, true);
    });

  
    var totalCount = $("#tblCommunityContactList > tbody > tr").find('input[type=checkbox]').length;

    $("#tblCommunityContactList > tbody > tr").find("input[type=checkbox]").click(function (e) {
        var count = $("#tblCommunityContactList > tbody > tr").find('input[type=checkbox]:checked').length;
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
        $('#tblCommunityContactList').dataTable().fnFilter('^' + FirstNameFilter + '.*', 1, true);
        $('#tblCommunityContactList').dataTable().fnFilter('^' + LastNameFilter + '.*', 2, true);
    });

    $("#selectall").click(function (e) {
        var isChecked = $("#selectall").is(":checked");
        $("#tblCommunityContactList > tbody > tr").each(function () {
            $(this).find("input[type=checkbox]").prop("checked", isChecked);
        });
    });

    function CloseDialogShareContacts() {
        $('.modal').modal('hide');
        ShowAjaxSuccessMessage($("#shareContactsSuccessMessage").val());
    }
});