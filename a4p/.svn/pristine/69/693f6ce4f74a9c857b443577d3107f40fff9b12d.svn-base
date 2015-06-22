$(document).ready(function () {
    DrawTable('#tblAddContactList');
    //filteronText('#FirstNameFilter', 1, '#tblAddContactList');
    //filteronText('#LastNameFilter', 2, '#tblAddContactList');

    function addContactinList(dataId) {
        $.post('/SharePetInfo/AddContact', { id: dataId }, function (data) {
            CloseDialogAddContact();
        });
    }

    function CloseDialogAddContact() {
        $('.modal').modal('hide');
        $(".modal-body").empty();
        ShowAjaxSuccessMessage($("#myContactSuccessMessage").val());
    }


    $("#FirstNameFilter").on('keyup click', function () {
        var FirstNameFilter = $('#FirstNameFilter').val();
        $('#tblAddContactList').dataTable().fnFilter('^' + FirstNameFilter + '.*', 1, true);
    });

    $("#LastNameFilter").on('keyup click', function () {
        var LastNameFilter = $('#LastNameFilter').val();
        $('#tblAddContactList').dataTable().fnFilter('^' + LastNameFilter + '.*', 2, true);
    });

});


function fromReload() {
    location.reload();
}