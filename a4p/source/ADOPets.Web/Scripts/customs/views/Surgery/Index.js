$(function () {

    $('#addSurgery').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#addModalSurgery').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addSurgeryForm");

            $('#addModalSurgery').modal('show');

        });

    });

});

function CloseDialogSurgery() {
    $('.modal').modal('hide');
    ShowAjaxSuccessMessage($("#surgerySuccessMessage").val());
}