function CloseDialogReminder() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
    ShowAjaxSuccessMessage($("#reminderSuccessMessage").val());

    $.get('/Calender/CalenderNotification', function (data) {
        $("#dvReminderData").html(data);
    });

}


function editReminder(id, redirectToUrl) {
    $.get('/Calender/Edit?reminderId=' + id + '&redirectToUrl=' + redirectToUrl, function (data) {
        $('#editModalReminder').html(data);
        Initialize();
        $.validator.unobtrusive.parse("#editReminderForm");
        $('#editModalReminder').modal('show');

    });
}

function addReminder(redirectToUrl, clickDate) {
    $.get('/Calender/Add?redirectToUrl=' + redirectToUrl + '&clickDate=' + clickDate, function (data) {
        $('#addModalReminder').html(data);
        Initialize();
        $.validator.unobtrusive.parse("#addReminderForm");
        $('#addModalReminder').modal('show');
    });
}
//function GetCurrentCulture() {
//    return $("meta[name='accept-language']").attr("content");
//}
//var culture = GetCurrentCulture();

//$('#StartDate').datepicker({
//    language: culture,
//    autoclose: true
//});

//$('#EndDate').datepicker({
//    language: culture,
//    autoclose: true,
//    startDate: $('#StartDate').val(),
//    endDate: '0d'
//});
