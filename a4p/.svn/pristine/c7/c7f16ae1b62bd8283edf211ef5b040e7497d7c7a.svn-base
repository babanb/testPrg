
$(function () {

    $('#addConsultation').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addModalConsultation').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addConsultationForm");

            $('#addModalConsultation').modal('show');

        });

    });

});

function CloseDialogConsultation() {
    $('.modal').modal('hide');
    ShowAjaxSuccessMessage($("#consultationSuccessMessage").val());
}