$(function () {

    $('.NewMsgModel').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addModalNewMessage').html(data);
            Initialize();

            $.validator.unobtrusive.parse("#newMessageForm");

            $('#addModalNewMessage').modal('show');

        });

    });

    DrawTable('#userList');
    filteronText('#FirstNameFilter', 1, '#userList');
    filteronText('#LastNameFilter', 2, '#userList');
});


function CloseDialogNewMessage() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
    ShowAjaxSuccessMessage($("#recepientSuccessMessage").val());
}
