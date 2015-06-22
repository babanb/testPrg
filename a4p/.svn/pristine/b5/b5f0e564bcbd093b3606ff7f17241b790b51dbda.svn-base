$(function () {

    $('#addMedication').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addModalMedication').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addMedicationForm");

            $('#addModalMedication').modal('show');

        });

    });

});

function CloseDialogMedication() {
    $('.modal').modal('hide');
    ShowAjaxSuccessMessage($("#medicationSuccessMessage").val());
}